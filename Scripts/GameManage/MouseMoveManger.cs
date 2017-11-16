using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMoveManger : MonoBehaviour {

   // public Camera camera;
    // We need to actually hit an object
   // RaycastHit hitt = new RaycastHit();

    private RoundController roundController;

    private bool locked = false;

    // Use this for initialization
    void Start () {
        roundController = FindObjectOfType<RoundController>();

    }

    public void updateLock(bool locked) {
        this.locked = locked;
    }
	
	// Update is called once per frame
	void Update () {

        if (!locked) {
            Character player = roundController.getPlayerChara();
            if (typeof(NPC).IsAssignableFrom(player.GetType()))
            {
                NPC npc = (NPC)player;
                if (Input.GetMouseButton(1))
                { 
                    // Debug.Log(Input.mousePosition);
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);          
                    npc.CharaMoveByMouse(ray.origin);
             
                 }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    Debug.Log(" do left ");
                    npc.CharaMoveByKey("L");
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    Debug.Log(" do right ");
                    npc.CharaMoveByKey("R");

                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    Debug.Log(" do up ");
                    npc.CharaMoveByKey("U");

                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    Debug.Log(" do down ");
                    npc.CharaMoveByKey("D");

                }
            }
        }
    }
}
