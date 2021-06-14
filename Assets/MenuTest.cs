using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTest : MonoBehaviour
{
    public GameObject menu;
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
        if (!menu.activeInHierarchy)
        {
            menu.SetActive(true);
            Debug.Log("Menu opened");
        }
    }

    public void CloseMenu()
    {
        if (menu.activeInHierarchy)
        {
            menu.SetActive(false);
            Debug.Log("Menu closed");
        }
    }
}
