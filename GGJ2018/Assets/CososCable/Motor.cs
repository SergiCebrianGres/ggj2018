using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    private Rigidbody rb;
    public bool grabbed = false;
    public float speed = 100f;
    private float lerpSpeed = .45f;

    public Transform target;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        if (grabbed)
        {
            if ((target.position - transform.position).sqrMagnitude > 1.4) UnGrab();
            else
            {
                transform.position = Vector3.Lerp(transform.position, target.position, lerpSpeed);
            }
        }
    }

    public void Grab()
    {
        grabbed = true;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void UnGrab()
    {
        grabbed = false;
        rb.useGravity = true;
        rb.isKinematic = false;
    }
}
