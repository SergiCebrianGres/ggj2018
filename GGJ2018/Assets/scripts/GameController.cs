using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameControllerStatus status;


    private GameObject closestSwitch;
    private GameObject closestComputer;
    private Rope1 grabbedRope;
    public GameObject player;
    public Transform playerHand;
    public float probBreak = 0.03f;
    
    public GameObject cablePrefab;

    public List<GameObject> connectedComputers;
    public List<Rope1> cables;
    public static GameController instance = null;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;
        
        else if (instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
        InitGame();
    }


    // Use this for initialization
    private void InitGame () {
        status = GameControllerStatus.FREE_MOV;
        foreach (var r in cables)
        {
            r.Init();
        }
	}

    public void tryGrab()
    {
        if (status == GameControllerStatus.FREE_MOV)
        {
            bool found = false;
            int i = 0;
            while (!found && i < cables.Count)
            {
                Rope1 r = cables[i];
                if ((r.MainNode.transform.position-player.transform.position).sqrMagnitude < .75f)
                {
                    found = true;
                    r.grab();
                    status = GameControllerStatus.HOLDING;
                    grabbedRope = r;
                }
                i++;
            }

            if (!found && closestSwitch != null)
            {
                GameObject go = Instantiate(cablePrefab, closestSwitch.transform.position, Quaternion.identity);
                Rope1 r = go.GetComponent<Rope1>();
                r.MainSwitch = closestSwitch.gameObject;
                r.playerHand = playerHand;
                r.Init();
                cables.Add(r);  
                r.grab();
                grabbedRope = r;
                status = GameControllerStatus.HOLDING;
            }
        }
    }

    public void tryConnect()
    {
        if (status == GameControllerStatus.HOLDING)
        {
            if (closestComputer != null && !connectedComputers.Contains(closestComputer))
            {
                connectedComputers.Add(closestComputer);
                grabbedRope.connectTo(closestComputer);
                Camera.main.GetComponent<AudioManager>().GetConnexio();
            }
        } 
    }

    public void grabbedNull()
    {
        grabbedRope = null;
    }

    public void setClosestSwitch(GameObject go)
    {
        closestSwitch = go;
    }

    public void SteppedOn(Rope1 r)
    {
        if (r.online && UnityEngine.Random.Range(0.0f, 1.0f) < probBreak)
        {
            Debug.Log("Y voló");
            connectedComputers.Remove(r.connectedComputer);
            r.disconnect();
            Camera.main.GetComponent<AudioManager>().GetDesconnexio();

        }
    }

    public void setClosestComputer(GameObject go)
    {
        closestComputer = go;
    }

    public GameControllerStatus GetStatus()
    {
        return status;
    }

    public enum GameControllerStatus{
        FREE_MOV, HOLDING
    }
}
