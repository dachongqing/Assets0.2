using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TaskItemInterface
{
    string getItemDesc();

    bool isCompleted();

    void checkItemComplelted();

    string getItemCode();


}
