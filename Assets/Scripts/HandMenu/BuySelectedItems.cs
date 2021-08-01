using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(MenuHelper))]
public class BuySelectedItems : MonoBehaviour
{
    public void BuyNow()
    {
        List<GameObject> placedItems = gameObject.GetComponent<MenuHelper>().FindPlacedItems();

        BeraterVoiceHandler voiceHandler = GameObject.Find("Berater").GetComponent<BeraterVoiceHandler>();
        voiceHandler.Say(voiceHandler.finishOrder);

        OrdersHandler ordersHandler = gameObject.GetComponent<HandMenuHandler>().orders.GetComponent<OrdersHandler>();
        ordersHandler.AddItemsToOrderHistory(placedItems);

        WriteToCSV(placedItems);
    }

    void WriteToCSV(List<GameObject> items)
    {
        string filePath = getPath();

        // Create a writer and use AppendText so if the file doesnt exist already it gets created
        using (StreamWriter writer = File.AppendText(filePath))
        {
            writer.WriteLine("Type, Name, Price");
            // This loops through everything in the item list and writes given parameters
            foreach (GameObject item in items)
            {
                writer.WriteLine(item.GetType().ToString() +
                                "," + item.name +
                                "," + item.GetComponent<FurnitureAttributes>().price.ToString());
            }

            writer.WriteLine("ShoppinCartValue: " + GetComponent<MenuHelper>().CalculateShoppingCartValue(items).ToString());
            writer.WriteLine("\n");

            writer.Flush();
            // This closes the file
            writer.Close();
        }
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

