using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BuySelectedItems : MonoBehaviour
{
    private void Start()
    {
        BuyNow();
    }

    public void BuyNow()
    {
        List<GameObject> placedItems = FindPlacedItems();
        WriteToCSV(placedItems);
    }

    List<GameObject> FindPlacedItems()
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

    void WriteToCSV(List<GameObject> items)
    {
        string filePath = getPath();

        //This is the writer, it writes to the filepath
        StreamWriter writer = new StreamWriter(filePath);

        //This is writing the line of the type, name, damage... etc... (I set these)
        writer.WriteLine("Type,Name");
        //This loops through everything in the inventory and sets the file to these.
        foreach (var item in items)
        {
            writer.WriteLine(item.GetType().ToString() +
                            "," + item.name);
        }

        writer.Flush();
        //This closes the file
        writer.Close();
    }

    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + "BoughtItems.csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+"BoughtItems.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"BoughtItems.csv";
#else
        return Application.dataPath +"/"+"BoughtItems.csv";
#endif
    }
}

