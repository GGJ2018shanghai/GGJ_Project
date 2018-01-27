#if UNITY_SWITCH
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nn.hid;

namespace InControl
{
    public class SwitchInputDeviceManager : InputDeviceManager
    {
        const int numControler = 2;
        bool[] deviceConnected = new bool[numControler] { false, false };
        private NpadId[] npadIds = new NpadId[numControler] { NpadId.Handheld, NpadId.No1, /*NpadId.No2, NpadId.No3, NpadId.No4, NpadId.No5, NpadId.No6, NpadId.No7, NpadId.No8*/ };

        public SwitchInputDeviceManager()
        {
            Npad.Initialize();
            Npad.SetSupportedIdType(npadIds, numControler);
            Npad.SetSupportedStyleSet(NpadStyle.Handheld | NpadStyle.FullKey | NpadStyle.JoyDual);

            for (uint deviceIndex = 0; deviceIndex < numControler; deviceIndex++)
            {
                devices.Add(new SwitchInputDevice(deviceIndex, npadIds[deviceIndex]));
            }

            Update(0, 0.0f);
        }

        public override void Update(ulong updateTick, float deltaTime)
        {
            for (int deviceIndex = 0; deviceIndex < numControler; deviceIndex++)
            {
                var device = devices[deviceIndex] as SwitchInputDevice;

                // Unconnected devices won't be updated otherwise, so poll here.
                if (!device.IsConnected)
                {
                    device.Update(updateTick, deltaTime);
                }
            }

            UpdateConnectedDevices();
        }

        public void UpdateConnectedDevices()
        {
            for (int deviceIndex = 0; deviceIndex < numControler; deviceIndex++)
            {
                var device = devices[deviceIndex] as SwitchInputDevice;

                if (device.IsConnected != deviceConnected[deviceIndex])
                {
                    if (device.IsConnected)
                    {
                        InputManager.AttachDevice(device);
                    }
                    else
                    {
                        InputManager.DetachDevice(device);
                    }

                    deviceConnected[deviceIndex] = device.IsConnected;
                }
            }
        }

        public static bool CheckPlatformSupport(ICollection<string> errors)
        {
            if (Application.platform != RuntimePlatform.Switch)
            {
                return false;
            }

            return true;
        }


        public static void Enable()
        {
            var errors = new List<string>();
            if (SwitchInputDeviceManager.CheckPlatformSupport(errors))
            {
                InputManager.AddDeviceManager<SwitchInputDeviceManager>();
            }
            else
            {
                foreach (var error in errors)
                {
                    Logger.LogError(error);
                }
            }
        }
    }
}

#endif
