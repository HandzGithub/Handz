using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandzRigTwo : MonoBehaviour
{
    public HandzInputWrapper leftHand;
    public HandzInputWrapper rightHand;
    public GroundCheck check;
    public CharacterController characterController;

    [Header("Enums")]
    public Hand movementHand;
    public Hand rotationHand;

    [Header("Values")]
    public float speed;
    public float rotationSpeed;
    public float grav;

    [Header("Transforms")]
    public Transform rig;
    public Transform cam;

    void FixedUpdate()
    {
        HandleRoomscale();
        HandleCharacter();
        HandleRotation();
    }

    void HandleCharacter()
    {

        if (movementHand == Hand.Left)
        {
            characterController.Move(cam.TransformDirection(new Vector3(leftHand.joystick.x, 0, leftHand.joystick.y) * speed));
        }

        if (movementHand == Hand.Right)
        {
            characterController.Move(cam.TransformDirection(new Vector3(rightHand.joystick.x, 0, rightHand.joystick.y) * speed));
        }

        characterController.Move(new Vector3(0, grav, 0));
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

    void HandleRoomscale()
    {
        characterController.height = cam.transform.position.y;
        Vector3 center = transform.InverseTransformPoint(cam.position);
        characterController.center = new Vector3(center.x, characterController.height / 2, center.z);
    }
}
