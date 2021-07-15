using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGrabbing : MonoBehaviour
{
    private Transform prefab;
    private GameObject spawnManager;
    private GameObject menu;
    SpawnManager spawnScript;
    bool alreadySpawned = false;
    bool outsideOfMenu = true;
    bool isHovering = false;

    // Start is called before the first frame update
    void Start()
    {
        if(transform.parent.parent.parent != null)
        {
            menu = transform.parent.parent.parent.gameObject;
            outsideOfMenu = false;
            spawnManager = GameObject.Find("SpawnManager");
            spawnScript = spawnManager.GetComponent<SpawnManager>();
            alreadySpawned = spawnScript.alreadySpawned;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnHoverEnter()
    {
        if(transform && !alreadySpawned && !outsideOfMenu)
        {
            alreadySpawned = true;
            spawnScript.alreadySpawned = true;
            isHovering = true;

            StartCoroutine(SpawnFurnitureAndHideMenu());
        }
    }

    public void OnHoverExited()
    {
        Debug.Log("hover exited");
        //isHovering = false;
    }

    IEnumerator SpawnFurnitureAndHideMenu()
    {
        yield return new WaitForSeconds(2);

        if(isHovering)
        {
            prefab = Instantiate(transform, transform.position, Quaternion.identity);
            prefab.rotation = transform.rotation;
            prefab.GetComponent<MenuGrabbing>().enabled = false;
            //prefab.gameObject.AddComponent<Microsoft.MixedReality.Toolkit.UI.ObjectManipulator>();
            prefab.gameObject.AddComponent<Microsoft.MixedReality.Toolkit.Input.NearInteractionGrabbable>();

            menu.SetActive(false);
        }
    }
}
