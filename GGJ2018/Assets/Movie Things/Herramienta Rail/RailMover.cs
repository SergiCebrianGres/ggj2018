using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public class RailMover : MonoBehaviour
{
    public Rail rail;
    public moveMode mode;
    public float speed = 2.5f;
    public bool isReversed;
    public bool isLooping;
    public bool pingPong;

    private int currentSeg = 1;
    private float transition;
    private bool isCompleted;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (!rail) return;
	    if (!isCompleted) Play(!isReversed);
	    if (isCompleted)
	    {
	        rail = rail.nextRail;
            currentSeg = 1;
	        isCompleted = false;
	    }
	}

    private void Play(bool forward = true)
    {
        float m = (rail.nodes[currentSeg + 1].position - rail.nodes[currentSeg].position).magnitude;
        float s = (Time.deltaTime * 1 / m) * speed;


        transition += forward ? s : -s ;
        if (transition > 1)
        {
            transition = 0;
            currentSeg++;
            if (currentSeg == rail.nodes.Length - 1)
            {
                if (isLooping)
                {
                    if (pingPong)
                    {
                        transition = 1;
                        currentSeg = rail.nodes.Length - 2;
                        isReversed = !isReversed;
                    }
                    else
                    {
                        currentSeg = 1;
                    }
                }
                else
                {
                    isCompleted = true;
                    return;
                }
            }
        }
        else if (transition < 0)
        {
            transition = 1;
            currentSeg--;
            if (currentSeg == 0)
            {
                if (isLooping)
                {
                    if (pingPong)
                    {
                        transition = 0;
                        currentSeg = 1;
                        isReversed = !isReversed;
                    }
                    else
                    {
                        currentSeg = rail.nodes.Length - 2;
                    }
                }
                else
                {
                    isCompleted = true;
                    return;
                }
            }
        }

        transform.position = rail.PositionOnRail(currentSeg, transition, mode);

        //Comment this line for VR(?)
        transform.rotation = rail.Orientation(currentSeg, transition);
    }
}
