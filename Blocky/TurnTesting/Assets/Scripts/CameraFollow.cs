using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float speed;
    
    Vector3 offset = new Vector3(0,6,-2);
    CharacterController charController;
    

    void Start()
    {
        SetCameraTarget(target);
    }

    void SetCameraTarget(Transform t)
    {
        target = t;
    }

    void Update()
    {

        testOne();

    }

    void testOne()
    {
        transform.position = target.transform.position + offset;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, speed);
        //transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * speed);
        //Vector3 targetPosition = playerPos.TransformPoint(new Vector3(horizontalBuffer, followDistance, verticalBuffer));
        //transform.position = Vector3.SmoothDamp(transform.position, playerPos.position, ref velocity, smoothTime);
    }

    void testTwo()
    {

    }

    

}
