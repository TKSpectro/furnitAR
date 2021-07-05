using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(MenuHelper))]
public class BuySelectedItems : MonoBehaviour
{
    private void Start()
    {
        BuyNow();
    }

    public void BuyNow()
    {
        List<GameObject> placedItems = gameObject.GetComponent<MenuHelper>().FindPlacedItems();
        WriteToCSV(placedItems);
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

