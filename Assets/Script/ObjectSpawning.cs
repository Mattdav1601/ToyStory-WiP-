using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawning : MonoBehaviour
{

    #region Tweaking Variables 
    public int NumberOfToys;
    #endregion

    #region Tracking Varibles
    public List<Transform> SpawnLocations;
    #endregion

    #region Object References
    GameObject[] SpawnPoints;
    public List<GameObject> Toys;
    #endregion

    #region Component References
    #endregion

    // Use this for initialization
    void Start () {

        SpawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");

        foreach(GameObject Node in SpawnPoints)
        {
            SpawnLocations.Add(Node.transform);
        }

        foreach(GameObject Node in SpawnPoints)
        {
            Destroy(Node);
        }

        SpawnObjects();
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void SpawnObjects()
    {
        List<int> UsedIndex = new List<int>();
        int ObjectsSpawnedTracker = 0;

        while(ObjectsSpawnedTracker < NumberOfToys)
        {
            int index = Random.Range(0, SpawnLocations.Count);

            if(!UsedIndex.Contains(index))
            {
                UsedIndex.Add(index);
                SpawnUsableToy(index);
                ObjectsSpawnedTracker++;
            }
        }
    }

    void SpawnUsableToy(int _SpawnIndex)
    {
        //Spawn a random toy from the list
        GameObject usableToy = Instantiate<GameObject>(Toys[Random.Range(0, Toys.Count)]);
        usableToy.transform.parent = transform;
        //Make the toys position equal to the appropriate index previous determined
        usableToy.transform.position = SpawnLocations[_SpawnIndex].position;
        usableToy.transform.rotation = SpawnLocations[_SpawnIndex].rotation;
    }
}
