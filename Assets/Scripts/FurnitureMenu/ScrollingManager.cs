using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingManager : MonoBehaviour
{
    public bool hasMomentum = false;

    public void OnMomentumStarted()
    {
        hasMomentum = true;
    }

    public void OnMomentumEnded()
    {
        hasMomentum = false;
    }
}
