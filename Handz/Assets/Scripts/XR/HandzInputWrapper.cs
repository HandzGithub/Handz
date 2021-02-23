//-------------------------------------
//Summary
//Usage: This script is used to simplify handling inputs
//Summary
//-------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

[System.Serializable]
public class PlayerInputs
{
    public bool active;
    public float value;
}

public class HandzInputWrapper : MonoBehaviour
{
    public HandzInputWrapper handz;

    public bool debugMode;

    public bool isLeft;
    public bool isRight;

    public Vector2 joystick;

    public PlayerInputs trigger;

    public PlayerInputs grip;

    public bool primary;
    public bool secondary;

    InputDevice targetDevice;

    void Start()
    {
        handz = this;

        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        foreach (var device in devices)
        {
            Debug.Log(device.name + device.characteristics);

            if (device.characteristics == InputDeviceCharacteristics.Left && isLeft)
            {
                targetDevice = device;
            }

            if (device.characteristics == InputDeviceCharacteristics.Right && isRight)
            {
                targetDevice = device;
            }
        }
    }

    void Update()
    {
        if (!debugMode)
        {
            targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystick);

            targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out trigger.active);
            targetDevice.TryGetFeatureValue(CommonUsages.trigger, out trigger.value);

            targetDevice.TryGetFeatureValue(CommonUsages.gripButton, out grip.active);
            targetDevice.TryGetFeatureValue(CommonUsages.grip, out grip.value);

            targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out primary);

            targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out secondary);
        }
    }
}
