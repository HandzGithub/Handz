using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public HandzInputWrapper input;
    public FingerCurls fingerCurls;

    public GameObject currentObject;

    public Collider palm;

    public GameObject[] colliders;

    public Transform anchor;

    public float grabRadius;

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(anchor.position, grabRadius);
        foreach (var hitCollider in hitColliders)
        {
            currentObject = hitCollider.gameObject;
            GrabLogic();
        }
    }

    void GrabLogic()
    {
        if (input.grip.active && currentObject != gameObject)
        {
            foreach (var col in colliders)
            {
                col.SetActive(false);
            }
            
            //transform.position = palm.ClosestPoint(currentObject.transform.position);

            if (!GetComponent<FixedJoint>())
            {
                gameObject.AddComponent<FixedJoint>();
            }
            if (currentObject.GetComponent<Rigidbody>())
            {
                gameObject.GetComponent<FixedJoint>().connectedBody = currentObject.GetComponent<Rigidbody>();
            }
        }

        if (!input.grip.active)
        {
            if (GetComponent<FixedJoint>())
            {
                Destroy(gameObject.GetComponent<FixedJoint>());
            }
            transform.position = transform.position;

            foreach (var col in colliders)
            {
                col.SetActive(true);
            }
        }


    }
}
