using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit.Input;

[RequireComponent(typeof(MenuHelper))]
public class ItemList : MonoBehaviour
{
    public GameObject itemPrefab;
    [SerializeField]
    public GameObject itemList;

    [SerializeField]
    bool updateList = false;

    private void Update()
    {
        if (updateList)
        {
            UpdateItemList();
            updateList = false;
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

        List<GameObject> placedItems = gameObject.GetComponent<MenuHelper>().FindPlacedItems();

        foreach (GameObject item in placedItems)
        {
            // Add MeshRenderers to the Clipping Bounds Renderer Array
            //GameObject clippingBoundsObject = FindInActiveObjectByName("Clipping Bounds");
            //ClippingBox clippingBox = clippingBoundsObject.GetComponent<ClippingBox>();
            //MeshRenderer[] meshRenderers = item.GetComponentsInChildren<MeshRenderer>();
            //for (int i = 0; i < meshRenderers.Length; i++)
            //{
            //    clippingBox.AddRenderer(meshRenderers[i]);
            //}

            // Instantiate the itemPrefab
            GameObject newItem = Instantiate(itemPrefab, itemList.transform);

            newItem.transform.Find("Name").GetComponent<TextMeshPro>().text = item.name;
            newItem.transform.Find("Price").GetComponent<TextMeshPro>().text = item.GetComponent<FurnitureAttributes>().price.ToString() + ".00€";
            // Create a minified version of the actual object as a icon
            GameObject model = Instantiate(item, newItem.transform.Find("Model"));
            // Also we have to remove every boxCollider for the scrollingObjectCollection to work correctly.
            // Appearently it also looks into the children and searches for their colliders 
            foreach (var collider in model.GetComponentsInChildren<BoxCollider>())
            {
                Destroy(collider);
            }
            foreach (var collider in model.GetComponentsInChildren<MeshCollider>())
            {
                Destroy(collider);
            }


            // This is some really fucked way to achieve this. We need to remove most of the script
            // of the cloned instance because else the furniture could be moved out the menu or would
            // even fall through the menu
            // This needs some refactoring if there would be more time.
            DestroyImmediate(model.GetComponent<ObjectPosition>());
            DestroyImmediate(model.GetComponent<CursorContextObjectManipulator>());
            DestroyImmediate(model.GetComponent<ObjectManipulator>());
            DestroyImmediate(model.GetComponent<BoundsControl>());
            DestroyImmediate(model.GetComponent<NearInteractionGrabbable>());

            foreach (var comp in model.GetComponents<Component>())
            {
                //Don't remove these components
                if (!(comp is Transform) && !(comp is BoxCollider) && !(comp is MeshRenderer) && !(comp is MeshFilter))
                {
                    DestroyImmediate(comp);
                }
            }

            // This also is not nice way to do this
            // The rigRoot gets added as a child from MRTK but if this sits on our models in the menu they are just grey
            // And we need the for because sometimes there are atleast two of them
            for (int i = 0; i < 5; i++)
            {
                if (model.transform.Find("rigRoot") != null)
                    DestroyImmediate(model.transform.Find("rigRoot").gameObject);
            }

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
        yield return new WaitForSeconds(0.01f);
        gridObjectCollection.UpdateCollection();
    }

    //GameObject FindInActiveObjectByName(string name)
    //{
    //    Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
    //    for (int i = 0; i < objs.Length; i++)
    //    {
    //        if (objs[i].hideFlags == HideFlags.None)
    //        {
    //            if (objs[i].name == name)
    //            {
    //                return objs[i].gameObject;
    //            }
    //        }
    //    }
    //    return null;
    //}
}
