using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SelectableCellFace : MonoBehaviour
{
	public void Awake()
	{
		selectionCollider = GetComponent<BoxCollider>();
	}

	public void Start()
	{
		parentCellEditor = transform.GetComponentInParent<CellEditor>();
	}

	public void SetFocused(
		bool shouldBeFocused)
	{
		if (shouldBeFocused != isFocused)
		{
			if (isFocused)
			{
				parentCellEditor.SetFocusedChild(null);

				if (selectionEffectInstance != null)
				{
					GameObject.Destroy(selectionEffectInstance);

					selectionEffectInstance = null;
				}
			}

			if (shouldBeFocused)
			{			
				parentCellEditor.SetFocusedChild(this.transform);

				if (SelectionEffectPrefab != null)
				{
					selectionEffectInstance = Instantiate(SelectionEffectPrefab);

					selectionEffectInstance.transform.SetParent(
						this.transform,
						false); // worldPositionStays

					if (selectionCollider != null)
					{
						selectionEffectInstance.transform.localPosition = selectionCollider.center;
						selectionEffectInstance.transform.localScale = selectionCollider.size;
					}
				}
			}

			isFocused = shouldBeFocused;
		}
	}
		
	[SerializeField]
	private GameObject SelectionEffectPrefab = null;

	private CellEditor parentCellEditor = null;
	private BoxCollider selectionCollider = null;

	private bool isFocused = false;
	private GameObject selectionEffectInstance = null;
}

