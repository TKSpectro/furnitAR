using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveAllChildsARandomPrice : MonoBehaviour
{
    [SerializeField]
    bool setNumeratedName;

    public void GeneratePrices(int min, int max)
    {
        GameObject placedItemsParent = GameObject.Find("PlacedItems");

        // Run through the childs of the parent and all the objects to our list
        for (int i = 0; i < placedItemsParent.transform.childCount; ++i)
        {
            if (setNumeratedName)
            {
                placedItemsParent.transform.GetChild(i).gameObject.name = "PlacedItem" + i;
            }
            placedItemsParent.transform.GetChild(i).gameObject.AddComponent<FurnitureAttributes>().price = Random.Range(min, max);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GeneratePrices(50, 200);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
