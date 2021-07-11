using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;

[RequireComponent(typeof(NearInteractionGrabbable))]
[RequireComponent(typeof(ConstraintManager))]
[RequireComponent(typeof(MinMaxScaleConstraint))]
[RequireComponent(typeof(CursorContextObjectManipulator))]
[RequireComponent(typeof(RotationAxisConstraint))]

public class ObjectPosition : MonoBehaviour
{

    GameObject child;
    public bool hoverEntered = false;
    public bool startManipulation = false;
    public bool manipulationEnded = false;
    float rotationZ, rotationY, rotationX;


    // Start is called before the first frame update
    void Start()
    {
        child = gameObject.transform.parent.GetComponent<FurnitureControl>().child;
        rotationY = gameObject.transform.eulerAngles.y;
        rotationX = gameObject.transform.eulerAngles.x;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnHover()
    {
        if (!hoverEntered)
        {
            // Different asset libraries different scale unit
            if (transform.localScale.y < 1)
            {
                // Dynamic calculation for the menu position  ( -0.12f of the X-Axis because the rotation axis is not in center and  )
                child.transform.position = new Vector3(transform.position.x - 0.12f, transform.position.y +
                    ((transform.localScale.y / 2) + 0.3f), transform.position.z);
            }
            else
            {
                child.transform.position = new Vector3((transform.position.x) - 0.12f, (transform.position.y) +
                (((transform.localScale.y / 10) / 2) + 0.3f), transform.position.z);
            }
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
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

        Debug.Log("StartManipulation");

        if (transform.position.y == 0.0f)
        {
            transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);

        }

    }

    public void EndManipulation()
    {

        child.SetActive(false);
        // correct the rotation of furniture  
        gameObject.transform.eulerAngles = new Vector3(rotationX, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
        //gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z);
        StartCoroutine(SetOfGround());

        gameObject.GetComponent<ObjectManipulator>().AllowFarManipulation = true;
        Debug.Log("EndManipulation");

    }
    IEnumerator SetOfGround()
    {
        if (!startManipulation)
        {
            //float PositionY = transform.position.y;
            yield return new WaitForSecondsRealtime(2);
            transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);

        }
        //gameObject.transform.eulerAngles = new Vector3(rotationX, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);
        startManipulation = true;
        //transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);

    }


}