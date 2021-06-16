using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTest : MonoBehaviour
{
    public GameObject menuItems;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenMenu()
    {
        if (!menuItems.activeInHierarchy)
        {
            menuItems.SetActive(true);
            Debug.Log("Menu opened");
        }
    }

    public void CloseMenu()
    {
        if (menuItems.activeInHierarchy)
        {
            menuItems.SetActive(false);
            Debug.Log("Menu closed");
        }
    }
}
