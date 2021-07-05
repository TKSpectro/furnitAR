using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;

public class ObjectPosition : MonoBehaviour
{

    GameObject child;
    public bool hoverEntered = false;
    // public bo+ol onHoverExited = false;
    public bool manipulationEnded = false;



    // Start is called before the first frame update
    void Start()
    {
        child = gameObject.transform.GetChild(0).gameObject;
        Debug.Log(child.name);

    }

    // Update is called once per frame
    void Update()
    {
        child.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.25f, gameObject.transform.position.z);
        child.transform.localRotation = Quaternion.Euler(0, 0, gameObject.transform.eulerAngles.z);
        //if (onHoverEntered)
        //{
        //}
        //if (onManipulationStarted)
        //{
        //    gameObject.GetComponent<Rigidbody>().useGravity = true;
        //    Debug.Log("onManipulationStarted");
        //    onManipulationStarted = false;
        //}
        // if (onManipulationEnded && !onHoverEntered)
        //{

        //    Debug.Log("onManipulationEnded");
        //}


    }

    public void OnHover()
    {
        if (!hoverEntered)
        {
            child.SetActive(true);
            Debug.Log("onHoverEntered");
        }
        hoverEntered = true;

    }

    public void OnHoverExited()
    {
        if (hoverEntered)
        {
            Debug.Log("onHoverExited");
            StartCoroutine(SetHoverToFalse());
        }
    }

    IEnumerator SetHoverToFalse()
    {
        yield return new WaitForSecondsRealtime(3);
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
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        gameObject.GetComponent<ObjectManipulator>().AllowFarManipulation = true;
        Debug.Log("onManipulationEnded");

    }


}