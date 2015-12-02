using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public Collider door;
	public float movementSpeed = 5.0f;
	//public float tSpeed = 50.0f;
    
	//Ray myRay;
	public Rigidbody rb;
	public Animator child;
	public AudioSource newLevel;
	public AudioSource pickUp;
	public AudioSource jumpSound;
	Ray ray;
	Ray grounded;
	Ray mColl;
	public Camera orthoCam;
	Vector3 rayOffset;
	Vector3 originPoint;
	private bool inAir = false;
	private bool ground;
	private float maxHeight;
	public bool holding = false;
	const float gravity = 9.81f;
	public bool enabled;
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		child = GetComponent<Animator> ();
		rayOffset = new Vector3 (0, .5f, 0);
		//blockOffset = new Vector3 (0, 1.5f, 0);
		originPoint = rb.transform.position;
		//this.gameObject.AddComponent<AudioSource>();
		//this.GetComponent<AudioSource>().clip = myclip;
		//this.GetComponent<AudioSource>().Play();
	}

	void OnTriggerEnter (Collider door)
	{
		if (door.name == "Door1") {
			this.transform.position = GameObject.Find ("SpawnLvl2").transform.position;
			orthoCam.transform.Translate (orthoCam.transform.position.x - 100, 0, 0);
			newLevel.Play ();
		} else if (door.name == "Door2") {
			this.transform.position = GameObject.Find ("SpawnLvl3").transform.position;
			orthoCam.transform.Translate (orthoCam.transform.position.x - 200, 0, 0);
			newLevel.Play ();
		} else if (door.name == "Door3") {
			this.transform.position = GameObject.Find ("SpawnLvl4").transform.position;
			orthoCam.transform.Translate (orthoCam.transform.position.x - 300, 0, 0);
			newLevel.Play ();
		} else if (door.name == "Door4") {
			newLevel.Play ();
			Debug.Log ("THE END");
		} else {
			Debug.Log ("No more levels");
		}
	}
	// Update is called once per frame
	void Update ()
	{
            
		ray = new Ray (transform.position + rayOffset, transform.forward * 1);
		//Debug.DrawRay(transform.position + rayOffset, transform.forward * 1);

		grounded = new Ray (transform.position + rayOffset, transform.up * -.6f);
		//Debug.DrawRay(transform.position + rayOffset, transform.up * -.6f);

		mColl = new Ray (transform.position + rayOffset, transform.forward * .8f);
		Debug.DrawRay (transform.position + rayOffset, transform.forward * .8f);

		if (Physics.Raycast (grounded, .6f)) {
			ground = true;
			//Debug.Log("Ground");
		} else {
			ground = false;
		}

		if (Input.GetKey (KeyCode.W) && enabled) {
			RaycastHit t;
			//doesn't run into something
			if (Physics.Raycast (mColl, out t, .4f) == false || t.collider.gameObject.tag != "Door" || t.collider.gameObject.tag != "Cube") {
				rb.position += rb.transform.forward * Time.deltaTime * movementSpeed;
			}
         

			if (!child.GetCurrentAnimatorStateInfo (0).IsName ("Jump"))
				child.Play ("Walking");
		}

		if (Input.GetKey (KeyCode.S) && enabled) {
			rb.position -= rb.transform.forward * Time.deltaTime * movementSpeed;
			if (!child.GetCurrentAnimatorStateInfo (0).IsName ("Jump"))
				child.Play ("Walking");
		}
       

		if (!Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S) && !child.GetCurrentAnimatorStateInfo (0).IsName ("Jump")) {
			child.Play ("Idle");
		}
   

		if (Input.GetKey (KeyCode.A) && enabled) {
			rb.transform.Rotate (Vector3.down);
			rb.transform.Rotate (0, rb.GetComponent<Transform> ().rotation.y - 3.0f, 0);
       
		}

		if (Input.GetKey (KeyCode.D) && enabled) {
			rb.transform.Rotate (Vector3.up);
			rb.transform.Rotate (0, rb.GetComponent<Transform> ().rotation.y + 3.0f, 0);

		}




		if (Input.GetKeyDown (KeyCode.Space) && !child.GetCurrentAnimatorStateInfo (0).IsName ("Jump") && ground && enabled) {
			          
			jump ();
			//rb.AddForce(rb.transform.position);
            
		}
        
        
		if (inAir) {
			rb.freezeRotation = true;
			rb.transform.position += new Vector3 (0, .2f, 0);
		 
		}

		if (rb.transform.position.y > maxHeight) {
			inAir = false;
		}

		if (ground) {
			rb.freezeRotation = true;
			Physics.gravity = new Vector3 (0, -9.81f * 10, 0);
			rb.mass /= 10;
		} else {
			Physics.gravity = new Vector3 (0, -9.81f, 0);
			rb.mass = 1;
		}
        
	}

	void FixedUpdate ()
	{
        
		if (Input.GetMouseButtonUp (1)) {
			if (holding) {
			
				//Debug.Log("Holding");
				//ray = new Ray(new Vector3(0,0,0),new Vector3(0,0,0));
			}
			if (!holding) {

				// Debug.Log("No Holding");
				//ray = new Ray(transform.position + rayOffset, transform.forward * 2);
			}
		}
            
            
        

		if (Input.GetMouseButtonUp (0)) {

			if (holding) {

				letGo (); 
				//Debug.Log("Let Go");
                
			} else {

				grab ();
				// Debug.Log("Pick Up");

			}
		}
        
	}

	void grab ()
	{
		RaycastHit target;
		if (Physics.Raycast (ray, out target, 2)) {
			//Debug.Log(target.collider.tag);
			try {
				target.collider.gameObject.GetComponent<GrabBlocks> ().grabbed = true;
				if (holding) {
					ray = new Ray (transform.position + rayOffset, transform.forward * 1);
					Debug.DrawRay (transform.position + rayOffset, transform.forward * 1);
					holding = false;
				} else {
					holding = true;
					pickUp.Play ();
				}
			} catch(UnityException e){
				//e.Message;
			}
		}
	} 

	void letGo ()
	{

		GrabBlocks[] blocks = FindObjectsOfType (typeof(GrabBlocks)) as GrabBlocks[];
		foreach (GrabBlocks b in blocks) {
			ray = new Ray (transform.position + rayOffset, new Vector3 (0, 0, 0));
			Debug.DrawRay (transform.position + rayOffset, new Vector3 (0, 0, 0));
			if (b.grabbed)
				placeObject (b.gameObject);
			b.grabbed = false; 
		}
		holding = false;
		pickUp.Play ();

	}

	void placeObject (GameObject block)
	{
		Ray dropRay = new Ray (block.transform.position, Vector3.down * 2);
		RaycastHit placement;
		if (Physics.Raycast (dropRay, out placement)) {
			if (placement.collider.gameObject.tag == "grid") {
				Vector3 newPos = placement.collider.gameObject.transform.position;
				newPos.y = block.transform.position.y;
				block.transform.position = newPos;
			}
		}
	}

	void jump ()
	{
		//rb.AddForce(0, grnewLevel.Play();avity*20f, 0);
		jumpSound.Play ();
		if (Input.GetKey (KeyCode.S)) {
			transform.position += transform.forward * -.5f;
		} else {
			transform.position += transform.forward * .51f;
		}

		maxHeight = rb.transform.position.y + 1f;
		inAir = true;
		ground = false;
		child.Play ("Jump");
	}

	void OnCollisionEnter (Collision coll)
	{
		if (coll.gameObject.tag == "Stacks") {
			rb.freezeRotation = true;
			//Debug.Log("Hit");
		}
	}

	public void resetPosition(){
		rb.transform.position = originPoint;
	}
    
}
