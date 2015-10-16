using UnityEngine;
using System.Collections;

public class GrabBlocks : MonoBehaviour {

    public bool holding = false;
    public GameObject grabbedBlock;
    public GameObject p;
    Ray myRay;
    private Vector3 rayOffset;
    private Vector3 blockOffset;
    private float distance = 2.0f;

	// Use this for initialization
	void Start () {
        myRay = new Ray(p.transform.position, p.transform.forward);
        rayOffset = new Vector3(0,1,0);
        blockOffset = new Vector3(0, 2, 0);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(p.transform.position + rayOffset, p.transform.forward * distance);

	   // if(Input.GetKey(KeyCode.Space)){
            grab();
       // }
	}

    void grab()
    {
        if (holding)
        {
            grabbedBlock.transform.position = p.transform.position + blockOffset;
        }
    }

}
