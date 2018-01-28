using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IndependentMovementController : MonoBehaviour {
    public Vector3 target;
    public float speed = 10f;
    private bool shouldMove;
    NavMeshAgent agent;
    GameObject tt;
    Animator anim;

    void Start()
    {
        shouldMove = false;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update(){
        if (shouldMove)
        {
            agent.SetDestination(target);
            anim.SetFloat("Forward", agent.velocity.magnitude / (agent.speed*2));
            if (pathComplete())
            {
                shouldMove = false;
                GetComponent<IAController>().NotifyEndOfRoad();
                agent.updatePosition = false;
                agent.updateRotation = false;

            }

        } else
        {
            anim.SetFloat("Forward", 0f);
        }
    }


    public void GoToFood()
    {
        shouldMove = true;
        agent.updatePosition = true;
        agent.updateRotation = true;
        target = GameObject.Find("Food").transform.position;

    }

    public void GoToBathroom()
    {
        shouldMove = true;
        agent.updatePosition = true;
        agent.updateRotation = true;
        target = GameObject.Find("Bathroom").transform.position;
    }

    public void GoToTable(Vector3 target)
    {
        shouldMove = true;
        agent.updatePosition = true;
        agent.updateRotation = true;
        this.target = target;
        
    }

    protected bool pathComplete()
    {
        if (Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }

        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cable")
        {
            GameController.instance.SteppedOn(other.transform.parent.parent.GetComponent<Rope1>());
        }
    }

}
