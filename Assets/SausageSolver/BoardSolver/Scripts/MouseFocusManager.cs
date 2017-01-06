using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MouseFocusManager : MonoBehaviour
{
	public void Update()
	{
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); 

		RaycastHit raycastResult; 
		Physics.Raycast(
			mouseRay, 
			out raycastResult, 
			100.0f); // maxDistance
		
		SetFocus(raycastResult.transform);
	}
	
	private Transform currentFocus = null;

	private void SetFocus(
		Transform newFocus)
	{
		if (newFocus != currentFocus)
		{
			if (currentFocus != null)
			{
				CellEditor previousOwningCell = currentFocus.GetComponentInParent<CellEditor>();

				if (previousOwningCell != null)
				{
					previousOwningCell.SetFocusedChild(null);
				}
			}

			if (newFocus != null)
			{
				CellEditor newOwningCell = newFocus.GetComponentInParent<CellEditor>();
				
				if (newOwningCell != null)
				{
					newOwningCell.SetFocusedChild(newFocus);
				}
			}

			currentFocus = newFocus;
		}
	}
}

