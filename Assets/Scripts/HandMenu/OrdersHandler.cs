using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Rendering;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrdersHandler : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> ordersItems;

    [SerializeField]
    GameObject itemPrefab;

    [SerializeField]
    GameObject itemList;

    public void AddItemsToOrderHistory(List<GameObject> items)
    {

        foreach (GameObject item in items)
        {
            ordersItems.Add(item);
            GameObject newItem = Instantiate(itemPrefab, itemList.transform);

            newItem.transform.Find("Name").GetComponent<TextMeshPro>().text = item.name;
            newItem.transform.Find("Price").GetComponent<TextMeshPro>().text = item.GetComponent<FurnitureAttributes>().price.ToString() + ".00€";
            // Create a minified version of the actual object as a icon
            GameObject model = Instantiate(item, newItem.transform.Find("Model"));
            foreach (var collider in model.GetComponentsInChildren<BoxCollider>())
            {
                Destroy(collider);
            }
            foreach (var collider in model.GetComponentsInChildren<MeshCollider>())
            {
                Destroy(collider);
            }
            DestroyImmediate(model.GetComponent<ObjectPosition>());
            DestroyImmediate(model.GetComponent<CursorContextObjectManipulator>());
            DestroyImmediate(model.GetComponent<ObjectManipulator>());
            DestroyImmediate(model.GetComponent<BoundsControl>());
            DestroyImmediate(model.GetComponent<NearInteractionGrabbable>());

            foreach (var comp in model.GetComponents<Component>())
            {
                //Don't remove these components
                if (!(comp is Transform) && !(comp is BoxCollider) && !(comp is MeshRenderer) && !(comp is MeshFilter) && !(comp is MaterialInstance))
                {
                    DestroyImmediate(comp);
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if (model.transform.Find("rigRoot") != null)
                    DestroyImmediate(model.transform.Find("rigRoot").gameObject);
                if (model.transform.Find("FurnitureInfo") != null)
                    DestroyImmediate(model.transform.Find("FurnitureInfo").gameObject);
            }
            model.transform.localPosition = new Vector3();
        }

        GameObject furnitures = GameObject.Find("Furnitures");
        foreach (Transform child in furnitures.transform)
        {
            // Remove everything, except these two, because they are needed for the furniture menus
            if (child.name == "NearMenu3x1" || child.name == "Materials")
            {
                continue;
            }

            Destroy(child.gameObject);
        }

        GridObjectCollection goc = itemList.GetComponent<GridObjectCollection>();
        goc.UpdateCollection();
    }
}
