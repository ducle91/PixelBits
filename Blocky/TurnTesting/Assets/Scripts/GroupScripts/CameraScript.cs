using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public GameObject player;
    private float camPosX = 30;
    private bool isOrtho = false;

	// Update is called once per frame
	void Update ()
    {
        if (isOrtho)
        {
            Camera.main.transform.position = new Vector3(player.transform.position.x-10.0f, player.transform.position.y, player.transform.position.z+5.0f);
            Camera.main.transform.rotation = Quaternion.Euler(new Vector3(0, 90f, 0));

            if (Input.GetKeyDown(KeyCode.O))
                isOrtho = false;

        }
        else
        {
            //Positions the camera above and behind the player object
            Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.0f, player.transform.position.z - 1.0f);

            //Rotates the camera according to the rotation values of the player object
            Camera.main.transform.rotation = Quaternion.Euler(new Vector3(camPosX, player.transform.eulerAngles.y, 0));

            //Press "O" to go into Orthographic View
            if (Input.GetKeyDown(KeyCode.O))
                isOrtho = true;
        }
    }
}
