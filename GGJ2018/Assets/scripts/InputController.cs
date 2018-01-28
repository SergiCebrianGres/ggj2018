using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    GameController gameController;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (GameController.instance.status == GameController.GameControllerStatus.FREE_MOV) GameController.instance.tryGrab();
            if (GameController.instance.status == GameController.GameControllerStatus.HOLDING) GameController.instance.tryConnect();
        }
    }
}