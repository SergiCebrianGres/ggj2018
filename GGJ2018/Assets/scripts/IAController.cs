using System.Collections; using System.Collections.Generic; using UnityEngine;  public class IAController : MonoBehaviour {      private IAState state;     private IAState previousState;     private IndependentMovementController movement;     private int happiness;     private float hunger;     private float urge;     private float hungerStep;     private float urgeStep;     private Vector3 tablePosition;     private float timer;     private float foodTimer;     private float bathroomTimer;     private float maxHunger;     private float maxUrge;     private float hungerChance;     private float urgeChance;     private float wander;   	// Use this for initialization 	void Start () {         state = IAState.WAITING;         hungerStep = 1f;         urgeStep = 1f;         tablePosition = transform.position;         timer = 0f;         foodTimer = 0f;         bathroomTimer = 0f;         movement = GetComponent<IndependentMovementController>();         happiness = 100;         maxHunger = Random.Range(10f, 40f);         maxUrge = Random.Range(10f, 50f);         hungerChance = Random.Range(0.2f, 0.6f);         urgeChance = Random.Range(0.2f, 0.6f);         wander = 0f;         hunger = 0f;         urge = 0f;      } 	 	// Update is called once per frame 	void Update () {          switch (state)         {             case IAState.WAITING:                 float luck = Random.Range(0f, 1f);                 if (timer >= 1)                 {                     if (luck < hungerChance)                     {                     hunger += hungerStep;                     }                     if (luck > urgeChance)                     {                         urge += urgeStep;                     }                     if (hunger >= maxHunger)                     {                         movement.GoToFood();                         hunger = 0f;                         urge = 0f;                         state = IAState.TO_FOOD;                     }                     else if (urge >= maxUrge)                     {                         movement.GoToBathroom();                         urge = 0f;                         hunger = 0f;                         state = IAState.TO_BATHROOM;                     }                     timer = 0f;                 }                 timer += Time.deltaTime;                 break;             case IAState.TO_BATHROOM:                 break;             case IAState.TO_FOOD:                 break;             case IAState.TO_TABLE:                 movement.GoToTable(tablePosition);                 break;             case IAState.FOOD:                 foodTimer += Time.deltaTime;                 if (foodTimer >= 1)                 {                     state = IAState.TO_TABLE;                 }                 break;             case IAState.BATHROOM:                 bathroomTimer += Time.deltaTime;                 if (bathroomTimer >= 1)                 {                     state = IAState.TO_TABLE;                 }                 break;             case IAState.WANDER:                 wander += Time.deltaTime;                 if(wander >= 3)
                {
                    state = previousState;
                    wander = 0;
                }                  break;         } 	}      public void NotifyEndOfRoad()     {         switch (state)         {             case IAState.WAITING:                 break;             case IAState.TO_BATHROOM:                 state = IAState.BATHROOM;                 BroadcastWait();                 bathroomTimer = 0f;                 break;             case IAState.TO_FOOD:                 state = IAState.FOOD;                 foodTimer = 0f;                 break;             case IAState.TO_TABLE:                 state = IAState.WAITING;                 break;             case IAState.FOOD:                 break;         }     }       private void BroadcastWait()
    {
        Debug.Log("WAIT!!!");
        GameObject[] objects = GameObject.FindGameObjectsWithTag("IA");
        switch(state){
            case IAState.FOOD:
                foreach (GameObject minion in objects)
                {
                    var cc = minion.GetComponent<IAController>();
                    if (cc != null) {
                        cc.DecreaseHunger();
                }
                }
                break;
            case IAState.BATHROOM:
                foreach (GameObject minion in objects)
                {
                    var cc = minion.GetComponent<IAController>();
                    if (cc != null) {
                        cc.DecreaseUrge();
                    }
                }
                break;
        }

    }      public void DecreaseHunger()
    {
        hunger -= Random.Range(1f, 5f);
    }      public void DecreaseUrge()
    {
        urge -= Random.Range(1f, 5f);
    }      public void TryToWait()
    {
        previousState = state;
        state = IAState.WANDER;
    }       public enum IAState     {         WAITING, TO_BATHROOM, TO_FOOD, TO_TABLE, BATHROOM, FOOD, WANDER     } } 