using UnityEngine;
using System.Collections;

public class GrabBlocks : MonoBehaviour
{

	//This function should hold just the boolean to check if the player is holding it
	public bool holding = false;
	public GameObject p;
	Ray myRay;
	RaycastHit hit;
	private Vector3 rayOffset;
	private Vector3 blockOffset;
	private float distance = 2.0f;
	public Rigidbody rb;
   
	// Use this for initialization
	void Start ()
	{
		myRay = new Ray (p.transform.position + rayOffset, p.transform.forward * distance);
		rayOffset = new Vector3 (0, .5f, 0);
		blockOffset = new Vector3 (0, 2.0f, 0);
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.DrawRay(p.transform.position + rayOffset, p.transform.forward * distance);
		//Debug.DrawRay(transform.position, Input.mousePosition * distance);
		if (holding) {
			transform.position = p.transform.position + blockOffset + p.transform.forward * 1.0f;
			DisableKinematic ();
		} 
		if(!holding)
		{
			EnableKinematic ();
		}

	}

	void rayRangeTest ()
	{

		RaycastHit hit;
		if (Physics.Raycast (myRay, out hit, distance)) {

			if (hit.collider.tag == "Player") {
				Debug.Log (hit.collider.tag);
			}

		} else {
			Debug.Log ("Nothing");
		}
	}

	void EnableKinematic ()
	{
		if (GameObject.FindGameObjectWithTag("Cube")) {
			rb.isKinematic = false;
			rb.detectCollisions = true;
		}
	}

	void DisableKinematic ()
	{
		if (GameObject.FindGameObjectWithTag ("Cube")) 
		{
			rb.isKinematic = true;
			rb.detectCollisions = false;
		}
	}

}
