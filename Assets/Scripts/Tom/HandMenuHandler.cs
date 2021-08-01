using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BuySelectedItems))]
[RequireComponent(typeof(ItemList))]
[RequireComponent(typeof(MenuHelper))]
public class HandMenuHandler : MonoBehaviour
{
    [SerializeField]
    GameObject menuContent;

    [SerializeField]
    GameObject shoppingCart;

    [SerializeField]
    public GameObject orders;

    [SerializeField]
    GameObject DialogPrefab;

    public void BuyNow()
    {
        GetComponent<BuySelectedItems>().BuyNow();

        // If the shoppingcart was shown we close it (set it inactive)
        if (shoppingCart.activeInHierarchy)
        {
            shoppingCart.SetActive(false);
        }
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

        // Hide the normal hand menu content
        menuContent.SetActive(!menuContent.activeInHierarchy);

        shoppingCart.SetActive(!shoppingCart.activeInHierarchy);
    }

    public void ToggleOrders()
    {
        // If the shoppingcart is not active, update its values (items and completePrice) and then show it
        if (!orders.activeInHierarchy)
        {
            List<GameObject> ordersItems = orders.GetComponent<OrdersHandler>().ordersItems;
        }

        // Hide the normal hand menu content
        menuContent.SetActive(false);

        orders.SetActive(!orders.activeInHierarchy);
    }

    public void toggleExpertMode()
    {
        menuContent.SetActive(false);
        Dialog.Open(DialogPrefab, DialogButtonType.OK, "Activate Expert Mode", "Dieses Feature konnte leider noch nicht implementiert werden.", true);
    }
}
