using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public HandzInputWrapper input;
    public FingerCurls fingerCurls;

    public GameObject currentObject;

    public Collider palm;

    public Transform anchor;

    public float grabRadius;

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(anchor.position, grabRadius);
        foreach (var hitCollider in hitColliders)
        {
            currentObject = hitCollider.gameObject;
            if (input.grip.active)
            {
                GrabLogic();
            }
        }
    }

    void GrabLogic()
    {
        transform.position = palm.ClosestPoint(transform.position);
        
        gameObject.AddComponent<FixedJoint>();
        if (currentObject.GetComponent<Rigidbody>())
        {
            gameObject.GetComponent<FixedJoint>().connectedBody = currentObject.GetComponent<Rigidbody>();
        }
    }
}
