using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 5.0f;
    public float tSpeed = 50.0f;
    public bool rotating = false;
    public bool holding = false;
    public GameObject c;
    public Rigidbody rb; 
    public Animator child;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        child = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            rb.position += rb.transform.forward * Time.deltaTime * movementSpeed;
            child.Play("Walking");
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.position -= rb.transform.forward * Time.deltaTime * movementSpeed;
            child.Play("Walking");
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            //child.Play("Walking");
        }else{
            //child.Play("Idle");
        }
   

        if (Input.GetKey(KeyCode.A))
        {
            rb.transform.Rotate(Vector3.down);
           rb.transform.Rotate(0,rb.GetComponent<Transform>().rotation.y - 1.0f,0);
       
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.transform.Rotate(Vector3.up);
            rb.transform.Rotate(0, rb.GetComponent<Transform>().rotation.y + 1.0f, 0);

        }

        if (Input.GetKey(KeyCode.Space)){
            child.Play("Jump");
            rb.AddForce(Vector3.up);
        }


    }
}
