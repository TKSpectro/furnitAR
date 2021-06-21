using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTest : MonoBehaviour
{
    public GameObject menuItems;
    public OVRHand leftHand;

    bool menuWasOpen = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!leftHand.IsTracked)
        {
            menuItems.SetActive(false);
        }
        else
        {
            if (!menuItems.activeInHierarchy && menuWasOpen)
            {
                menuItems.SetActive(true);
            }
        }
    }

    public void OpenMenu()
    {
        if (!menuItems.activeInHierarchy)
        {
            menuItems.SetActive(true);
            menuWasOpen = true;
            Debug.Log("Menu opened");
        }
    }

    public void CloseMenu()
    {
        if (menuItems.activeInHierarchy)
        {
            menuItems.SetActive(false);
            menuWasOpen = false;
            Debug.Log("Menu closed");
        }
    }
}
