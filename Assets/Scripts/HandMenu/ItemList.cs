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

    public List<GameObject> UpdateItemList()
    {
        GridObjectCollection gridObjectCollection = itemList.GetComponent<GridObjectCollection>();

        // Clear the item list before reusing it
        foreach (Transform child in itemList.transform)
        {
            Destroy(child.gameObject);
        }

        List<GameObject> placedItems = gameObject.GetComponent<MenuHelper>().FindPlacedItems();
        foreach (GameObject item in placedItems)
        {
            // Instantiate the itemPrefab
            GameObject newItem = Instantiate(itemPrefab, itemList.transform);

            newItem.transform.Find("Name").GetComponent<TextMeshPro>().text = item.name;
            newItem.transform.Find("Price").GetComponent<TextMeshPro>().text = item.GetComponent<FurnitureAttributes>().price.ToString() + ".00€";
            // Create a minified version of the actual object as a icon
            GameObject model = Instantiate(item, newItem.transform.Find("Model"));

            // Reset position and rotation because we duplicate the actual model in the scene
            // Dont reset rotation because the furniture prefabs are rotated randomly, so we need to keep the rotations
            // model.transform.localRotation = new Quaternion();
            model.transform.localPosition = new Vector3();

            // Set the itemList as its parent
            newItem.SetActive(true);
        }

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
