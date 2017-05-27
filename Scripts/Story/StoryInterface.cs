using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface StoryInterface  {

    StoryScript getGoodManScript();

    StoryScript getBadManScript();
    
    bool checkStoryStart(Character chara, RoomInterface room);

    bool checkStoryEnd(Character chara, RoomInterface room);
    
    string getStoryInfo();

 




}
