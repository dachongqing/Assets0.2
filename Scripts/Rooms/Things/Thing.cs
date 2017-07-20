using System.Collections;
using System.Collections.Generic;


public interface Thing  {

     void doClick();

    string getThingCode();

    void setClickMessage(string[] msg);

    string[] getClickMessage();
}
