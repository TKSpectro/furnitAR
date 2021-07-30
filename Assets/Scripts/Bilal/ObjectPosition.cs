using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Utilities;

//[RequireComponent(typeof(NearInteractionGrabbable))]
//[RequireComponent(typeof(ConstraintManager))]
//[RequireComponent(typeof(MinMaxScaleConstraint))]
//[RequireComponent(typeof(CursorContextObjectManipulator))]

public class ObjectPosition : MonoBehaviour
{
    // Daniel Furniture Selection Menu
    private Transform prefab;
    private GameObject spawnManager;
    private GameObject menu;
    bool alreadySpawned = false;
    bool outsideOfMenu = true;
    bool isHovering = false;
    private GameObject furnitures;
    private bool isClone = false;
    ObjectManipulator objectManipulator;
    ScrollingManager scrollingManager;
    private bool hasMomentum = false;

    // Bilal near menu for manipulation
    GameObject nearMenu;
    public bool hoverEntered = false;
    public bool startManipulation = false;
    public bool manipulationEnded = false;
    float rotationZ, rotationY, rotationX;
    private GameObject ground;
    float menuOffset = 0.2f;
    float groundOffset = 0.1f;
    private ManipulationHandFlags Nothing;
    private bool alreadyResized = false;


    // Start is called before the first frame update
    void Start()
    {
        furnitures = GameObject.Find("Furnitures");
        // Daniel Furniture Selection Menu

        Debug.Log("parent");
        Debug.Log("parent name: " + transform.parent.name);

        objectManipulator = gameObject.GetComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>();
        if (transform.parent.name != "Furnitures")
        {
            menu = transform.parent.parent.parent.gameObject;
            outsideOfMenu = false;
            spawnManager = GameObject.Find("SpawnManager");
            objectManipulator.manipulationType = Nothing;
            scrollingManager = menu.GetComponent<ScrollingManager>();
        }
        else
        {
            nearMenu = gameObject.transform.parent.GetComponent<FurnitureControl>().child;
            rotationY = gameObject.transform.eulerAngles.y;
            rotationX = gameObject.transform.eulerAngles.x;
            objectManipulator.manipulationType = ManipulationHandFlags.OneHanded | ManipulationHandFlags.TwoHanded;
            gameObject.EnsureComponent<Rigidbody>();
            gameObject.GetComponent<Rigidbody>().mass = 0.001f;
            gameObject.GetComponent<Rigidbody>().drag = 1f;
            gameObject.GetComponent<Rigidbody>().angularDrag = 1f;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            ground = GameObject.Find("Ground");
            isClone = true;
        }

        // Bilal near menu for manipulation

    }

    // Update is called once per frame
    void Update()
    {
        if (!isClone)
        {
            hasMomentum = scrollingManager.hasMomentum;
        }
        //Debug.Log("has momentum: " + scrollingManager.hasMomentum);

    }

    public void OnHover()
    {
        Debug.Log("hover started");
        // Daniel Furniture Selection Menu
        if (transform && !alreadySpawned && !outsideOfMenu && !hasMomentum)
        {
            alreadySpawned = true;
            isHovering = true;

            StartCoroutine(SpawnFurnitureAndHideMenu());
        }
        // Bilal near menu for manipulation
        if (isClone)
        {
            Debug.Log("alreadySpawned= " + alreadySpawned);
            Debug.Log("outsideOfMenu= " + outsideOfMenu);
            Debug.Log("hasMomentum= " + hasMomentum);

            if (!hoverEntered)
            {

                float height = GetComponent<Collider>().bounds.size.y;
                Debug.Log("nearmenu: " + nearMenu);
                nearMenu.transform.position = new Vector3(transform.position.x - 0.12f, transform.position.y + height + menuOffset, transform.position.z);

                // Show the menu
                nearMenu.SetActive(true);
                nearMenu.GetComponent<NearMenu>().SetFurniture(transform.gameObject);

                Debug.Log("onHoverEntered");
            }
            hoverEntered = true;
        }
    }

    public void OnHoverExited()
    {
        Debug.Log("onHoverExited");
        if (hoverEntered)
        {
            // hide the menu after 3 second
            StartCoroutine(StopHover());
        }
    }

    IEnumerator StopHover()
    {
        yield return new WaitForSecondsRealtime(6);
        nearMenu.SetActive(false);
        nearMenu.GetComponent<NearMenu>().newItem.SetActive(false);
        Debug.Log("SetHoverToFalse");
        hoverEntered = false;
    }

    // Daniel Furniture Selection Menu
    IEnumerator SpawnFurnitureAndHideMenu()
    {
        yield return new WaitForSeconds(2);

        if (isHovering && !isClone && !hasMomentum)
        {
            prefab = Instantiate(transform, transform.position, Quaternion.identity);
            prefab.transform.parent = furnitures.transform;

            prefab.rotation = transform.rotation;
            //prefab.GetComponent<ObjectPosition>().enabled = false;
            //prefab.gameObject.AddComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>();
            prefab.gameObject.AddComponent<Microsoft.MixedReality.Toolkit.Input.NearInteractionGrabbable>();

            menu.SetActive(false);
        }
    }

    public void StartManipulation()
    {
        if (outsideOfMenu)
        {

        }

        Debug.Log("StartManipulation");

    }

    public void EndManipulation()
    {

        nearMenu.SetActive(false);
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
        if (!alreadyResized)
        {
            //transform.localScale = new Vector3(transform.localScale.x * 10f, transform.localScale.y * 10f, transform.localScale.z * 10f);
            alreadyResized = true;
        }

    }


}