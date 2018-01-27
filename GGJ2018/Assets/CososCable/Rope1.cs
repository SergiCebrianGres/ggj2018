using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope1 : MonoBehaviour {
    private LineRenderer lr;

    public bool connected;
    public bool broken;

    public Material looseMat;
    public Material brokenMat;
    public Material onlineMat;
    
    public Transform playerHand;
    public GameObject MainSwitch;
    public GameObject cablePiece;

    public GameObject MainNode;
    private GameObject LastNode;
    private HingeJoint worldHinge;
    private Motor motor;

    public int maxLength = 100;
    public int Length = 0;

    internal Rigidbody RBody;
    internal void Start()
    {
        lr = GetComponent<LineRenderer>();

        int childCount = transform.childCount;
        Length = childCount;

        MainNode = transform.GetChild(0).gameObject;
        motor = MainNode.GetComponent<Motor>();
        LastNode = transform.GetChild(childCount - 1).gameObject;
        for (int i = 0; i < childCount; i++)
        {
            Transform t = transform.GetChild(i);
            lr.SetPosition(i, t.position);
            if (i > 0)
            {
                HingeJoint hinge = t.gameObject.AddComponent<HingeJoint>();
                hinge.connectedBody = transform.GetChild(i - 1).GetComponent<Rigidbody>();
                //hinge.useSpring = true;
                hinge.enableCollision = true;
                /*
                hinge.useLimits = true;
                var lim = hinge.limits;
                lim.contactDistance = 100;
                lim.max = 45;
                hinge.limits = lim;
                //hinge.enablePreprocessing = false;

                /*
                SpringJoint spring = t.gameObject.AddComponent<SpringJoint>();
                spring.connectedBody = transform.GetChild(i - 1).GetComponent<Rigidbody>();
                spring.damper = 10;
                spring.spring = 75;
                spring.maxDistance = .01f;
                spring.tolerance = .025f;
                spring.enableCollision = true;
                //spring.enablePreprocessing = false;
                */
            }
        }
        lr.positionCount += 1;
        Length = 2;
        lr.SetPosition(2, MainSwitch.transform.position);
    }

    public void AddCable()
    {
        if (Length<=maxLength)
        { 
            lr.positionCount += 1;

            Transform t = LastNode.transform;
            GameObject go = Instantiate(cablePiece, MainSwitch.transform.position, t.rotation);
            
            HingeJoint hinge = go.AddComponent<HingeJoint>();
            hinge.connectedBody = LastNode.GetComponent<Rigidbody>();
            hinge.useSpring = true;
            hinge.enableCollision = true;
            hinge.enablePreprocessing = false;

            go.transform.SetParent(transform);

            /*
            worldHinge = go.AddComponent<HingeJoint>();
            worldHinge.useSpring = true;
            */
            LastNode = go;
            lr.SetPosition(lr.positionCount - 1, MainSwitch.transform.position);
            Length++;
        }
    }   

    internal void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            grab();
        }

        if ((LastNode.transform.position-MainSwitch.transform.position).sqrMagnitude > .2f)
        {
            AddCable();
        }

        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform t = transform.GetChild(i);
            lr.SetPosition(i, t.position);
            if (motor.grabbed && i > 0 && i < Length - 1)
            {
                if ((t.position - transform.GetChild(i + 1).position).sqrMagnitude > 4f) motor.UnGrab();
            }
        }
        if (connected) lr.material = onlineMat;
        else if (broken) lr.material = brokenMat;
        else lr.material = looseMat;
    }

    internal void grab()
    {
        motor.target = playerHand.transform;
        motor.Grab();
    }
}
