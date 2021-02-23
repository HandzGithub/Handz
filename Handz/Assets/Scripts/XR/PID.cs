using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PID : MonoBehaviour
{
    private Vector3 lastError = new Vector3();
    private Vector3 integral = new Vector3();

    //PID parameters
    public float P = 0f;
    public float I = 0f;
    public float D = 0f;
    public float frequency;
    public float damping;
    public float forceAmount;
    public float climbForceAmount;

    public Transform target;
    public Rigidbody rb;
    public Rigidbody lrb;

    public Vector3 torque;
    public Vector3 force;

    public Vector3 error;

    float kp;
    float kd;

    public Vector3 GetOutput(Vector3 error)
    {
        Vector3 derivative = (error - lastError) / Time.fixedDeltaTime;
        integral += error * Time.fixedDeltaTime;

        lastError = error;

        return P * error + I * integral + D * derivative;
    }

    public void Update()
    {
        //Debug.Log("force" + force);
        //Debug.Log("torque" + torque);
        HandlePID();
        force = GetOutput(error);

        rb.AddForce(force);
        rb.AddTorque(torque);

        lrb.AddForce(-force / climbForceAmount);
    }

    public void HandlePID()
    {
        error = target.position - transform.position;

        float dt = Time.fixedDeltaTime;
        //float g = 1 / (1 + kd * dt + kp * dt * dt);
        //float ksg = kp * g;
        //float kdg = (kd + kp * dt) * g;
        //Vector3 Pt0 = transform.position;
        //Vector3 Vt0 = hand.rb.velocity;
        //Vector3 F = (target.position - Pt0) * ksg + (-target.position - Vt0) * kdg;

        force = GetOutput(error);

        //Torque
        Quaternion desiredRotation = target.rotation;
        kp = (6f * frequency) * (6f * frequency) * 0.25f;
        kd = 4.5f * frequency * damping;

        Vector3 x;
        float xMag;
        Quaternion q = desiredRotation * Quaternion.Inverse(transform.rotation);
        // Q can be the-long-rotation-around-the-sphere eg. 350 degrees
        // We want the equivalant short rotation eg. -10 degrees
        // Check if rotation is greater than 190 degees == q.w is negative
        if (q.w < 0)
        {
            // Convert the quaterion to eqivalent "short way around" quaterion
            q.x = -q.x;
            q.y = -q.y;
            q.z = -q.z;
            q.w = -q.w;
        }
        q.ToAngleAxis(out xMag, out x);
        x.Normalize();
        x *= Mathf.Deg2Rad;
        Vector3 pidv = kp * x * xMag - kd * GetComponent<Rigidbody>().angularVelocity;
        Quaternion rotInertia2World = GetComponent<Rigidbody>().inertiaTensorRotation * transform.rotation;
        pidv = Quaternion.Inverse(rotInertia2World) * pidv;
        pidv.Scale(GetComponent<Rigidbody>().inertiaTensor);
        pidv = rotInertia2World * pidv;
        torque = pidv;
    }
}