using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    private bool isOrtho = false;
    public Camera orthoCam;
    public GameObject player;
    public GameObject level;


    public float levelScaleX;
    public float levelScaleY;

    void Start()
    {
        orthoCam.GetComponent<Camera>().enabled = false;
        setUpOrtho();
    }

    void Update()
    {
        if (!isOrtho)
        {

            if (Input.GetKeyDown(KeyCode.O))
            {
                isOrtho = true;
                orthoCam.GetComponent<Camera>().enabled = true;

            }
        }
        else
        {
            // orthoCam.GetComponent<Transform>().transform.position = new Vector3(orthoCam.transform.position.x, orthoCam.transform.position.y, player.transform.position.z);
            if (Input.GetKeyDown(KeyCode.O))
            {
                isOrtho = false;
                orthoCam.GetComponent<Camera>().enabled = false;
            }
        }



    }


    //setUpOrtho sets up the positioning of the Ortho cam
    void setUpOrtho()
    {
        //float levelScaleX = level.GetComponent<Transform>().localScale.x;
        //float levelScaleY = level.GetComponent<Transform>().localScale.y;
        Vector3 levelPosition = level.GetComponent<Transform>().position;

        //Initially, set the ortho cam's position to the level's position
        orthoCam.GetComponent<Transform>().position = levelPosition;
        Debug.Log(orthoCam.transform.position.ToString());
        //Move the camera away from the level model based on the level's Y scale value
        //orthoCam.GetComponent<Transform>().position = new Vector3(orthoCam.transform.position.x-(levelScaleY*2), orthoCam.transform.position.y, orthoCam.transform.position.z);
        orthoCam.GetComponent<Transform>().position = new Vector3(orthoCam.transform.position.x, orthoCam.transform.position.y, orthoCam.transform.position.z + 28.0f);


    }
}
