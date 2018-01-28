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

    GameObject happybar;


    private float luck;
    private float timer;
    private bool gameOver;
    private double happiness;
    private float accTime;


    private int MAX_COMP = 24;

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

    private void Start()
    {
        luck = UnityEngine.Random.Range(5f, 10f);
        timer = 0;
        Camera.main.GetComponent<AudioSource>().loop = true;
        gameOver = false;
        happiness = 1d;

        happybar = GameObject.Find("HappyBar");
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
                if ((r.MainNode.transform.position-player.transform.position).sqrMagnitude < .8f)
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
                var audio = closestComputer.GetComponent<AudioSource>();
                audio.clip = Camera.main.GetComponent<AudioManager>().GetConnexio();
                audio.Play();
                happiness += (double)((1f / 24));
                accTime = (float)(0.9 * accTime);
                if (happiness > 1)
                {
                    happiness = 1;
                }
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
            var audio = r.connectedComputer.GetComponent<AudioSource>();
            audio.clip = Camera.main.GetComponent<AudioManager>().GetDesconnexio();
            audio.Play();
            r.disconnect();


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




    void Update()
    {
        if (!gameOver)
        {
            if (timer >= luck)
            {
                var audio = Camera.main.transform.GetChild(0).GetComponent<AudioSource>();
                audio.clip = Camera.main.GetComponent<AudioManager>().GetQueixa();
                audio.Play();
                luck = UnityEngine.Random.Range(15f, 20f);
                timer = 0;
            }

            timer += Time.deltaTime;

            if (connectedComputers.Count < MAX_COMP)
            {
                happiness -= (Mathf.Log(1f + 0.0005f * accTime) * (float)(MAX_COMP - connectedComputers.Count)) / (1000 + 0.1 * accTime + MAX_COMP);
            }
            
            if (happiness <= 0)
            {
                gameOver = true;

            }
            if(happybar == null)
            {
                happybar = GameObject.Find("HappyBar");
            }
            var sc = happybar.GetComponent<HappyBar>();
            if(sc != null)
            {
                sc.RepaintHappiness(happiness);
            }
        }
        accTime += Time.deltaTime;
        
    }
}
