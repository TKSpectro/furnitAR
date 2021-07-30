using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    GameObject tableMenu;
    GameObject chairMenu;
    GameObject wardrobeMenu;

    BeraterVoiceHandler voiceHandler;
    // Start is called before the first frame update
    void Start()
    {
        tableMenu = GameObject.Find("TableMenu");
        chairMenu = GameObject.Find("ChairMenu");
        wardrobeMenu = GameObject.Find("WardrobeMenu");

        voiceHandler = GameObject.Find("Berater").GetComponent<BeraterVoiceHandler>();
        //tableMenu.SetActive(false);
        //chairMenu.SetActive(false);
        //wardrobeMenu.SetActive(false);
    }

    public void ToggleTableMenu()
    {
        tableMenu.SetActive(true);
        chairMenu.SetActive(false);
        wardrobeMenu.SetActive(false);

        Transform tableTransform = CalculateMenuPosition();

        tableMenu.transform.position = tableTransform.position;
        tableMenu.transform.rotation = tableTransform.rotation;

        voiceHandler.Say(voiceHandler.menuTables);
    }
    public void ToggleChairMenu()
    {
        chairMenu.SetActive(true);
        tableMenu.SetActive(false);
        wardrobeMenu.SetActive(false);

        Transform chairTransform = CalculateMenuPosition();

        chairMenu.transform.position = chairTransform.position;
        chairMenu.transform.rotation = chairTransform.rotation;

        voiceHandler.Say(voiceHandler.menuChairs);
    }
    public void ToggleWardrobeMenu()
    {
        wardrobeMenu.SetActive(true);
        tableMenu.SetActive(false);
        chairMenu.SetActive(false);

        Transform chairTransform = CalculateMenuPosition();

        wardrobeMenu.transform.position = chairTransform.position;
        wardrobeMenu.transform.rotation = chairTransform.rotation;

        voiceHandler.Say(voiceHandler.menuWardrobes);
    }

    private Transform CalculateMenuPosition()
    {
        Transform menuTransform = Camera.main.transform;
        Quaternion menuRotation = Camera.main.transform.rotation;
        menuRotation.x = 0;
        menuRotation.z = 0;
        menuTransform.rotation = menuRotation;
        Vector3 spawnPos = Camera.main.transform.position + (menuTransform.forward * 0.5f) - (menuTransform.right * 0.3f);
        menuTransform.position = spawnPos;

        return menuTransform;
    }
}
