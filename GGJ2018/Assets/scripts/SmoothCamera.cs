using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {
    public Transform lookAt;

    public bool smooth = true;
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 5, -10);
	// Use this for initialization
	void Start () {
        transform.position = lookAt.position + offset;
        transform.LookAt(lookAt);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 desired = lookAt.position + offset;

        if (smooth)
        {
            transform.position = Vector3.Lerp(transform.position, desired, smoothSpeed);
        }
        else
        {
            transform.position = desired;
        }
	}
}
