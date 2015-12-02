using UnityEngine;
using System.Collections;

public class GrabBlocks : MonoBehaviour
{

	//This function should hold just the boolean to check if the player is holding it
	public bool grabbed = false;
	public GameObject p;
	//Ray myRay;
    Ray blockFall;
	RaycastHit hit;
	//private Vector3 rayOffset;
	private Vector3 blockOffset;
	//private float distance = 2.0f;
	public Rigidbody rb;
    private bool ground;
    
   
	// Use this for initialization
	void Start ()
	{
		//myRay = new Ray (p.transform.position + rayOffset, p.transform.forward * distance);
		//rayOffset = new Vector3 (0, .5f, 0);
		blockOffset = new Vector3 (0, 1.5f, 0);
		rb = GetComponent<Rigidbody> ();
        
	}

	void fixedUpdate()
    {
        //fallingBlock();
    }

	// Update is called once per frame
	void Update ()
	{
		//Debug.DrawRay(p.transform.position + rayOffset, p.transform.forward * distance);
		//Debug.DrawRay(transform.position, Input.mousePosition * distance);

        //fallingBlock();

		if (grabbed) {
			transform.position = p.transform.position + blockOffset + p.transform.forward * .8f;
			DisableKinematic ();
		} 
		if(!grabbed)
		{
			EnableKinematic ();
            //transform.position -= new Vector3(0, .2f, 0);
		}

	}

    


/*
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
*/

    void fallingBlock()
    {
        blockFall = new Ray(transform.position + blockOffset, transform.up * -.6f);
       
        if (Physics.Raycast(blockFall, .2f))
        {
            ground = true;
            Debug.Log("Ground");
        }
        else
        {
            ground = false;
            Debug.Log("Not");
        }

        if(ground){
            rb.AddForce(0, -9.81f, 0);
        }
        else
        {
            rb.AddForce(Vector3.zero);
        }
        
        
    }

	void EnableKinematic ()
	{
		if (GameObject.FindGameObjectWithTag("Cube")) {
			rb.isKinematic = false;
			rb.detectCollisions = true;
            rb.velocity = Vector3.zero;
		}
	}

	void DisableKinematic ()
	{
		if (GameObject.FindGameObjectWithTag ("Cube")) 
		{
			rb.isKinematic = true;
			rb.detectCollisions = false;
            rb.velocity = Vector3.zero;
		}
	}

}
