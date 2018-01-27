using InControl;
using UnityEngine;

public class InputActions : PlayerActionSet
{
    public PlayerAction OptionLeft;     // minus
    public PlayerAction OptionRight;    // plus

    public PlayerAction ButtonA;
    public PlayerAction ButtonB;
    public PlayerAction ButtonX;
    public PlayerAction ButtonY;

    public PlayerAction DPadLeft;
    public PlayerAction DPadRight;
    public PlayerAction DPadUp;
    public PlayerAction DPadDown;

    public PlayerAction LeftBumper;     // L
    public PlayerAction RightBumper;    // R
    public PlayerAction LeftTrigger;    // ZL
    public PlayerAction RightTrigger;   // ZR

    private PlayerAction LeftStickUp;
    private PlayerAction LeftStickDown;
    private PlayerAction LeftStickLeft;
    private PlayerAction LeftStickRight;
    private PlayerAction RightStickUp;
    private PlayerAction RightStickDown;
    private PlayerAction RightStickLeft;
    private PlayerAction RightStickRight;
    public PlayerTwoAxisAction LeftStick;
    public PlayerTwoAxisAction RightStick;

    public InputActions()
    {
        OptionLeft = CreatePlayerAction("OptionLeft");
        OptionRight = CreatePlayerAction("OptionRight");
        ButtonA = CreatePlayerAction("ButtonA");
        ButtonB = CreatePlayerAction("ButtonB");
        ButtonX = CreatePlayerAction("ButtonX");
        ButtonY = CreatePlayerAction("ButtonY");
        DPadLeft = CreatePlayerAction("DPadLeft");
        DPadRight = CreatePlayerAction("DPadRight");
        DPadUp = CreatePlayerAction("DPadUp");
        DPadDown = CreatePlayerAction("DPadDown");
        LeftBumper = CreatePlayerAction("LeftBumper");
        RightBumper = CreatePlayerAction("RightBumper");
        LeftTrigger = CreatePlayerAction("LeftTrigger");
        RightTrigger = CreatePlayerAction("RightTrigger");
        LeftStickUp = CreatePlayerAction("LeftStickUp");
        LeftStickDown = CreatePlayerAction("LeftStickDown");
        LeftStickLeft = CreatePlayerAction("LeftStickLeft");
        LeftStickRight = CreatePlayerAction("LeftStickRight");
        RightStickUp = CreatePlayerAction("RightStickUp");
        RightStickDown = CreatePlayerAction("RightStickDown");
        RightStickLeft = CreatePlayerAction("RightStickLeft");
        RightStickRight = CreatePlayerAction("RightStickRight");

        LeftStick = CreateTwoAxisPlayerAction(LeftStickLeft, LeftStickRight, LeftStickDown, LeftStickUp);
        RightStick = CreateTwoAxisPlayerAction(RightStickLeft, RightStickRight, RightStickDown, RightStickUp);
    }

    public void Vibrate(float intensity)
    {
        var inputDevice = InControl.InputManager.ActiveDevice;
        inputDevice.Vibrate(intensity);
    }

    public static InputActions CreateWithDefaultBindings()
    {
        var playerActions = new InputActions();

        playerActions.ButtonA.AddDefaultBinding(Key.L);
        playerActions.ButtonA.AddDefaultBinding(InputControlType.Action2);  // right
        playerActions.ButtonB.AddDefaultBinding(Key.K);
        playerActions.ButtonB.AddDefaultBinding(InputControlType.Action1);  // down
        playerActions.ButtonX.AddDefaultBinding(Key.O);
        playerActions.ButtonX.AddDefaultBinding(InputControlType.Action4);  // up
        playerActions.ButtonY.AddDefaultBinding(Key.I);
        playerActions.ButtonY.AddDefaultBinding(InputControlType.Action3);  // left

        playerActions.OptionLeft.AddDefaultBinding(Key.Y);
        playerActions.OptionLeft.AddDefaultBinding(InputControlType.Minus);
        playerActions.OptionRight.AddDefaultBinding(Key.U);
        playerActions.OptionRight.AddDefaultBinding(InputControlType.Plus);


        playerActions.DPadDown.AddDefaultBinding(Key.DownArrow);
        playerActions.DPadDown.AddDefaultBinding(InputControlType.DPadDown);
        playerActions.DPadUp.AddDefaultBinding(Key.UpArrow);
        playerActions.DPadUp.AddDefaultBinding(InputControlType.DPadUp);
        playerActions.DPadLeft.AddDefaultBinding(Key.LeftArrow);
        playerActions.DPadLeft.AddDefaultBinding(InputControlType.DPadLeft);
        playerActions.DPadRight.AddDefaultBinding(Key.RightArrow);
        playerActions.DPadRight.AddDefaultBinding(InputControlType.DPadRight);

        playerActions.LeftBumper.AddDefaultBinding(Key.A);
        playerActions.LeftBumper.AddDefaultBinding(InputControlType.LeftBumper);
        playerActions.RightBumper.AddDefaultBinding(Key.S);
        playerActions.RightBumper.AddDefaultBinding(InputControlType.RightBumper);
        playerActions.LeftTrigger.AddDefaultBinding(Key.Q);
        playerActions.LeftTrigger.AddDefaultBinding(InputControlType.LeftTrigger);
        playerActions.RightTrigger.AddDefaultBinding(Key.W);
        playerActions.RightTrigger.AddDefaultBinding(InputControlType.RightTrigger);


        playerActions.LeftStickLeft.AddDefaultBinding(InputControlType.LeftStickLeft);
        playerActions.LeftStickRight.AddDefaultBinding(InputControlType.LeftStickRight);
        playerActions.LeftStickUp.AddDefaultBinding(InputControlType.LeftStickUp);
        playerActions.LeftStickDown.AddDefaultBinding(InputControlType.LeftStickDown);

        playerActions.RightStickLeft.AddDefaultBinding(InputControlType.RightStickLeft);
        playerActions.RightStickRight.AddDefaultBinding(InputControlType.RightStickRight);
        playerActions.RightStickUp.AddDefaultBinding(InputControlType.RightStickUp);
        playerActions.RightStickDown.AddDefaultBinding(InputControlType.RightStickDown);

        playerActions.ListenOptions.IncludeUnknownControllers = true;
        playerActions.ListenOptions.MaxAllowedBindings = 4;
        playerActions.ListenOptions.UnsetDuplicateBindingsOnSet = true;

        return playerActions;
    }
}
