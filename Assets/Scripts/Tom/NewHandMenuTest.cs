using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuySelectedItems))]
public class NewHandMenuTest : MonoBehaviour
{
    bool isExpertModeEnabled = false;


    public void BuyNow()
    {
        gameObject.GetComponent<BuySelectedItems>().BuyNow();
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
