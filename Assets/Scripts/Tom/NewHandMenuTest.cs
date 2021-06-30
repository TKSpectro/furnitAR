using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHandMenuTest : MonoBehaviour
{
    bool isExpertModeEnabled = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
