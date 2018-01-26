using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    private GameControllerStatus status;
    private GameObject dummy;

	// Use this for initialization
	void Start () {
        status = GameControllerStatus.FREE_MOV;
        dummy = GameObject.Find("Cube");
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public GameControllerStatus GetStatus()
    {
        return status;
    }

    public void Move()
    {
        Debug.Log(dummy);
        dummy.GetComponent<PlayerMovementController>().Move();
    }

    public enum GameControllerStatus{
        FREE_MOV, ROPE
    }
}
