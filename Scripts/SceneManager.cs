using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

	public GameObject purple;
	public GameObject zombie;
	public GameObject human;
	public GameObject[] humans;
	private GameObject zombieI;


	private bool debugRen = false;
	public Material m1;
	// Use this for initialization
	void Start () {
		//Instantiate (purple, new Vector3 (Random.Range (-9f, 9f), 0f, Random.Range (-9f, 9f)), Quaternion.identity);

		Instantiate (zombie, new Vector3 (Random.Range (-14f, 14f), 0f, Random.Range (-14f, 14f)), Quaternion.identity);
		//Instantiate (zombie, new Vector3 (Random.Range (-9f, 9f), 0f, Random.Range (-9f, 9f)), Quaternion.identity);

		for (int i = 0; i < 10; i++) {
			Instantiate (human, new Vector3 (Random.Range (-14f, 14f), 0f, Random.Range (-14f, 14f)), Quaternion.identity);

		}

		humans = GameObject.FindGameObjectsWithTag ("Human");

		//Debug.Log (humans [0]);
		zombieI = GameObject.FindGameObjectWithTag ("Zombie");
		//Debug.Log (purpleTransform);

	}
	
	// Update is called once per frame
	void Update () {
		//CircleCollision ();
		if (Input.GetKeyDown(KeyCode.Z)) {
			Instantiate (zombie, new Vector3 (Random.Range (-14f, 14f), 0f, Random.Range (-14f, 14f)), Quaternion.identity);
		}
		if (Input.GetKeyDown(KeyCode.H)) {
			Instantiate (human, new Vector3 (Random.Range (-14f, 14f), 0f, Random.Range (-14f, 14f)), Quaternion.identity);
		}	
	}


    //Finds the closest object of "name"
    //Name is a parameter that looks for the tag of a game object
	public GameObject FindClosest(string name) {
		GameObject[] humans;
		humans = GameObject.FindGameObjectsWithTag (name);
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject human in humans) {
			Vector3 diff = human.transform.position - position;
			float curDist = diff.sqrMagnitude;
			if (curDist < distance) {
				closest = human;
				distance = curDist;
			}
		}
		return closest;
	}
}
