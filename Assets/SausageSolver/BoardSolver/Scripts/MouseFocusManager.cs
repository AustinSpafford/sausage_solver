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
				SelectableCellFace oldFocusedFace = currentFocus.GetComponentInParent<SelectableCellFace>();

				if (oldFocusedFace != null)
				{
					oldFocusedFace.SetFocused(false);
				}
			}

			if (newFocus != null)
			{
				SelectableCellFace newFocusedFace = newFocus.GetComponentInParent<SelectableCellFace>();
				
				if (newFocusedFace != null)
				{
					newFocusedFace.SetFocused(true);
				}
			}

			currentFocus = newFocus;
		}
	}
}

