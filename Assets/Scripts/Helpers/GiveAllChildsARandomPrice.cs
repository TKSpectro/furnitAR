using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveAllChildsARandomPrice : MonoBehaviour
{
    [SerializeField]
    bool setNumeratedName;

    [SerializeField]
    int rangeMin = 10;
    [SerializeField]
    int rangeMax = 100;

    public void GeneratePrices(int min, int max)
    {
        int i = 1;

        // Run through the childs of the gameobject
        foreach (Transform child in gameObject.transform)
        {
            if (setNumeratedName)
            {
                child.gameObject.name = "PlacedItem " + i;
                ++i;
            }
            child.gameObject.AddComponent<FurnitureAttributes>().price = Random.Range(min, max);
        }
    }

    void Start()
    {
        GeneratePrices(rangeMin, rangeMax);
    }
}
