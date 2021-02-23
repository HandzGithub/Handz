using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public enum Hand
{
    Left = 1,
    Right = 2,
}

public class HandzRig : MonoBehaviour
{
    public HandzInputWrapper leftHand;
    public HandzInputWrapper rightHand;

    [Header("Enums")]
    public Hand movementHand;
    public Hand rotationHand;

    [Header("Values")]
    public float speed;

    [Header("Rigidbodies")]
    public Rigidbody locoSphere;

    [Header("Transforms")]
    public Transform cam;

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleRoomscaleMovement()
    {

    }

    void HandleMovement()
    {
        if (movementHand == Hand.Left)
        {
            locoSphere.AddTorque(cam.TransformDirection(new Vector3(leftHand.joystick.x, 0, leftHand.joystick.y) * speed));
        }

        if (movementHand == Hand.Right)
        {
            locoSphere.AddTorque(cam.TransformDirection(new Vector3(rightHand.joystick.x, 0, rightHand.joystick.y) * speed));
        }
    }
}
