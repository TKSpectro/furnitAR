using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Utilities;

[RequireComponent(typeof(MenuHelper))]
public class ItemList : MonoBehaviour
{
    public GameObject itemPrefab;
    [SerializeField]
    public GameObject itemList;
    public bool lul = false;

    private void Start()
    {
        UpdateItemList();
    }

    private void Update()
    {
        if (lul)
        {
            UpdateItemList();
            lul = false;
        }
    }

    public List<GameObject> UpdateItemList()
    {
        GridObjectCollection gridObjectCollection = itemList.GetComponent<GridObjectCollection>();

        // Clear the item list before reusing it
        foreach (Transform child in itemList.transform)
        {
            Destroy(child.gameObject);
        }

        // gridObjectCollection.UpdateCollection();

        List<GameObject> placedItems = gameObject.GetComponent<MenuHelper>().FindPlacedItems();
        foreach (GameObject item in placedItems)
        {
            // Instantiate the itemPrefab
            GameObject newItem = Instantiate(itemPrefab, itemList.transform);

            newItem.transform.GetChild(1).GetComponent<TextMeshPro>().text = item.name;
            newItem.transform.GetChild(2).GetComponent<TextMeshPro>().text = item.GetComponent<FurnitureAttributes>().price.ToString() + ".00€";

            // Set the itemList as its parent
            // newItem.transform.parent = itemList.transform;
            newItem.SetActive(true);
        }

        //Vector3 newPos = new Vector3(0.0f, -0.03f, 0.0f);
        //foreach (Transform child in itemList.transform)
        //{
        //    child.position.Set(newPos.x, newPos.y, newPos.z);
        //    newPos.y += 0.06f;
        //}

        // BaseObjectCollection baseObjectCollection = gridObjectCollection;
        // baseObjectCollection.UpdateCollection();
        // gridObjectCollection.UpdateCollection();

        IEnumerator coroutine = CustomUpdateCollection(gridObjectCollection);
        StartCoroutine(coroutine);

        return placedItems;
    }

    // Appearently we need to call UpdateCollection on a GridObjectCollection async even if its not documented to be async
    private IEnumerator CustomUpdateCollection(GridObjectCollection gridObjectCollection)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            gridObjectCollection.UpdateCollection();
        }
    }
}
