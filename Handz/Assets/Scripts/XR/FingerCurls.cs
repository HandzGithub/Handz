using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FingerCurls : MonoBehaviour
{

    public HandzInputWrapper input;

    public bool canCurl;

    [Header("Values")]
    public bool moveThumb;

    [Range(0, 1)]
    public float thumbMoveAmount;
    public float thumbMoveAmp;
    public float thumbLimit;

    [Range(0, 1)]
    public float indexTrigger;

    [Range(0, 1)]
    public float indexMiddle;

    [Range(0, 1)]
    public float indexRing;

    [Range(0, 1)]
    public float indexPinky;

    [Header("Bones")]
    public Transform thumbBase1;
    public Transform thumbBase2;
    public Transform thumbMid;
    public Transform thumbTip;

    public Transform indexBase;
    public Transform indexMid;
    public Transform indexTip;

    public Transform middleBase;
    public Transform middleMid;
    public Transform middleTip;

    public Transform ringBase;
    public Transform ringMid;
    public Transform ringTip;

    public Transform pinkyBase;
    public Transform pinkyMid;
    public Transform pinkyTip;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        indexTrigger = input.trigger.value;
        indexMiddle = input.grip.value;
        indexRing = input.grip.value;
        indexPinky = input.grip.value;

        indexBase.rotation = new Quaternion(0, 0, Mathf.Clamp(0, -90, 0), 0);
        indexMid.rotation = new Quaternion(0, 0, Mathf.Clamp(0, -90, 0), 0);
        indexTip.rotation = new Quaternion(0, 0, Mathf.Clamp(0, -90, 0), 0);
        indexBase.Rotate(0, 0, -indexTrigger * 90);
        indexMid.Rotate(0, 0, -indexTrigger * 90);
        indexTip.Rotate(0, 0, -indexTrigger * 90);

        if (canCurl)
        {
            middleBase.rotation = new Quaternion(0, 0, Mathf.Clamp(0, -90, 0), 0);
            middleMid.rotation = new Quaternion(0, 0, Mathf.Clamp(0, -90, 0), 0);
            middleTip.rotation = new Quaternion(0, 0, Mathf.Clamp(0, -90, 0), 0);
            middleBase.Rotate(0, 0, -indexMiddle * 90);
            middleMid.Rotate(0, 0, -indexMiddle * 90);
            middleTip.Rotate(0, 0, -indexMiddle * 90);

            ringBase.rotation = new Quaternion(0, 0, Mathf.Clamp(0, -90, 0), 0);
            ringMid.rotation = new Quaternion(0, 0, Mathf.Clamp(0, -90, 0), 0);
            ringTip.rotation = new Quaternion(0, 0, Mathf.Clamp(0, -90, 0), 0);
            ringBase.Rotate(0, 0, -indexRing * 90);
            ringMid.Rotate(0, 0, -indexRing * 90);
            ringTip.Rotate(0, 0, -indexRing * 90);

            pinkyBase.rotation = new Quaternion(0, 0, Mathf.Clamp(25, -90, 0), 0);
            pinkyMid.rotation = new Quaternion(0, 0, Mathf.Clamp(0, -90, 0), 0);
            pinkyTip.rotation = new Quaternion(0, 0, Mathf.Clamp(0, -90, 0), 0);
            pinkyBase.Rotate(25, 0, -indexPinky * 90);
            pinkyMid.Rotate(0, 0, -indexPinky * 90);
            pinkyTip.Rotate(0, 0, -indexPinky * 90);
        }
    }
}
