using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TaskAwardInterface  {

    int[] getTaskAwardAttrInfo();

    List<Item> getTaskAwardItemInfo();


    void setItemAwards(List<Item> items);

    void setAttriAwards(int str,int spe,int san,int inte);

    void executeAwards();

}
