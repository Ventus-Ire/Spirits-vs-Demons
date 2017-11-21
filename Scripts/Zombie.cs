using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Vehicle {

	public float seekingWeight;
	public float numAhead = 5f;
	private GameObject target;
	private Vector3 center = new Vector3(0f, 0f, 0f);
	public float radiusWander = 3f;
	float boundsWeight = 100f;
	public GameObject[] zombies;

	private bool debugRen = false;
	public Material m1;
	public Material m2;
	public Material m3;

	// Use this for initialization
	protected override void Start () {
		zombies = GameObject.FindGameObjectsWithTag ("Zombie");
		base.Start ();
	}

    //Calculates the steering forces for various scenarios in the area
    //Also houses the "OnRenderObject" to draw debug line
    public override void CalcSteeringForces() {
		//AvoidObstacle (FindClosest ("Tree"), 3f);


		Vector3 ultimateForce = Vector3.zero;
		ApplyForce(AvoidObstacle (FindClosest ("Tree"), 300f));
		if (OutOfBounds ()) 
		{
			ultimateForce += Seek(center) * boundsWeight;
		}

		target = FindClosest ("Human");
		if (target == null) {
			ultimateForce += Seek(CalcWander());
			ultimateForce = Vector3.ClampMagnitude (ultimateForce, 10000f)  ;
			ApplyForce (ultimateForce);
		} else {
			//ultimateForce += Separation(zombies, this.gameObject);
			ultimateForce += Seek (FindClosest ("Human").transform.position);
			ultimateForce.Normalize ();
			ultimateForce *= 2f;
			ultimateForce = Vector3.ClampMagnitude (ultimateForce, maxForce);
			ApplyForce (ultimateForce);
		}
		OnRenderObject ();

	}

    //Determines whether a "zombie" is out of bounds
    private bool OutOfBounds()
	{
		Vector3 currPos = gameObject.transform.position;

		if (currPos.x <= -15f || currPos.x >= 15f || currPos.z <= -15f || currPos.z >= 15f) {
			return true;
		} else {
			return false;
		}
	}

    //Draws the debug lines for the zombie objects
	void OnRenderObject() {
		if (Input.GetKeyDown (KeyCode.D)) {
			if (debugRen == false) {
				debugRen = true;
			} else if (debugRen == true) {
				debugRen = false;
			}

		}
		if (debugRen == true) { //  && Camera.current.name == "Camera"
			m1.SetPass (0);

			GL.Begin (GL.LINES);
			GL.Vertex (transform.position);
			GL.Vertex (FindClosest ("Human").transform.position);
			GL.End ();

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



}
