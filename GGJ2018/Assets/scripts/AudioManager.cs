using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioClip[] queixes;
    public AudioClip[] connexions;
    public AudioClip[] desconnexions;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public AudioClip GetQueixa()
    {
        return queixes[Random.Range(0, queixes.Length)];
    }

    public AudioClip GetConnexio()
    {
        return connexions[Random.Range(0, connexions.Length)];
    }

    public AudioClip GetDesconnexio()
    {
        return desconnexions[Random.Range(0, desconnexions.Length)];
    }
}
