using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;

public class ObjectPosition : MonoBehaviour
{

    GameObject child;
    public bool hoverEntered = false;
    public bool manipulationEnded = false;
    float rotationZ, rotationY, rotationX;


    // Start is called before the first frame update
    void Start()
    {
        child = gameObject.transform.parent.GetComponent<FurnitureControl>().child;
        rotationZ = gameObject.transform.eulerAngles.z;
        rotationX = gameObject.transform.eulerAngles.x;
        //rotationY = child.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {



    }

    public void OnHover()
    {
        if (!hoverEntered)
        {

            // Dynamic calculation for the menu position  ( -0.12f of the X-Axis because the rotation axis is not in center and  )
            child.transform.position = new Vector3(transform.position.x - 0.12f, transform.position.y +
                ((transform.localScale.y / 2) + 0.3f), transform.position.z);

            // Show the menu
            child.SetActive(true);
            child.GetComponent<NearMenu>().obj = transform.gameObject;
            Debug.Log("onHoverEntered");
        }
        hoverEntered = true;

    }

    public void OnHoverExited()
    {
        if (hoverEntered)
        {
            Debug.Log("onHoverExited");
            // hide the menu after 3 second
            StartCoroutine(StopHover());
        }
    }

    IEnumerator StopHover()
    {
        yield return new WaitForSecondsRealtime(5);
        child.SetActive(false);
        Debug.Log("SetHoverToFalse");
        hoverEntered = false;
    }

    public void StartManipulation()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    public void EndManipulation()
    {

        child.SetActive(false);
        // correct the rotation of furniture  
        gameObject.transform.localRotation = Quaternion.Euler(rotationX, gameObject.transform.localRotation.y, rotationZ);
        gameObject.GetComponent<ObjectManipulator>().AllowFarManipulation = true;
        Debug.Log("onManipulationEnded");

    }


}