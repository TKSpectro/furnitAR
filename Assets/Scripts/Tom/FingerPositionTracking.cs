using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class FingerPositionTracking : MonoBehaviour
{
    OVRHand leftHand;
    OVRHand rightHand;

    public OVRHand[] hands;

    public GameObject fingerTip;
    public GameObject sphere;

    public bool draw = false;

    // Start is called before the first frame update
    void Start()
    {
        hands = GameObject.FindObjectsOfType<OVRHand>();

        rightHand = hands[0];
        leftHand = hands[1];

        //leftHand = GameObject.FindObjectOfType<HandsManager>().LeftHand;
        //rightHand = GameObject.FindObjectOfType<HandsManager>().RightHand;
    }

    // Update is called once per frame
    void Update()
    {
        if (rightHand.IsTracked && draw)
        {
            Vector3 pos = rightHand.gameObject.GetComponent<OVRSkeleton>().Bones[20].Transform.position;
            //Debug.Log(pos);
            fingerTip.transform.position = pos;
            Instantiate(sphere, pos, Quaternion.identity, null);
        }

    }

    public void StartDraw()
    {
        draw = true;
    }

    public void StopDraw()
    {
        draw = false;
    }
}
