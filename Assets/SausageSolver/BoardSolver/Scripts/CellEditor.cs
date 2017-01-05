using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CellEditor : MonoBehaviour
{
	public CellState TargetCell = null;

	public void Start()
	{
		UpdateCellDisplay();
	}

	public void UpdateCellDisplay()
	{
		if (TargetCell == null)
		{
			GameObject[] children = 
				Enumerable.Range(0, transform.childCount)
					.Select(index => transform.GetChild(index).gameObject)
					.ToArray();

			foreach (GameObject child in children)
			{
				Destroy(child);
			}
		}
		else
		{
			GameObject wantedTopPrefab;

			if (TargetCell.IsGrill)
			{
				wantedTopPrefab = GrillPrefab;
			}
			else if (TargetCell.Height > 0)
			{
				wantedTopPrefab = GrassPrefab;
			}
			else
			{
				wantedTopPrefab = WaterPrefab;
			}

			SetInstance(
				wantedTopPrefab,
				0.0f, // yAxisRotationAngle
				ref topInstance,
				ref topSourcePrefab);
			
			SetInstance(
				(TargetCell.CanBeClimbedByVerb[(int)PlayerVerb.MoveSouth] ? LadderPrefab : DirtPrefab),
				180.0f, // yAxisRotationAngle
				ref northInstance,
				ref northSourcePrefab);
			
			SetInstance(
				(TargetCell.CanBeClimbedByVerb[(int)PlayerVerb.MoveNorth] ? LadderPrefab : DirtPrefab),
				0.0f, // yAxisRotationAngle
				ref southInstance,
				ref southSourcePrefab);
			
			SetInstance(
				(TargetCell.CanBeClimbedByVerb[(int)PlayerVerb.MoveWest] ? LadderPrefab : DirtPrefab),
				-90.0f, // yAxisRotationAngle
				ref eastInstance,
				ref eastSourcePrefab);
			
			SetInstance(
				(TargetCell.CanBeClimbedByVerb[(int)PlayerVerb.MoveEast] ? LadderPrefab : DirtPrefab),
				90.0f, // yAxisRotationAngle
				ref westInstance,
				ref westSourcePrefab);

			topInstance.transform.localPosition = new Vector3(0.0f, (float)TargetCell.Height, 0.0f);

			Vector3 sideLocalScale = new Vector3(1.0f, (float)TargetCell.Height, 1.0f);

			northInstance.transform.localScale = sideLocalScale;
			southInstance.transform.localScale = sideLocalScale;
			eastInstance.transform.localScale = sideLocalScale;
			westInstance.transform.localScale = sideLocalScale;

			bool shouldShowSides = (TargetCell.Height > 0);

			northInstance.SetActive(shouldShowSides);
			southInstance.SetActive(shouldShowSides);
			eastInstance.SetActive(shouldShowSides);
			westInstance.SetActive(shouldShowSides);
		}
	}

	private GameObject topInstance = null;
	private GameObject topSourcePrefab = null;

	private GameObject northInstance = null;
	private GameObject northSourcePrefab = null;

	private GameObject southInstance = null;
	private GameObject southSourcePrefab = null;

	private GameObject eastInstance = null;
	private GameObject eastSourcePrefab = null;

	private GameObject westInstance = null;
	private GameObject westSourcePrefab = null;
	
	[SerializeField]
	private GameObject DirtPrefab = null;
	[SerializeField]
	private GameObject GrassPrefab = null;
	[SerializeField]
	private GameObject GrillPrefab = null;
	[SerializeField]
	private GameObject LadderPrefab = null;
	[SerializeField]
	private GameObject WaterPrefab = null;

	private void SetInstance(
		GameObject wantedPrefab,
		float yAxisRotationAngle,
		ref GameObject inoutInstance,
		ref GameObject inoutSourcePrefab)
	{
		if (inoutSourcePrefab != wantedPrefab)
		{
			inoutInstance = Instantiate(wantedPrefab);

			inoutInstance.transform.SetParent(
				this.transform,
				false); // worldPositionStays

			inoutInstance.transform.rotation = (
				Quaternion.AngleAxis(yAxisRotationAngle, Vector3.up) *
				inoutInstance.transform.rotation);

			inoutSourcePrefab = wantedPrefab;
		}
	}
}

