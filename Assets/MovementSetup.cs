using UnityEngine;
using System.Collections;
using System.Diagnostics;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MovementSetup : MonoBehaviour
{
    private LocomotionController lc;
    private bool inMenu = false;
    private LocomotionTeleport TeleportController
    {
        get
        {
            return lc.GetComponent<LocomotionTeleport>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        lc = FindObjectOfType<LocomotionController>();

        SetupLeftStrafeRightTeleport();
        Physics.IgnoreLayerCollision(0, 4);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetupTeleportDefaults()
    {
        TeleportController.enabled = true;
        //lc.PlayerController.SnapRotation = true;
        lc.PlayerController.RotationEitherThumbstick = false;
        //lc.PlayerController.FixedSpeedSteps = 0;
        TeleportController.EnableMovement(false, false, false, false);
        TeleportController.EnableRotation(false, false, false, false);

        var input = TeleportController.GetComponent<TeleportInputHandlerTouch>();
        input.InputMode = TeleportInputHandlerTouch.InputModes.CapacitiveButtonForAimAndTeleport;
        input.AimButton = OVRInput.RawButton.A;
        input.TeleportButton = OVRInput.RawButton.A;
        input.CapacitiveAimAndTeleportButton = TeleportInputHandlerTouch.AimCapTouchButtons.A;
        input.FastTeleport = false;

        var hmd = TeleportController.GetComponent<TeleportInputHandlerHMD>();
        hmd.AimButton = OVRInput.RawButton.A;
        hmd.TeleportButton = OVRInput.RawButton.A;

        var orient = TeleportController.GetComponent<TeleportOrientationHandlerThumbstick>();
        orient.Thumbstick = OVRInput.Controller.LTouch;
    }

    void SetupLeftStrafeRightTeleport()
    {
        SetupTeleportDefaults();
        TeleportController.EnableRotation(true, false, false, true);
        TeleportController.EnableMovement(true, false, false, false);
        //lc.PlayerController.SnapRotation = true;
        //lc.PlayerController.FixedSpeedSteps = 1;

        var input = TeleportController.GetComponent<TeleportInputHandlerTouch>();
        input.InputMode = TeleportInputHandlerTouch.InputModes.ThumbstickTeleportForwardBackOnly;
        input.AimingController = OVRInput.Controller.RTouch;
        ActivateHandlers<TeleportInputHandlerTouch, TeleportAimHandlerParabolic, TeleportTargetHandlerPhysical, TeleportOrientationHandlerThumbstick, TeleportTransitionBlink>();
        var orient = TeleportController.GetComponent<TeleportOrientationHandlerThumbstick>();
        orient.Thumbstick = OVRInput.Controller.RTouch;

        // custom fix
        var targetHandlerPhysical = TeleportController.GetComponent<TeleportTargetHandlerPhysical>();
        targetHandlerPhysical.AimCollisionLayerMask = LayerMask.GetMask("Default", "Water");
    }

    /// <summary>
    /// This method will ensure only one specific type TActivate in a given group of components derived from the same TCategory type is enabled.
    /// This is used by the sample support code to select between different targeting, input, aim, and other handlers.
    /// </summary>
    /// <typeparam name="TCategory"></typeparam>
    /// <typeparam name="TActivate"></typeparam>
    /// <param name="target"></param>
    public static TActivate ActivateCategory<TCategory, TActivate>(GameObject target) where TCategory : MonoBehaviour where TActivate : MonoBehaviour
    {
        var components = target.GetComponents<TCategory>();
        Debug.Log("Activate " + typeof(TActivate) + " derived from " + typeof(TCategory) + "[" + components.Length + "]");
        TActivate result = null;
        for (int i = 0; i < components.Length; i++)
        {
            var c = (MonoBehaviour)components[i];
            var active = c.GetType() == typeof(TActivate);
            Debug.Log(c.GetType() + " is " + typeof(TActivate) + " = " + active);
            if (active)
            {
                result = (TActivate)c;
            }
            if (c.enabled != active)
            {
                c.enabled = active;
            }
        }
        return result;
    }

    /// <summary>
    /// This generic method is used for activating a specific set of components in the LocomotionController. This is just one way 
    /// to achieve the goal of enabling one component of each category (input, aim, target, orientation and transition) that
    /// the teleport system requires.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TAim"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    /// <typeparam name="TOrientation"></typeparam>
    /// <typeparam name="TTransition"></typeparam>
    protected void ActivateHandlers<TInput, TAim, TTarget, TOrientation, TTransition>()
        where TInput : TeleportInputHandler
        where TAim : TeleportAimHandler
        where TTarget : TeleportTargetHandler
        where TOrientation : TeleportOrientationHandler
        where TTransition : TeleportTransition
    {
        ActivateInput<TInput>();
        ActivateAim<TAim>();
        ActivateTarget<TTarget>();
        ActivateOrientation<TOrientation>();
        ActivateTransition<TTransition>();
    }

    protected void ActivateInput<TActivate>() where TActivate : TeleportInputHandler
    {
        ActivateCategory<TeleportInputHandler, TActivate>();
    }

    protected void ActivateAim<TActivate>() where TActivate : TeleportAimHandler
    {
        ActivateCategory<TeleportAimHandler, TActivate>();
    }

    protected void ActivateTarget<TActivate>() where TActivate : TeleportTargetHandler
    {
        ActivateCategory<TeleportTargetHandler, TActivate>();
    }

    protected void ActivateOrientation<TActivate>() where TActivate : TeleportOrientationHandler
    {
        ActivateCategory<TeleportOrientationHandler, TActivate>();
    }

    protected void ActivateTransition<TActivate>() where TActivate : TeleportTransition
    {
        ActivateCategory<TeleportTransition, TActivate>();
    }

    protected TActivate ActivateCategory<TCategory, TActivate>() where TCategory : MonoBehaviour where TActivate : MonoBehaviour
    {
        return ActivateCategory<TCategory, TActivate>(lc.gameObject);
    }

    protected void UpdateToggle(Toggle toggle, bool enabled)
    {
        if (enabled != toggle.isOn)
        {
            toggle.isOn = enabled;
        }
    }
}
