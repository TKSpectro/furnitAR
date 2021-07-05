using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Utilities;

[RequireComponent(typeof(MenuHelper))]
public class ItemList : MonoBehaviour
{
    public GameObject itemPrefab;

    private void Start()
    {
        UpdateItemList();
    }

    public void UpdateItemList()
    {
        GameObject itemList = GameObject.Find("ItemList").gameObject;

        // Clear the item list before reusing it
        foreach (Transform child in itemList.transform)
        {
            Destroy(child.gameObject);
        }

        List<GameObject> placedItems = gameObject.GetComponent<MenuHelper>().FindPlacedItems();
        foreach (GameObject item in placedItems)
        {
            // Instantiate the itemPrefab
            GameObject newItem = Instantiate(itemPrefab);

            newItem.transform.GetChild(1).GetComponent<TextMeshPro>().text = item.name;
            newItem.transform.GetChild(2).GetComponent<TextMeshPro>().text = item.GetComponent<FurnitureAttributes>().price.ToString() + ".00€";

            // Set the itemList as its parent
            newItem.transform.parent = itemList.transform;

        }

        itemList.GetComponent<GridObjectCollection>().UpdateCollection();
    }
}
