using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTriggerConditionItem : Condition {

	// Use this for initialization
	public string getConditionInfo()
	{
		return "NPC拿到带血的黑书";
	}

	public bool getConditionStatus(Character chara, RoomInterface room, RoundController roundController)
	{
		NPC npc = (NPC)chara;
		List<Item> its = npc.getBag ().getAllItems ();

		for(int i =0; i<its.Count; i++) {
			if(its[i].getCode() == ItemConstant.ITEM_CODE_SPEC_00001) {
				Debug.Log("检查剧情开启：找到任务道具"  + its[i].getName());
				return true;
				
			}

		}

	
		return false;

	}

	public string getConditionType()
	{
		return StoryConstan.CONDITION_TYPE_TRIGGER;
	}
}
