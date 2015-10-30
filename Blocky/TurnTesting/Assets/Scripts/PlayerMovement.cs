using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 5.0f;
    //public float tSpeed = 50.0f;
    
    //Ray myRay;
    public GameObject c;
    public Rigidbody rb; 
    public Animator child;
    public Animation a;
    
    Ray ray;

    Vector3 rayOffset, blockOffset;

    private bool jumping = false;
    private bool inAir = false;
    private bool ground = false;
    private bool increaseHeight = false;

    private float maxHeight;
    public bool holding = false;

    const float gravity = 9.81f;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        child = GetComponent<Animator>();
        rayOffset = new Vector3(0, .5f, 0);
        blockOffset = new Vector3(0, 1.5f, 0);
       
    }

    // Update is called once per frame
    void Update()
    {

        ray = new Ray(transform.position + rayOffset, transform.forward * 1);
        Debug.DrawRay(transform.position + rayOffset, transform.forward * 1);

        if (Input.GetKey(KeyCode.W))
        {
            rb.position += rb.transform.forward * Time.deltaTime * movementSpeed;
            if (!child.GetCurrentAnimatorStateInfo(0).IsName("Jump")) child.Play("Walking");
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.position -= rb.transform.forward * Time.deltaTime * movementSpeed;
            if(!child.GetCurrentAnimatorStateInfo(0).IsName("Jump")) child.Play("Walking");
        }
       

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !child.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            child.Play("Idle");
        }
   

        if (Input.GetKey(KeyCode.A))
        {
            rb.transform.Rotate(Vector3.down);
            rb.transform.Rotate(0,rb.GetComponent<Transform>().rotation.y - 3.0f,0);
       
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.transform.Rotate(Vector3.up);
            rb.transform.Rotate(0, rb.GetComponent<Transform>().rotation.y + 3.0f, 0);

        }

    

        
        if (Input.GetKeyDown(KeyCode.Space)){

            jumping = true;
            child.Play("Jump");
            jump();
			//rb.AddForce(rb.transform.position);
            rb.transform.position += new Vector3(0, .2f, 0);
            //rb.GetComponent<Transform>().position.y = 3.0f;
            //rb.AddForce(Vector3.up);
            //jumping = false;
            
        }
         

		if(Input.GetKeyUp(KeyCode.Space))
		{
	        jumping = false;
			//child.Play("Walking");
		}
        
        
        if(inAir)
        {
            rb.freezeRotation = true;
            rb.transform.position += new Vector3(0, .2f, 0); 
            
            
        }

        if (rb.transform.position.y > maxHeight)
        {
            inAir = false;
        }

        if (ground)
        {
            rb.freezeRotation = true;
        }
        
    }

    void FixedUpdate()
    {
        
        if (Input.GetMouseButtonUp(1))
        {
            if (holding)
			{
			
                Debug.Log("Holding");
			    //ray = new Ray(new Vector3(0,0,0),new Vector3(0,0,0));
			}
            if(!holding)
			{

                Debug.Log("No Holding");
				//ray = new Ray(transform.position + rayOffset, transform.forward * 2);
			}
        }
            
            
        

        if(Input.GetMouseButtonUp(0)){

            if (holding)
            {

                letGo(); 
                Debug.Log("Let Go");
                
            }
            else
            {

                grab();
                Debug.Log("Pick Up");

            }
        }
        
    }

    void grab()
    {
        RaycastHit target;
        if(Physics.Raycast(ray, out target, 2)){
            Debug.Log(target.collider.tag);
            target.collider.gameObject.GetComponent<GrabBlocks>().grabbed = true;
            if (holding)
            {
				ray = new Ray(transform.position + rayOffset, transform.forward * 1);
				Debug.DrawRay(transform.position + rayOffset, transform.forward * 1);
                holding = false;
            }
            else
            {
                holding = true;
            }
        }
    }

    void letGo()
    {
        GrabBlocks[] blocks = FindObjectsOfType(typeof(GrabBlocks)) as GrabBlocks[];
        foreach (GrabBlocks b in blocks)
        {
			ray = new Ray(transform.position + rayOffset, new Vector3(0,0,0));
			Debug.DrawRay(transform.position + rayOffset, new Vector3(0,0,0));
            b.grabbed = false; 
        }
        holding = false;
    }

    void jump()
    {
        //rb.AddForce(0, gravity*20f, 0);
        maxHeight = rb.transform.position.y + 1;
        inAir = true;
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Stacks"){
            rb.freezeRotation = true;
            Debug.Log("Hit");
        }
    }
    
}
