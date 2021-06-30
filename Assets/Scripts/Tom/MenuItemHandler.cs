using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class MenuItemHandler : MonoBehaviour
{
    private OVRHand rightHand;
    public float pinchTreshold = 0.7f;

    float pinchStrength;
    bool isPinching = false;

    bool isColliding = false;

    void Start()
    {
        rightHand = GameObject.FindObjectOfType<HandsManager>().RightHand;
    }

    void Update()
    {
        pinchStrength = rightHand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
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
