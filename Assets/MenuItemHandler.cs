using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemHandler : MonoBehaviour
{
    public OVRHand hand;
    public float pinchTreshold = 0.7f;

    public float pinchStrength;
    bool isPinching = false;

    public bool isColliding = false;

    void Update()
    {
        pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        isPinching = pinchStrength > pinchTreshold;

        if (isColliding)
        {
            if (isPinching)
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.yellow;
            }
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameController"))
        {
            isColliding = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GameController"))
        {
            isColliding = false;
        }
    }
}
