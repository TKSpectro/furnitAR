using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHelper : MonoBehaviour
{
    // Returns a list of all the items placed by the player in the scene
    public List<GameObject> FindPlacedItems()
    {
        List<GameObject> items = new List<GameObject>();

        // Find the parent item where all placed items should be placed
        GameObject placedItemsParent = GameObject.Find("PlacedItems");

        // Run through the childs of the parent and add all the objects the list
        foreach (Transform child in placedItemsParent.transform)
        {
            items.Add(child.gameObject);
        }

        return items;
    }

    public int CalculateShoppingCartValue(List<GameObject> placedItems)
    {
        int completePrice = 0;

        foreach (GameObject item in placedItems)
        {
            completePrice += item.GetComponent<FurnitureAttributes>().price;
        }

        return completePrice;
    }
}
