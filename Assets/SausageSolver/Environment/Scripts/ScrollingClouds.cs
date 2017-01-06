using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ScrollingClouds : MonoBehaviour
{
	public Vector3 LocalVelocity = Vector3.forward;
	public float WrappingDistance = 1.0f;

	public void Update()
	{
		float oldDistance = currentDistance;

		currentDistance += (LocalVelocity.magnitude * Time.deltaTime);
		currentDistance = (currentDistance % WrappingDistance);

		float updateDistanceDelta = (currentDistance - oldDistance);

		transform.localPosition += (LocalVelocity * updateDistanceDelta);
	}

	private float currentDistance = 0.0f;
}