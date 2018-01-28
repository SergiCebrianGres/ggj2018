using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private bool stop;

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
        stop = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString("00");
            string seconds = ((int)t % 60).ToString("00");

            timerText.text = minutes + ":" + seconds;
        }
    }


    public void Run()
    {
        stop = false;
    }

    public void Stop()
    {
        stop = true;
    }

}
