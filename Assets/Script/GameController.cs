using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void WinGame()
    {

    }

    public void LoseGame()
    {

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
}
