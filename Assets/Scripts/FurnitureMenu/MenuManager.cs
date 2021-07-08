using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    GameObject tableMenu;
    GameObject chairMenu;
    GameObject wardrobeMenu;
    bool showTableMenu = false;
    bool showChairMenu = false;
    bool showWardrobeMenu = false;
    // Start is called before the first frame update
    void Start()
    {
        tableMenu = GameObject.Find("TableMenu");
        chairMenu = GameObject.Find("ChairMenu");
        wardrobeMenu = GameObject.Find("WardrobeMenu");
        tableMenu.SetActive(showTableMenu);
        chairMenu.SetActive(showChairMenu);
        wardrobeMenu.SetActive(showWardrobeMenu);
        Debug.Log("table menu:" + showTableMenu + "object: " + tableMenu);
    }

    public void ToggleTableMenu()
    {
        showTableMenu = !showTableMenu;
        Debug.Log("table menu:" + showTableMenu + "object: " + tableMenu);
        tableMenu.SetActive(showTableMenu);

        if(showTableMenu)
        {
            if (showChairMenu)
            {
                showChairMenu = !showChairMenu;
                chairMenu.SetActive(false);
            }
            if (showWardrobeMenu)
            {
                showWardrobeMenu = !showWardrobeMenu;
                wardrobeMenu.SetActive(false);
            }
        }

    }
    public void ToggleChairMenu()
    {
        showChairMenu = !showChairMenu;
        chairMenu.SetActive(showChairMenu);

        if (showChairMenu)
        {
            if (showTableMenu)
            {
                showTableMenu = !showTableMenu;
                tableMenu.SetActive(false);
            }
            if (showWardrobeMenu)
            {
                showWardrobeMenu = !showWardrobeMenu;
                wardrobeMenu.SetActive(false);
            }
        }
    }
    public void ToggleWardrobeMenu()
    {
        showWardrobeMenu = !showWardrobeMenu;
        wardrobeMenu.SetActive(showWardrobeMenu);

        if (showWardrobeMenu)
        {
            if (showTableMenu)
            {
                showTableMenu = !showTableMenu;
                tableMenu.SetActive(false);
            }
            if (showChairMenu)
            {
                showChairMenu = !showChairMenu;
                chairMenu.SetActive(false);
            }
        }
    }
}
