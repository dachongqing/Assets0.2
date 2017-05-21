using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomNameUIManager : MonoBehaviour {


    private Player player;

    public Text roomName;

    private RoomContraller roomContraller;

    // Use this for initializ
    void Start () {
        player = FindObjectOfType<Player>();
        roomContraller = FindObjectOfType<RoomContraller>();
    }
	
	// Update is called once per frame
	void Update () {
        roomName.text = roomContraller.findRoomByXYZ(player.getCurrentRoom()).getRoomName();

    }
}
