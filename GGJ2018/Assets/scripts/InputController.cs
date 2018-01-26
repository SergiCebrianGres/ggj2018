using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    GameController gameController;

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.Find("Main Camera").GetComponent<GameController>();

    }

    // Update is called once per frame
    void Update()
    {

        if (gameController.GetStatus() == GameController.GameControllerStatus.FREE_MOV)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                gameController.Move();
            }
        }
    }
}