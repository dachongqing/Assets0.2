using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionUnity<T>  {

    public static  System.Random random = new System.Random();

    public static  List<T> orderList(List<T> ContentList)
    {

        List<T> newList = new List<T>();
        foreach (T item in ContentList)
        {
            newList.Insert(random.Next(newList.Count), item);
        }
        return newList;
    }

}
