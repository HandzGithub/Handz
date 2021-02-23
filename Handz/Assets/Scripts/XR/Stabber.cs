using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis
{
    X = 1,
    Y = 2,
    Z = 3,
}

public class Stabber : MonoBehaviour
{
    public bool disableCollider;

    public Collider blade;

    public GameObject parent;

    public ConfigurableJoint joint;

    public Transform anchorPoint;

    public float mass;
    public float savedMass;

    public float driveF;
    public float damperF;

    public bool touch;

    public Axis axis;

    JointDrive drive = new JointDrive();

    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (parent.GetComponent<Rigidbody>().velocity.x > 0.1f || parent.GetComponent<Rigidbody>().velocity.y > 0.1f || parent.GetComponent<Rigidbody>().velocity.z > 0.1f)
        {
            if (disableCollider)
            {
                blade.enabled = false;
            }
            parent.GetComponent<Rigidbody>().mass = mass;
            joint = parent.AddComponent<ConfigurableJoint>();
            joint.xMotion = joint.zMotion = joint.yMotion = ConfigurableJointMotion.Locked;
            joint.angularXMotion = joint.angularYMotion = joint.angularZMotion = ConfigurableJointMotion.Locked;
            drive.positionSpring = driveF;
            drive.positionDamper = damperF;

            if (axis == Axis.X)
            {
                joint.xDrive = drive;
                joint.xMotion = ConfigurableJointMotion.Free;
            }

            if (axis == Axis.Y)
            {
                joint.yDrive = drive;
                joint.yMotion = ConfigurableJointMotion.Free;
            }

            if (axis == Axis.Z)
            {
                joint.zDrive = drive;
                joint.zMotion = ConfigurableJointMotion.Free;
            }

            if (other.GetComponent<Rigidbody>())
            {
                joint.connectedBody = other.GetComponent<Rigidbody>();
                //blade.isTrigger = true;
            }

            if (other.GetComponentInParent<Rigidbody>())
            {
                joint.connectedBody = other.GetComponentInParent<Rigidbody>();
                //.isTrigger = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Destroy(joint);
        blade.enabled = true;
        parent.GetComponent<Rigidbody>().mass = savedMass;
        Debug.Log("Yessir");
        //blade.isTrigger = false;
    }
}
