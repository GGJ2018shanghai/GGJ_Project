using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonCode
{
    OptionLeft,
    OptionRight,

    ButtonA,
    ButtonB,
    ButtonX,
    ButtonY,

    DPadLeft,
    DPadRight,
    DPadUp,
    DPadDown,

    LeftBumper,
    RightBumper,

    LeftTrigger,
    RightTrigger,

    NONE,
}

public class InputManager : SystemBase<InputManager, InputManagerData> {

	// Use this for initialization
	new void Awake ()
    {
        base.Awake();
        var incontrolManager = gameObject.AddComponent<InControl.InControlManager>();
        inputActions = InputActions.CreateWithDefaultBindings();

        incontrolManager.logDebugInfo = Data.logDebugInfo;
        incontrolManager.invertYAxis = Data.invertYAxis;
        incontrolManager.useFixedUpdate = Data.useFixedUpdate;
        incontrolManager.suspendInBackground = Data.suspendInBackground;
    }

    private InputActions inputActions;
    public InputActions InputActions { get { return inputActions; } }

    static public bool IsPressed(ButtonCode btn)
    {
        switch (btn)
        {
            case ButtonCode.OptionLeft:
                return Instance.inputActions.OptionLeft.IsPressed;
            case ButtonCode.OptionRight:
                return Instance.inputActions.OptionRight.IsPressed;
            case ButtonCode.ButtonA:
                return Instance.inputActions.ButtonA.IsPressed;
            case ButtonCode.ButtonB:
                return Instance.inputActions.ButtonB.IsPressed;
            case ButtonCode.ButtonX:
                return Instance.inputActions.ButtonX.IsPressed;
            case ButtonCode.ButtonY:
                return Instance.inputActions.ButtonY.IsPressed;
            case ButtonCode.DPadLeft:
                return Instance.inputActions.DPadLeft.IsPressed;
            case ButtonCode.DPadRight:
                return Instance.inputActions.DPadRight.IsPressed;
            case ButtonCode.DPadUp:
                return Instance.inputActions.DPadUp.IsPressed;
            case ButtonCode.DPadDown:
                return Instance.inputActions.DPadDown.IsPressed;
            case ButtonCode.LeftBumper:
                return Instance.inputActions.LeftBumper.IsPressed;
            case ButtonCode.RightBumper:
                return Instance.inputActions.RightBumper.IsPressed;
            case ButtonCode.LeftTrigger:
                return Instance.inputActions.LeftTrigger.IsPressed;
            case ButtonCode.RightTrigger:
                return Instance.inputActions.RightTrigger.IsPressed;
            default:
                return false;
        }
    }
    static public bool IsButtonDown(ButtonCode btn)
    {
        switch (btn)
        {
            case ButtonCode.OptionLeft:
                return Instance.inputActions.OptionLeft.IsPressed && !Instance.inputActions.OptionLeft.LastState;
            case ButtonCode.OptionRight:
                return Instance.inputActions.OptionRight.IsPressed && !Instance.inputActions.OptionRight.LastState;
            case ButtonCode.ButtonA:
                return Instance.inputActions.ButtonA.IsPressed && !Instance.inputActions.ButtonA.LastState;
            case ButtonCode.ButtonB:
                return Instance.inputActions.ButtonB.IsPressed && !Instance.inputActions.ButtonB.LastState;
            case ButtonCode.ButtonX:
                return Instance.inputActions.ButtonX.IsPressed && !Instance.inputActions.ButtonX.LastState;
            case ButtonCode.ButtonY:
                return Instance.inputActions.ButtonY.IsPressed && !Instance.inputActions.ButtonY.LastState;
            case ButtonCode.DPadLeft:
                return Instance.inputActions.DPadLeft.IsPressed && !Instance.inputActions.DPadLeft.LastState;
            case ButtonCode.DPadRight:
                return Instance.inputActions.DPadRight.IsPressed && !Instance.inputActions.DPadRight.LastState;
            case ButtonCode.DPadUp:
                return Instance.inputActions.DPadUp.IsPressed && !Instance.inputActions.DPadUp.LastState;
            case ButtonCode.DPadDown:
                return Instance.inputActions.DPadDown.IsPressed && !Instance.inputActions.DPadDown.LastState;
            case ButtonCode.LeftBumper:
                return Instance.inputActions.LeftBumper.IsPressed && !Instance.inputActions.LeftBumper.LastState;
            case ButtonCode.RightBumper:
                return Instance.inputActions.RightBumper.IsPressed && !Instance.inputActions.RightBumper.LastState;
            case ButtonCode.LeftTrigger:
                return Instance.inputActions.LeftTrigger.IsPressed && !Instance.inputActions.LeftTrigger.LastState;
            case ButtonCode.RightTrigger:
                return Instance.inputActions.RightTrigger.IsPressed && !Instance.inputActions.RightTrigger.LastState;
            default:
                return false;
        }
    }
    static public bool IsButtonUp(ButtonCode btn)
    {
        switch (btn)
        {
            case ButtonCode.OptionLeft:
                return Instance.inputActions.OptionLeft.LastState && !Instance.inputActions.OptionLeft.IsPressed;
            case ButtonCode.OptionRight:
                return Instance.inputActions.OptionRight.LastState && !Instance.inputActions.OptionRight.IsPressed;
            case ButtonCode.ButtonA:
                return Instance.inputActions.ButtonA.LastState && !Instance.inputActions.ButtonA.IsPressed;
            case ButtonCode.ButtonB:
                return Instance.inputActions.ButtonB.LastState && !Instance.inputActions.ButtonB.IsPressed;
            case ButtonCode.ButtonX:
                return Instance.inputActions.ButtonX.LastState && !Instance.inputActions.ButtonX.IsPressed;
            case ButtonCode.ButtonY:
                return Instance.inputActions.ButtonY.LastState && !Instance.inputActions.ButtonY.IsPressed;
            case ButtonCode.DPadLeft:
                return Instance.inputActions.DPadLeft.LastState && !Instance.inputActions.DPadLeft.IsPressed;
            case ButtonCode.DPadRight:
                return Instance.inputActions.DPadRight.LastState && !Instance.inputActions.DPadRight.IsPressed;
            case ButtonCode.DPadUp:
                return Instance.inputActions.DPadUp.LastState && !Instance.inputActions.DPadUp.IsPressed;
            case ButtonCode.DPadDown:
                return Instance.inputActions.DPadDown.LastState && !Instance.inputActions.DPadDown.IsPressed;
            case ButtonCode.LeftBumper:
                return Instance.inputActions.LeftBumper.LastState && !Instance.inputActions.LeftBumper.IsPressed;
            case ButtonCode.RightBumper:
                return Instance.inputActions.RightBumper.LastState && !Instance.inputActions.RightBumper.IsPressed;
            case ButtonCode.LeftTrigger:
                return Instance.inputActions.LeftTrigger.LastState && !Instance.inputActions.LeftTrigger.IsPressed;
            case ButtonCode.RightTrigger:
                return Instance.inputActions.RightTrigger.LastState && !Instance.inputActions.RightTrigger.IsPressed;
            default:
                return false;
        }
    }
    static Vector2 LeftStickValue { get { return Instance.inputActions.LeftStick.Value; } }
    static Vector2 RightStickValue { get { return Instance.inputActions.RightStick.Value; } }
}
