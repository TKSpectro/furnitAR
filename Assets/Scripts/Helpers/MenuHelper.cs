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

        // Run through the childs of the parent and all the objects to our list
        for (int i = 0; i < placedItemsParent.transform.childCount; ++i)
        {
            items.Add(placedItemsParent.transform.GetChild(i).gameObject);
        }

        return items;
    }
}
