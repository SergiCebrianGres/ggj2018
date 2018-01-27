using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
    private float speed = 2f;
    private Vector3 forward, right;
    private bool moving;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update ()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        right = Camera.main.transform.right;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;
        Vector3 move = Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward;
        move = move.normalized;
        transform.position += move * speed * Time.deltaTime;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Computer")
        {

        }
        if (other.tag == "Switch")
        {
            GameController.instance.setClosestSwitch(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Computer")
        {

        }
        if (other.tag == "Switch")
        {
            GameController.instance.setClosestSwitch(null);
        }
    }
    
}
