﻿using System.Collections;
using System.Collections.Generic;


public interface Thing : BaseGameOject {

     void doClick();

    string getThingCode();

    void setClickMessage(string[] msg);

    string[] getClickMessage();
}
