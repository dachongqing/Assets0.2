using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuangBoController : MonoBehaviour {

    private Queue<GuangBoAction> guangBoList = new Queue<GuangBoAction>();

    private RoundController roundController;

    private RoomContraller roomContraller;

    private DiceRollCtrl diceRoll;

    private ConfirmManageUI confirmUI;

    private CameraCtrl camCtrl;

    private Dictionary<string, GuangBoAction> eventGuangBoMap = new Dictionary<string, GuangBoAction>();

    public void insertGuangBo(GuangBoAction gba) {
        guangBoList.Enqueue(gba);
    }

    public Dictionary<string, GuangBoAction> getEventGuangBoMap() {

        return eventGuangBoMap;

    }

    public void doNow(string gbCode) {
        GuangBoAction gb = eventGuangBoMap[gbCode];
        if (gb.getGuangBoType() == GuangBoConstant.GUANGBO_TYPE_MOVE_ROOM) {
            RoomInterface targetRoom = roomContraller.findRoomByRoomType(gb.getGuangBoTargetVaule());
            //targetRoom.getXYZ();
            camCtrl.setTargetPos(targetRoom.getXYZ(), targetRoom.getXYZ()[2],true);
            foreach (string name in gb.getWhiteList())
            {
                Character npc = roundController.getCharaByName(name);
                roomContraller.findRoomByXYZ(npc.getCurrentRoom()).removeChara(npc);
                this.roomContraller.setCharaInMiniMap(npc.getCurrentRoom(), npc, false);
                npc.setCurrentRoom(targetRoom.getXYZ());
                this.roomContraller.setCharaInMiniMap(targetRoom.getXYZ(), npc, true);
                
            }
            Character chara = roundController.getPlayerChara();
            roomContraller.findRoomByXYZ(chara.getCurrentRoom()).removeChara(chara);
            this.roomContraller.setCharaInMiniMap(chara.getCurrentRoom(), this.roundController.getCurrentRoundChar(), false);
              foreach (string name in gb.getWhiteList()) {
                Character npc = roundController.getCharaByName(name);
                roomContraller.findRoomByXYZ(npc.getCurrentRoom()).removeChara(npc);
                this.roomContraller.setCharaInMiniMap(npc.getCurrentRoom(), npc, false);

                npc.setCurrentRoom(targetRoom.getXYZ());
                this.roomContraller.setCharaInMiniMap(targetRoom.getXYZ(), npc, true);
            }
            chara.setCurrentRoom(targetRoom.getXYZ());            
            this.roomContraller.setCharaInMiniMap(targetRoom.getXYZ(), chara, true);
            Debug.Log("loading。。。ready to move room");

            //
          
        }
    }

    // Use this for initialization
    void Start () {
        roundController = FindObjectOfType<RoundController>();
        roomContraller = FindObjectOfType<RoomContraller>();
        diceRoll = FindObjectOfType<DiceRollCtrl>();
        confirmUI = FindObjectOfType<ConfirmManageUI>();
        camCtrl = FindObjectOfType<CameraCtrl>();
    }
	
	// Update is called once per frame
	void Update () {
        if (guangBoList.Count > 0) {

           Character[] charas =  roundController.getAllChara();

            GuangBoAction gAction = guangBoList.Dequeue();

            if (gAction.getGuangBoType() == GuangBoConstant.GUANGBO_TYPE_MOVE_ROOM) {
                eventGuangBoMap.Add(gAction.getGuangBoCode(), gAction);
            }

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
