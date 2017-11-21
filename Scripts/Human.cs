using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Vehicle {
	
	public float seekingWeight;
	//public GameObject target;
	public GameObject[] humans;
	private Vector3 center = new Vector3(0f, 0f, 0f);
	float boundsWeight = 100f;
	public float radiusWander = 3f;
	public float numAhead = 5f;

	private bool debugRen = false;
	public Material m1;
	public Material m2;
	public Material m3;


	// Use this for initialization
	protected override void Start () {
		base.Start ();
		//Used for 1st Calc Steering Force
	
		//target = GameObject.FindGameObjectWithTag ("Purple");
		//obst = GameObject.FindGameObjectWithTag ("Human");
		humans = GameObject.FindGameObjectsWithTag ("Human");

	}

    //Calculates the steering forces for various scenarios in the area
    //Also houses the "OnRenderObject" to draw debug line
	public override void CalcSteeringForces() {
		//Rendererer ();

		Vector3 ultimateForce = Vector3.zero;
		ApplyForce(AvoidObstacle (FindClosest("Tree"), 300f));
		if (OutOfBounds ()) 
		{
			ultimateForce += Seek(center) * boundsWeight;
		}

		GameObject zombie = FindClosest("Zombie");
		float o2x = zombie.transform.position.x;
		float o2y = zombie.transform.position.z;
		float o1x = transform.position.x;
		float o1y = transform.position.z;
		float distance = Mathf.Sqrt (((o1x - o2x) * (o1x - o2x)) + ((o1y - o2y) * (o1y - o2y)));
		if (zombie == null || distance >= 6f) {
			//ApplyForce(Separation (humans, this.gameObject));
			ultimateForce += Seek(CalcWander ()); //CalcWander (1f, 2f, gameObject)
			//ultimateForce.Normalize ();
			//ultimateForce *= maxForce;
			ultimateForce = Vector3.ClampMagnitude (ultimateForce, maxForce);
			ApplyForce (ultimateForce);
			//ApplyForce(Separation (humans, this.gameObject));
		} else {
			
			if (distance < 6f) {
				ultimateForce += Flee (zombie.transform.position);
				ultimateForce = Vector3.ClampMagnitude (ultimateForce, maxForce);
				ApplyForce (ultimateForce);
			}
			if (distance < 1.2f) {
				Vector3 savePoint = transform.position;
				Destroy (gameObject);
				Instantiate (zombie, savePoint, Quaternion.identity);
			}
		
		}
		OnRenderObject();
	}

    //Determines whether a "human" is out of bounds
	private bool OutOfBounds()
	{
		Vector3 currPos = gameObject.transform.position;

		if (currPos.x <= -15f || currPos.x >= 15f || currPos.z <= -15f || currPos.z >= 15f) {
			return true;
		} else {
			return false;
		}
	}

	void OnRenderObject() {
		if (Input.GetKeyDown (KeyCode.D)) {
			if (debugRen == false) {
				debugRen = true;
			} else if (debugRen == true) {
				debugRen = false;
			}

		}
		if (debugRen == true) { //  && Camera.current.name == "Camera"

			m2.SetPass (0);

			GL.Begin (GL.LINES);
			GL.Vertex (gameObject.transform.position);
			GL.Vertex (gameObject.transform.position + gameObject.transform.forward * 2f);
			GL.End ();

			m3.SetPass (0);

			GL.Begin (GL.LINES);
			GL.Vertex (gameObject.transform.position);
			GL.Vertex (gameObject.transform.position + gameObject.transform.right * 2f);
			GL.End ();

		}

	}

	/*
	void Rendererer() {
		if (Input.GetKeyDown (KeyCode.D)) {
			if (debugRen == false) {
				debugRen = true;
			} else if (debugRen == true) {
				debugRen = false;
			}
				
		}
		if (debugRen == true && FindClosest("Zombie") == null) {
			Debug.DrawLine (transform.position, FindClosest ("Zombie").transform.position, Color.red);
		}

	}
	*/
}
