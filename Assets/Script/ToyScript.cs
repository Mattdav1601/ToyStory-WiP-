using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyScript: MonoBehaviour {
    public Material ToyDefault;
    public Material ToyView;
    public Material ToyComplete;

    public float ViewPoints;

    public ToyState toyState;


    public enum ToyState
    {
        Default,
        Viewing,
        Viewed
    }


    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {

        if(toyState == ToyState.Viewing)
        {
            ToyPoints();
        }

        if(Scr_CharacterController.inst.viewingObject != this.gameObject)
        {
            if(toyState == ToyState.Viewing)
            {
                UpdateToyState(ToyState.Default);
                
            }
        }   
	}

    public void UpdateToyState(ToyState _newToyState)
    {
        toyState = _newToyState;

        switch(toyState)
        {
            case ToyState.Default:

                this.gameObject.GetComponent<MeshRenderer>().material = ToyDefault;

                break;
            case ToyState.Viewed:
                

                break;
            case ToyState.Viewing:
                this.gameObject.GetComponent<MeshRenderer>().material = ToyView;

                break;

        }


    }
    public void ToyPoints()
    {
        ViewPoints = ViewPoints + Time.deltaTime;
        Debug.Log(ViewPoints);

        if (ViewPoints >= 3.0f)
        {
            toyState = ToyState.Viewed;
            this.gameObject.GetComponent<MeshRenderer>().material = ToyComplete;
            Debug.Log("Complete");
        }
    }
}
