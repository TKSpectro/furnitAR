using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.LookAt(target.localEulerAngles);
        gameObject.transform.localRotation = Quaternion.Euler(target.localEulerAngles.x, target.localEulerAngles.y, 0);
        //gameObject.transform.localRotation = (gameObject.transform.LookAt(target));
    }
}
