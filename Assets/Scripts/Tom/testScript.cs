using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRHand;

public class testScript : MonoBehaviour
{
    [SerializeField]
    bool isThumbPinching;
    [SerializeField]
    float thumbPinchStrength;
    [SerializeField]
    TrackingConfidence thumbConfidence;

    public bool isIndexFingerPinching;
    public bool isMiddleFingerPinching;
    public bool isRingFingerPinching;
    public bool isPinkyFingerPinching;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OVRHand hand = GetComponent<OVRHand>();

        isThumbPinching = hand.GetFingerIsPinching(HandFinger.Thumb);
        thumbPinchStrength = hand.GetFingerPinchStrength(HandFinger.Thumb);
        thumbConfidence = hand.GetFingerConfidence(HandFinger.Thumb);

        isIndexFingerPinching = hand.GetFingerIsPinching(HandFinger.Index);
        isMiddleFingerPinching = hand.GetFingerIsPinching(HandFinger.Middle);
        isRingFingerPinching = hand.GetFingerIsPinching(HandFinger.Ring);
        isPinkyFingerPinching = hand.GetFingerIsPinching(HandFinger.Pinky);

        float ringFingerPinchStrength = hand.GetFingerPinchStrength(HandFinger.Index);
        TrackingConfidence confidence = hand.GetFingerConfidence(HandFinger.Index);

        Debug.Log("Strength Thumb" + hand.GetFingerPinchStrength(HandFinger.Thumb));
        Debug.Log("Strength Index" + hand.GetFingerPinchStrength(HandFinger.Index));
        Debug.Log("Strength Pinky" + hand.GetFingerPinchStrength(HandFinger.Pinky));

        //Debug.Log(ringFingerPinchStrength);
        //Debug.Log(confidence);
    }
}
