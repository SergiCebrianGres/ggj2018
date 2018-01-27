using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    private GameControllerStatus status;
    private GameObject closestSwitch;
    public GameObject player;
    public Transform playerHand;

    public GameObject mainSwitch;
    public GameObject cablePrefab;
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
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public GameControllerStatus GetStatus()
    {
        return status;
    }

    public enum GameControllerStatus{
        FREE_MOV, HOLDING
    }

    public void setClosestSwitch(GameObject go)
    {
        closestSwitch = go;
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
                if ((r.MainNode.transform.position-player.transform.position).sqrMagnitude < 1)
                {
                    found = true;
                    r.grab();
                }
            }

            if (!found && closestSwitch != null)
            {
                GameObject go = Instantiate(cablePrefab, closestSwitch.transform.position, Quaternion.identity);
                Rope1 r = go.GetComponent<Rope1>();
                cables.Add(r);
                r.MainSwitch = closestSwitch;
                r.playerHand = playerHand;
            }
        }
    }
}
