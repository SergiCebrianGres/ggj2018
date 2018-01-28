using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope1 : MonoBehaviour
{
    private LineRenderer lr;

    public Rigidbody player;
    public Transform playerHand;
    public GameObject MainSwitch;
    public GameObject cablePiece;

    public Material onlineMat;
    public Material brokenMat;
    public Material looseMat;

    public bool online = false;
    public bool broken = false;

    public GameObject MainNode;
    private GameObject LastNode;
    private HingeJoint worldHinge;
    public GameObject connectedComputer;
    private Motor motor;

    public int maxLength = 100;
    public int Length = 0;

    internal Rigidbody RBody;
    internal void Start()
    {
        //Init();
    }

    public void Init()
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
                hinge.useSpring = true;
                hinge.enableCollision = true;
                hinge.enablePreprocessing = false;
            }
        }
        lr.positionCount += 1;
        Length = 2;
        lr.SetPosition(2, MainSwitch.transform.position);
    }

    public void AddCable()
    {
        if (Length <= maxLength)
        {
            lr.positionCount += 1;

            Transform t = LastNode.transform;
            GameObject go = Instantiate(cablePiece, MainSwitch.transform.position, Quaternion.identity);//t.rotation);

            HingeJoint hinge = go.AddComponent<HingeJoint>();
            hinge.connectedBody = LastNode.GetComponent<Rigidbody>();
            hinge.useSpring = true;
            /*
            var s = hinge.spring;
            s.spring = 1;
            s.damper = 10;
            hinge.spring = s;
            */
            hinge.enableCollision = true;
            hinge.enablePreprocessing = false;

            go.transform.SetParent(transform);
            
            LastNode = go;
            lr.SetPosition(lr.positionCount - 1, MainSwitch.transform.position);
            Length++;
        }
    }

    internal void LateUpdate()
    {
        if ((LastNode.transform.position - MainSwitch.transform.position).sqrMagnitude > .15f)
        {
            AddCable();
        }

        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform t = transform.GetChild(i);
            lr.SetPosition(i, t.position);

            if ( i > 0 && i < Length - 1)
            {
                float dist = (t.position - transform.GetChild(i + 1).position).sqrMagnitude;
                if ((motor.grabbed || motor.connected) && dist > 4f)
                {
                    if (motor.grabbed) motor.UnGrab();
                    if (motor.connected)
                    {
                        online = false;
                        motor.UnConnect();
                    }
                }
                if (dist > 49f)
                {
                    Destroy(this.gameObject);
                }
            }
        }

        if (broken) lr.material = brokenMat;
        else if (online) lr.material = onlineMat;
        else lr.material = looseMat;    
    }

    public void grab()
    {
        motor.target = playerHand.transform;
        motor.Grab();
    }

    public void connectTo(GameObject go)
    {
        online = true;
        connectedComputer = go;
        motor.target = go.transform;
        motor.UnGrab();
        motor.Connect();
    }

    public void disconnect()
    {
        online = false;
        connectedComputer = null;
        motor.UnConnect();
    }
}
