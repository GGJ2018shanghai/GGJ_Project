#if UNITY_SWITCH
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using nn.hid;


public class SwitchInputDevice : InputDevice
{
    public uint DeviceIndex { get; private set; }
    public NpadId NpadID { get; private set; }
    public NpadStyle NpadStyle { get; private set; }

    public NpadState npadState = new NpadState();
    private VibrationValue vibrationValue = VibrationValue.Make();


    const int VibrationDeviceCountMax = 2;

    private int vibrationDeviceCount = 0;
    private VibrationDeviceHandle[] vibrationDeviceHandles = new VibrationDeviceHandle[VibrationDeviceCountMax];
    private VibrationDeviceInfo[] vibrationDeviceInfos = new VibrationDeviceInfo[VibrationDeviceCountMax];

    private float leftVibrationValue = 0.0f;
    private float rightVibrationValue = 0.0f;

    public SwitchInputDevice(uint deviceIndex, NpadId npadID)
        : base("Switch Controller")
    {
        DeviceIndex = deviceIndex;
        NpadID = npadID;
        NpadStyle = NpadStyle.None;
        SortOrder = (int)deviceIndex;
        Meta = "Switch Controller On Switch - #" + deviceIndex;

        AddControl(InputControlType.Action1, "A");
        AddControl(InputControlType.Action2, "B");
        AddControl(InputControlType.Action3, "Y");
        AddControl(InputControlType.Action4, "X");

        AddControl(InputControlType.LeftBumper, "L");
        AddControl(InputControlType.RightBumper, "R");

        AddControl(InputControlType.Select, "Minus");
        AddControl(InputControlType.Start, "Plus");

        AddControl(InputControlType.LeftStickButton, "LeftStickButton");
        AddControl(InputControlType.RightStickButton, "RightStickButton");

        AddControl(InputControlType.LeftTrigger, "ZL");
        AddControl(InputControlType.RightTrigger, "ZR");

        AddControl(InputControlType.LeftStickX, "Left Stick X");
        AddControl(InputControlType.LeftStickY, "Left Stick Y");

        AddControl(InputControlType.RightStickX, "Right Stick X");
        AddControl(InputControlType.RightStickY, "Right Stick Y");

        AddControl(InputControlType.DPadUp, "DPadUp");
        AddControl(InputControlType.DPadDown, "DPadDown");
        AddControl(InputControlType.DPadLeft, "DPadLeft");
        AddControl(InputControlType.DPadRight, "DPadRight");
    }


    public override void Update(ulong updateTick, float deltaTime)
    {
        UpdateGamepadState();
        switch (NpadStyle)
        {
            case NpadStyle.Handheld:

                //NOTE: A and B swapped so that A is the main confirm action for the game
                UpdateWithState(InputControlType.Action1, GetButtonState(NpadButton.A), updateTick, deltaTime);
                UpdateWithState(InputControlType.Action2, GetButtonState(NpadButton.B), updateTick, deltaTime);
                UpdateWithState(InputControlType.Action3, GetButtonState(NpadButton.Y), updateTick, deltaTime);
                UpdateWithState(InputControlType.Action4, GetButtonState(NpadButton.X), updateTick, deltaTime);

                UpdateWithState(InputControlType.LeftBumper, GetButtonState(NpadButton.L), updateTick, deltaTime);
                UpdateWithState(InputControlType.RightBumper, GetButtonState(NpadButton.R), updateTick, deltaTime);

                UpdateWithState(InputControlType.LeftTrigger, GetButtonState(NpadButton.ZL), updateTick, deltaTime);
                UpdateWithState(InputControlType.RightTrigger, GetButtonState(NpadButton.ZR), updateTick, deltaTime);

                UpdateWithState(InputControlType.Select, GetButtonState(NpadButton.Minus), updateTick, deltaTime);
                UpdateWithState(InputControlType.Start, GetButtonState(NpadButton.Plus), updateTick, deltaTime);

                UpdateWithState(InputControlType.LeftStickButton, GetButtonState(NpadButton.StickL), updateTick, deltaTime);
                UpdateWithState(InputControlType.RightStickButton, GetButtonState(NpadButton.StickR), updateTick, deltaTime);

                UpdateWithState(InputControlType.DPadUp, GetButtonState(NpadButton.Up), updateTick, deltaTime);
                UpdateWithState(InputControlType.DPadDown, GetButtonState(NpadButton.Down), updateTick, deltaTime);
                UpdateWithState(InputControlType.DPadLeft, GetButtonState(NpadButton.Left), updateTick, deltaTime);
                UpdateWithState(InputControlType.DPadRight, GetButtonState(NpadButton.Right), updateTick, deltaTime);

                UpdateWithValue(InputControlType.LeftStickX, GetAnalogValue(AnalogID.LeftStickHoz), updateTick, deltaTime);
                UpdateWithValue(InputControlType.LeftStickY, GetAnalogValue(AnalogID.LeftStickVert), updateTick, deltaTime);
                UpdateWithValue(InputControlType.RightStickX, GetAnalogValue(AnalogID.RightStickHoz), updateTick, deltaTime);
                UpdateWithValue(InputControlType.RightStickY, GetAnalogValue(AnalogID.RightStickVert), updateTick, deltaTime);

                break;
            default:
                break;
        }

        for (int i = 0; i < vibrationDeviceCount; i++)
        {
            vibrationValue.Clear();

            if (NpadStyle != NpadStyle.None)
            {
                if (vibrationDeviceInfos[i].position == VibrationDevicePosition.Left)
                {
                    vibrationValue.amplitudeLow = leftVibrationValue;
                }
                else if (vibrationDeviceInfos[i].position == VibrationDevicePosition.Right)
                {
                    vibrationValue.amplitudeLow = rightVibrationValue;
                }
            }

            Vibration.SendValue(vibrationDeviceHandles[i], vibrationValue);
        }

        Commit(updateTick, deltaTime);

    }

    public override void Vibrate(float leftMotor, float rightMotor)
    {
        //Reducing Vibration by 75% here so we can use same vibrations settings across all platforms
        leftVibrationValue = leftMotor * 0.25f;
        rightVibrationValue = rightMotor * 0.25f;
    }

    public bool IsConnected
    {
        get { return NpadStyle != NpadStyle.None && (npadState.attributes & NpadAttribute.IsConnected) != 0; }
    }


    private bool GetButtonState(NpadButton buttonID)
    {
        if (NpadStyle == NpadStyle.None)
        {
            return false;
        }

        return (npadState.buttons & buttonID) != 0;
    }

    enum AnalogID
    {
        LeftStickHoz,
        LeftStickVert,
        RightStickHoz,
        RightStickVert,
    }

    private float GetAnalogValue(AnalogID analogID)
    {
        return GetAnalogValue(analogID, false);
    }


    private float GetAnalogValue(AnalogID analogID, bool invert)
    {
        if (NpadStyle == NpadStyle.None)
        {
            return 0.0f;
        }

        float rawValue = 0.0f;
        bool isYAxis = false;

        switch (analogID)
        {
            case AnalogID.LeftStickHoz:
                rawValue = npadState.analogStickL.x;
                break;
            case AnalogID.LeftStickVert:
                rawValue = npadState.analogStickL.y;
                isYAxis = true;
                break;
            case AnalogID.RightStickHoz:
                rawValue = npadState.analogStickR.x;
                break;
            case AnalogID.RightStickVert:
                rawValue = npadState.analogStickR.y;
                isYAxis = true;
                break;
            default:
                throw new ArgumentOutOfRangeException("analogID", analogID, null);
        }

        if (invert ^ (isYAxis && InControl.InputManager.InvertYAxis))
        {
            rawValue = -rawValue;
        }

        return rawValue / AnalogStickState.Max;
    }

    private void UpdateGamepadState()
    {
        NpadStyle npadStyle = Npad.GetStyleSet(NpadID);

        if (NpadStyle != npadStyle)
        {
            Debug.Log(NpadID.ToString() + " Pad Style Changed. Old: " + NpadStyle.ToString() + " New: " + npadStyle.ToString());
            vibrationDeviceCount = Vibration.GetDeviceHandles(vibrationDeviceHandles, VibrationDeviceCountMax, NpadID, npadStyle);
            vibrationDeviceCount = Mathf.Min(vibrationDeviceCount, VibrationDeviceCountMax);

            for (int i = 0; i < vibrationDeviceCount; i++)
            {
                Vibration.InitializeDevice(vibrationDeviceHandles[i]);
                Vibration.GetDeviceInfo(ref vibrationDeviceInfos[i], vibrationDeviceHandles[i]);
            }
        }

        NpadStyle = npadStyle;

        if (NpadStyle != NpadStyle.None)
        {
            Npad.GetState(ref npadState, NpadID, npadStyle);
        }
    }
}

#endif