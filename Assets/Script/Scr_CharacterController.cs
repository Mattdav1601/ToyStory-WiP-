using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CharacterController : MonoBehaviour
{

    public static Scr_CharacterController inst;

    Camera playerCamera;

    public GameObject viewingObject;


    private void Awake()
    {
        inst = this;
        playerCamera = GetComponent<Camera>();
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        VisionScan();
	}

    public void VisionScan()
    {
        viewingObject = null;

        Ray Outlook = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit Hitinfo;
        if (Physics.Raycast(Outlook, out Hitinfo))
        {
            if (Hitinfo.collider.gameObject.tag == "Toy")
            {
                viewingObject = Hitinfo.collider.gameObject;
                if (viewingObject.GetComponent<ToyScript>().toyState == ToyScript.ToyState.Default)
                {
                    viewingObject.GetComponent<ToyScript>().UpdateToyState(ToyScript.ToyState.Viewing);
                }  
            } 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(playerCamera.transform.position, playerCamera.transform.position + playerCamera.transform.forward * 100);
    }
}
