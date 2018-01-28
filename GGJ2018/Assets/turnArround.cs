using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnArround : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.Rotate(Vector3.up, 180);
	}   
}
