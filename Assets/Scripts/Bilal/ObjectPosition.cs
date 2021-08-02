using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;
using Microsoft.MixedReality.Toolkit.Rendering;

//[RequireComponent(typeof(NearInteractionGrabbable))]
//[RequireComponent(typeof(ConstraintManager))]
//[RequireComponent(typeof(MinMaxScaleConstraint))]
//[RequireComponent(typeof(CursorContextObjectManipulator))]

public class ObjectPosition : MonoBehaviour
{
    // Daniel Furniture Selection Menu
    //public GameObject customizableIconPrefab;
    private GameObject customizableIcon;
    private GameObject furnitureClone;
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
    GridObjectCollection gridObjectCollection;

    // Bilal near menu for manipulation
    GameObject nearMenu;
    public bool hoverEntered = false;
    public bool startManipulation = false;
    public bool manipulationEnded = false;
    float rotationZ, rotationY, rotationX;
    private GameObject ground;
    float menuOffset = 0.2f;
    float groundOffset = 0.01f;
    private ManipulationHandFlags Nothing;
    private bool alreadyResized = false;

    Vector3 initalScale;



    // Start is called before the first frame update
    void Start()
    {
        furnitures = GameObject.Find("Furnitures");
        // Daniel Furniture Selection Menu

        initalScale = transform.localScale;

        objectManipulator = gameObject.GetComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>();
        if (transform.parent.name != "Furnitures")
        {
            menu = transform.parent.parent.parent.gameObject;
            outsideOfMenu = false;
            spawnManager = GameObject.Find("SpawnManager");
            objectManipulator.manipulationType = Nothing;
            scrollingManager = menu.GetComponent<ScrollingManager>();
            FurnitureAttributes fa = gameObject.GetComponent<FurnitureAttributes>();
            gridObjectCollection = transform.parent.GetComponent<GridObjectCollection>();
            IEnumerator coroutine = AddCustomizableIcon();
            StartCoroutine(coroutine);
        }
        else
        {
            nearMenu = gameObject.transform.parent.GetComponent<FurnitureControl>().child;
            rotationY = gameObject.transform.eulerAngles.y;
            rotationX = gameObject.transform.eulerAngles.x;
            objectManipulator.manipulationType = ManipulationHandFlags.OneHanded | ManipulationHandFlags.TwoHanded;
            gameObject.EnsureComponent<BoxCollider>().enabled = true;
            Destroy(transform.GetChild(0).gameObject.GetComponent<MaterialInstance>());
            ground = GameObject.Find("Ground");
            gameObject.GetComponent<BoundsControl>().enabled = true;
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

    }

    public void OnHover()
    {
        // Daniel Furniture Selection Menu
        if (transform && !alreadySpawned && !outsideOfMenu && !hasMomentum)
        {

            isHovering = true;

            StartCoroutine(SpawnFurnitureAndHideMenu());
        }
        // Bilal near menu for manipulation
        if (isClone)
        {
            if (!hoverEntered)
            {

                float height = GetComponent<Collider>().bounds.size.y;
                Debug.Log("nearmenu: " + nearMenu);
                nearMenu.transform.position = new Vector3(transform.position.x - 0.12f, transform.position.y + height + menuOffset, transform.position.z);

                // Show the menu
                if (gameObject.GetComponent<MeshRenderer>())
                {
                    nearMenu.GetComponent<NearMenu>().x.SetActive(true);
                }
                else
                {
                    nearMenu.GetComponent<NearMenu>().x.SetActive(false);
                }
                nearMenu.SetActive(true);
                nearMenu.GetComponent<NearMenu>().SetFurniture(gameObject);

                Debug.Log("onHoverEntered");
            }
            hoverEntered = true;
        }
    }

    public void OnHoverExited()
    {
        if (hoverEntered)
        {
            // hide the menu after 3 second
            StartCoroutine(StopHover());
        }
    }

    IEnumerator StopHover()
    {
        yield return new WaitForSecondsRealtime(4);
        nearMenu.SetActive(false);
        nearMenu.GetComponent<NearMenu>().newItem.SetActive(false);
        hoverEntered = false;
    }

    // Daniel Furniture Selection Menu
    IEnumerator SpawnFurnitureAndHideMenu()
    {
        yield return new WaitForSeconds(2);

        if (isHovering && !isClone && !hasMomentum)
        {
            alreadySpawned = true;
            furnitureClone = Instantiate(gameObject, transform.position, Quaternion.identity);
            furnitureClone.transform.parent = furnitures.transform;
            if (transform.Find("FurnitureInfo"))
            {
                furnitureClone.transform.Find("FurnitureInfo").gameObject.SetActive(false);
            }

            furnitureClone.transform.rotation = transform.rotation;
            furnitureClone.gameObject.AddComponent<Microsoft.MixedReality.Toolkit.Input.NearInteractionGrabbable>();

            menu.SetActive(false);
            alreadySpawned = false;
        }
    }

    public void EndManipulation()
    {

        nearMenu.SetActive(false);
        StartCoroutine(SetOfGround());
    }

    IEnumerator SetOfGround()
    {

        float height = ground.GetComponent<MeshCollider>().bounds.size.y;
        yield return new WaitForSecondsRealtime(2);
        // correct the rotation of furniture  
        gameObject.transform.eulerAngles = new Vector3(rotationX, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);

        // set furniture piece of the ground
        transform.position = new Vector3(transform.position.x, height + groundOffset, transform.position.z);
        if (!alreadyResized)
        {
            transform.localScale = new Vector3(initalScale.x * 8f, initalScale.y * 8f, initalScale.z * 8f);
            alreadyResized = true;
        }

    }

    private IEnumerator AddCustomizableIcon()
    {
        yield return new WaitForSeconds(0.01f);
        if (transform.Find("FurnitureInfo"))
        {
            string price = gameObject.GetComponent<FurnitureAttributes>().price.ToString();
            transform.Find("FurnitureInfo").transform.Find("Price").GetComponent<TextMeshPro>().text = price + ".00€";

            if (transform.childCount < 2)
            {
                transform.Find("FurnitureInfo").transform.Find("CustomizableIcon").gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
    }
}