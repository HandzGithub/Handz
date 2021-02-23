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
    
    public GroundCheck check;

    [Header("Enums")]
    public Hand movementHand;
    public Hand rotationHand;

    [Header("Values")]
    public float speed;
    public float rotationSpeed;

    [Header("Rigidbodies")]
    public Rigidbody locoSphere;
    public Rigidbody rigRB;

    [Header("Transforms")]
    public Transform cam;
    public Transform rig;
    public Transform offset;
    public Transform locc;

    void FixedUpdate()
    {
        HandleMovement();
        HandleRoomscaleMovement();
        HandleRotation();
        //SlowMo();
    }

    void HandleRoomscaleMovement()
    {
        //offset.localPosition = new Vector3(0, .4f, 0);

        
        var delta = cam.transform.position - rig.transform.position;
        delta.y = 0f;
        var direction = delta.normalized;
        var magnitude = delta.magnitude;
        float CameraMoveThreshold = 0.1f;
        if (magnitude > CameraMoveThreshold)
        {
            //rigRB.MovePosition(new Vector3(locc.position.x, rigRB.position.y, locc.position.z));
            locoSphere.MovePosition(new Vector3(locc.position.x, locoSphere.position.y, locc.position.z));
        }
    }

    void HandleMovement()
    {
        if (check.isGrounded)
        {
            if (movementHand == Hand.Left)
            {
                locoSphere.AddTorque(cam.TransformDirection(new Vector3(leftHand.joystick.y, 0, -leftHand.joystick.x) * speed));
            }

            if (movementHand == Hand.Right)
            {
                locoSphere.AddTorque(cam.TransformDirection(new Vector3(rightHand.joystick.y, 0, -rightHand.joystick.x) * speed));
            }
        }

        if (!check.isGrounded)
        {
            if (movementHand == Hand.Left)
            {
                locoSphere.AddForce(cam.TransformDirection(new Vector3(leftHand.joystick.y, 0, -leftHand.joystick.x) * speed));
            }

            if (movementHand == Hand.Right)
            {
                locoSphere.AddForce(cam.TransformDirection(new Vector3(rightHand.joystick.y, 0, -rightHand.joystick.x) * speed));
            }
        }
    }

    void HandleRotation()
    {
        if (rotationHand == Hand.Left)
        {
            rig.Rotate(0, leftHand.joystick.x * rotationSpeed, 0);
        }

        if (rotationHand == Hand.Right)
        {
            rig.Rotate(0, rightHand.joystick.x * rotationSpeed, 0);
        }
    }

    void SlowMo()
    {
        if (leftHand.primary)
        {
            Time.timeScale = 0.1f;
        }

        if (!leftHand.primary)
        {
            Time.timeScale = 1;
        }
    }
}
