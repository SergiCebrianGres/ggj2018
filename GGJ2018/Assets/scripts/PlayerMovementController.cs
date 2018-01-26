using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
    private int speed = 10;
    private Vector3 forward, right;
    private bool moving;

	// Use this for initialization
	void Start () {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        right = Camera.main.transform.right;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

    }
	
	// Update is called once per frame
	void Update () {

        if (moving)
        {
            Vector3 move = Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward;
            move = move.normalized;
            transform.position += move * speed * Time.deltaTime;
        }
    }

    public void Move()
    {
        moving = true;
    }
}
