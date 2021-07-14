using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;

[RequireComponent(typeof(NearInteractionGrabbable))]
[RequireComponent(typeof(ConstraintManager))]
[RequireComponent(typeof(MinMaxScaleConstraint))]
[RequireComponent(typeof(CursorContextObjectManipulator))]

public class ObjectPosition : MonoBehaviour
{

    GameObject menu;
    public bool hoverEntered = false;
    public bool startManipulation = false;
    public bool manipulationEnded = false;
    float rotationZ, rotationY, rotationX;
    private GameObject ground;
    float menuOffset = 0.4f;
    float groundOffset = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        ground = GameObject.Find("Ground");
        menu = gameObject.transform.parent.GetComponent<FurnitureControl>().child;
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

            float height = GetComponent<Collider>().bounds.size.y;
            menu.transform.position = new Vector3(transform.position.x - 0.12f, height + menuOffset, transform.position.z);

            // Show the menu
            menu.SetActive(true);
            menu.GetComponent<NearMenu>().SetFurniture(transform.gameObject);

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
        menu.SetActive(false);
        Debug.Log("SetHoverToFalse");
        hoverEntered = false;
    }

    public void StartManipulation()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

        Debug.Log("StartManipulation");

    }

    public void EndManipulation()
    {

        menu.SetActive(false);
        StartCoroutine(SetOfGround());
        gameObject.GetComponent<ObjectManipulator>().AllowFarManipulation = true;
        Debug.Log("EndManipulation");

    }
    IEnumerator SetOfGround()
    {

        float height = ground.GetComponent<Collider>().bounds.size.y;
        yield return new WaitForSecondsRealtime(2);
        // correct the rotation of furniture  
        gameObject.transform.eulerAngles = new Vector3(rotationX, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);

        // set furniture piece of the ground
        transform.position = new Vector3(transform.position.x, height + groundOffset, transform.position.z);

    }


}