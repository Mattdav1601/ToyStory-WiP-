using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public float GameTime;

    public static GameController inst;

    public float toysWitnessed;


    public float toysGoalOverride;
    public float toysGoal;


    private void Awake()
    {
        inst = this;
    }

    // Use this for initialization
    void Start ()
    {
        ScanForToys();
        StartCoroutine(CreateGameTimer());
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void EndGame(bool winState)
    {
        //If they win
        if(winState == true)
        {

        }
        //If they lose
        else
        {

        }
        Debug.Log(winState);
    }

     public void UpdateToysSeen(int adjustment)
    {
        toysWitnessed += adjustment;
        if(toysWitnessed == toysGoal)
        {

        }
    }

    void ScanForToys()
    {
        if (toysGoalOverride != 0)
        {
            toysGoal = toysGoalOverride;
        }
        else
        {
            toysGoal = GameObject.FindGameObjectsWithTag("Toy").Length;
        }
    }

    IEnumerator CreateGameTimer()
    {
        float gameTimer = GameTime;

        while(gameTimer > 0)
        {
            //Do stuff
            gameTimer -= Time.deltaTime;

            //Checks if the player has won before the timer has run out
            if(toysWitnessed >= toysGoal)
            {
                EndGame(true);
                break;
            }

            yield return null;
        }

        //Have you reached the end of the time, if so lose
        if(gameTimer <= 0)
        {
            EndGame(false);
        }
        
    }
}
