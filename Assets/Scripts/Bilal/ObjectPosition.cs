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
    // Daniel Furniture Selection Menu
    private Transform prefab;
    private GameObject spawnManager;
    private GameObject menu;
    bool alreadySpawned = false;
    bool outsideOfMenu = true;
    bool isHovering = false;
    private GameObject furnitures;
    private bool isClone = false;

    // Bilal near menu for manipulation
    GameObject nearMenu;
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
        furnitures = GameObject.Find("Furnitures");
        // Daniel Furniture Selection Menu
        Debug.Log("parent");
        Debug.Log("parent name: " + transform.parent.name);
        if (transform.parent.name != "Furnitures")
        {
            menu = transform.parent.parent.parent.gameObject;
            outsideOfMenu = false;
            spawnManager = GameObject.Find("SpawnManager");
        }
        else
        {
            nearMenu = gameObject.transform.parent.GetComponent<FurnitureControl>().child;
            rotationY = gameObject.transform.eulerAngles.y;
            rotationX = gameObject.transform.eulerAngles.x;
            ground = GameObject.Find("Ground");
            isClone = true;
        }

        // Bilal near menu for manipulation

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnHover()
    {
        Debug.Log("hover started");
        // Daniel Furniture Selection Menu
            if (transform && !alreadySpawned && !outsideOfMenu)
            {
                alreadySpawned = true;
                isHovering = true;

                StartCoroutine(SpawnFurnitureAndHideMenu());
            }
        // Bilal near menu for manipulation
        //else
        //{
        //    if (!hoverEntered)
        //    {
        //
        //        float height = GetComponent<Collider>().bounds.size.y;
        //        nearMenu.transform.position = new Vector3(transform.position.x - 0.12f, height + menuOffset, transform.position.z);
        //
        //        // Show the menu
        //        nearMenu.SetActive(true);
        //        nearMenu.GetComponent<NearMenu>().SetFurniture(transform.gameObject);
        //
        //        Debug.Log("onHoverEntered");
        //    }
        //    hoverEntered = true;
        //}
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
        nearMenu.SetActive(false);
        Debug.Log("SetHoverToFalse");
        hoverEntered = false;
    }

    // Daniel Furniture Selection Menu
    IEnumerator SpawnFurnitureAndHideMenu()
    {
        yield return new WaitForSeconds(2);

        if (isHovering && !isClone)
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
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

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

    }


}