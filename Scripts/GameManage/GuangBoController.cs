using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuangBoController : MonoBehaviour {

    private Queue<GuangBoAction> guangBoList = new Queue<GuangBoAction>();

    private RoundController roundController;

    private RoomContraller roomContraller;

    private DiceRollCtrl diceRoll;

    public void insertGuangBo(GuangBoAction gba) {
        guangBoList.Enqueue(gba);
    }

    // Use this for initialization
    void Start () {
        roundController = FindObjectOfType<RoundController>();
        roomContraller = FindObjectOfType<RoomContraller>();
        diceRoll = FindObjectOfType<DiceRollCtrl>();
    }
	
	// Update is called once per frame
	void Update () {
        if (guangBoList.Count > 0) {

           Character[] charas =  roundController.getAllChara();

            GuangBoAction gAction = guangBoList.Dequeue();

            foreach (Character chara in charas ){
                if (typeof(NPC).IsAssignableFrom(chara.GetType()))
                {
                   


                    NPC npc = (NPC)chara;
                    if (gAction.hasVictim() && gAction.getVictim().getName() == npc.getName()) {
                        npc.setFollowGuangBoAction(true);
                        npc.setGuangBoAction(gAction);
                        gAction.addWhiteList(npc.getName());
                        gAction.sendGuangBoToOwner(npc, roomContraller, roundController);
                    }

                    if (!npc.isFollowGuangBoAction() && !gAction.checkOwner(npc.getName()) && !npc.isPlayer()) {
                        int san = npc.getAbilityInfo()[3];
                        int res = diceRoll.calculateDice(san);
                        if (res < gAction.getSanLimit())
                        {
                            npc.setFollowGuangBoAction(true);
                            npc.setGuangBoAction(gAction);
                            gAction.addWhiteList(npc.getName());
                            gAction.sendGuangBoToOwner(npc, roomContraller, roundController);
                        }
                        else {
                            gAction.addBlackList(npc.getName());
                        }

                    }
                  
                }
            }

        }
	}
}
