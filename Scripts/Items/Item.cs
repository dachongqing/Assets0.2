using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Item  {

   
    int getDurability();

	void used();

    string getType();

    string getCode();

    string getName();

    string getDesc();
}
