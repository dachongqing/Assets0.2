using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMoveManger : MonoBehaviour {

   // public Camera camera;
    // We need to actually hit an object
   // RaycastHit hitt = new RaycastHit();

    private RoundController roundController;

    // Use this for initialization
    void Start () {
        roundController = FindObjectOfType<RoundController>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(1))
        {
            Character player =  roundController.getPlayerChara();
            if (typeof(NPC).IsAssignableFrom(player.GetType()))
            {           
                NPC npc = (NPC)player;
                // Debug.Log(Input.mousePosition);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           
                 npc.CharaMoveByMouse(ray.origin);
             
            }

        }
      
    }
}
