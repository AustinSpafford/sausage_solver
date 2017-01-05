using UnityEngine;
using System.Collections;

public class BoardEditor : MonoBehaviour
{
	public void Awake()
	{
		boardState = new BoardState(InitialCellCountX, InitialCellCountY);
		
		for (int xIndex = 0; xIndex < boardState.CellCountX; ++xIndex)
		{
			for (int yIndex = 0; yIndex < boardState.CellCountY; ++yIndex)
			{
				GameObject newCell = Instantiate(CellPrefab);

				newCell.transform.SetParent(
					this.transform,
					false); // worldPositionStays

				newCell.transform.localPosition = 
					new Vector3(
						(float)(xIndex - (boardState.CellCountX / 2)),
						0.0f,
						(float)(yIndex - (boardState.CellCountY / 2)));

				newCell.GetComponent<CellEditor>().TargetCell = boardState.GetCell(xIndex, yIndex);
			}
		}
	}

	[SerializeField]
	private int InitialCellCountX = 10;
	[SerializeField]
	private int InitialCellCountY = 10;
	
	[SerializeField]
	private GameObject CellPrefab = null;

	private BoardState boardState = null;
}

