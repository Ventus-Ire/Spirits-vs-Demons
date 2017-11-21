using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
	public Transform target;
	public float distance = 5.0f;
	public float height = 1.5f;
	public float heightDamping = 2.0f;
	public float positionDamping = 2.0f;
	public float rotationDamping = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (!target)
			return;
		float wantedHeight = target.position.y + height;
		float currentHeight = transform.position.y;

		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);

		Vector3 wantedPosition = target.position - target.forward * distance;
		transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * positionDamping);

		transform.position = new Vector3 (transform.position.x, currentHeight, transform.position.z);

		transform.forward = Vector3.Lerp (transform.forward, target.forward, Time.deltaTime * rotationDamping);
	}
}
