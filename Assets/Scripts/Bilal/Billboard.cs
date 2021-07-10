using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    //public Transform target;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Billboard follow the camera postition
        gameObject.transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward,
            camera.transform.rotation * Vector3.up);

        //transform.LookAt(target.position);
        gameObject.transform.localRotation = Quaternion.Euler(0, gameObject.transform.localEulerAngles.y, 0);

    }
}
