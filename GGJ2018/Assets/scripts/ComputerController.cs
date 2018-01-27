using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour {
    private bool connected;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsConnected()
    {
        return connected;
    }

    public void ChangeConnection(bool connection)
    {
        connected = connection;
    }

}
