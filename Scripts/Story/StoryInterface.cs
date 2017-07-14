using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface StoryInterface  {

    StoryScript getGoodManScript();

    StoryScript getBadManScript();
    
    bool checkStoryStart(Character chara, RoomInterface room, RoundController roundController);

    bool checkStoryEnd(Character chara, RoomInterface room, RoundController roundController);
    
    string getStoryInfo();

    void initStroy(Character chara, RoundController roundController);

    string getStoryCode();

 




}
