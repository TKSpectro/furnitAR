using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BuySelectedItems))]
[RequireComponent(typeof(ItemList))]
[RequireComponent(typeof(MenuHelper))]
public class NewHandMenuTest : MonoBehaviour
{
    bool isExpertModeEnabled = false;

    [SerializeField]
    GameObject shoppingCart;

    public void BuyNow()
    {
        GetComponent<BuySelectedItems>().BuyNow();

        // If the shoppingcart was shown we close it (set it inactive)
        if (shoppingCart.activeInHierarchy)
        {
            shoppingCart.SetActive(false);
        }
    }

    public void UpdateCollection()
    {
        GetComponent<ItemList>().itemList.GetComponent<GridObjectCollection>().UpdateCollection();
    }

    public void ToggleShoppingCart()
    {
        // If the shoppingcart is not active, update its values (items and completePrice) and then show it
        if (!shoppingCart.activeInHierarchy)
        {
            List<GameObject> placedItems = GetComponent<ItemList>().UpdateItemList();

            int completePrice = GetComponent<MenuHelper>().CalculateShoppingCartValue(placedItems);

            shoppingCart.transform.Find("ShoppingCartPrice").gameObject.GetComponent<TextMeshPro>().text = completePrice.ToString() + "€";
        }

        shoppingCart.SetActive(!shoppingCart.activeInHierarchy);
    }

    public void toggleExpertMode()
    {
        isExpertModeEnabled = !isExpertModeEnabled;
        Debug.Log("Is Expertmode enabled: " + isExpertModeEnabled);
    }

    public void OutputSomething(bool test)
    {
        Debug.Log("Output: ");
    }

    public void OutputSomething2(string test)
    {
        Debug.Log("Output: " + test);
    }

    public void OutputSomething3(int test)
    {
        Debug.Log("Output: ");
    }
}
