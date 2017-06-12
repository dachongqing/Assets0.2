using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SystemUtil  {


    public static bool IsTouchedUI()
    {
        bool touchedUI = false;
        if (Application.isMobilePlatform)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                touchedUI = true;
            }
        }
        else if (EventSystem.current.IsPointerOverGameObject())
        {
            touchedUI = true;
        }
        return touchedUI;
    }
}
