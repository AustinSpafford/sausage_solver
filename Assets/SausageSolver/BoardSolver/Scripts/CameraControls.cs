using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CameraControls : MonoBehaviour
{
	public void Update()
	{
		Vector3 inputDirection = 
			new Vector3(
				Input.GetAxis("Horizontal"),
				0.0f,
				Input.GetAxis("Vertical"));

		transform.localPosition += (inputDirection * (Speed * Time.deltaTime));
	}

	[SerializeField]
	private float Speed = 1.0f;
}