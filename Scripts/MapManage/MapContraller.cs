using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapContraller  {

    public List<List<int>> tmp = new List<List<int>>();

    public List<int[]> rooms = new List<int[]>();

    public Dictionary<int[], int[]> hashMap = new Dictionary<int[], int[]>();

    public System.Random random = new System.Random();

    public int[] checkRoom(Dictionary<int[], int[]> rooms, int[] gRoom)
    {


        foreach (int[] key in rooms.Keys)
        {
            if (key[0] == gRoom[0] && key[1] == gRoom[1])
            {
                return key;
            }
        }
        return null;
    }

    public int[] findDoor(int[] gRoom)
    {
        int[] door = new int[] { 0, 0, 0, 0 };
        int[] upR = new int[] { gRoom[0], gRoom[1] };
        upR[1] = upR[1] + 1;
        int[] key = checkRoom(hashMap, upR);
        if (key != null)
        {

            int i = random.Next(0, 2);
            if (i == 1)
            {
                hashMap[key][1] = 1;
                door[0] = 1;
            }
        }
        else
        {

        }

        int[] dwR = new int[] { gRoom[0], gRoom[1] };
        dwR[1] = dwR[1] - 1;

        key = checkRoom(hashMap, dwR);
        if (key != null)
        {


            int i = random.Next(0, 2);
            if (i == 1)
            {
                hashMap[key][0] = 1;
                door[1] = 1;
            }
        }
        else
        {

        }

        int[] leR = new int[] { gRoom[0], gRoom[1] };
        leR[0] = leR[0] + 1;

        key = checkRoom(hashMap, leR);
        if (key != null)
        {

            int i = random.Next(0, 2);
            if (i == 1)
            {
                hashMap[key][2] = 1;
                door[3] = 1;
            }
        }
        else
        {

        }

        int[] riR = new int[] { gRoom[0], gRoom[1] };
        riR[0] = riR[0] - 1;

        key = checkRoom(hashMap, riR);
        if (key != null)
        {
         //   Debug.Log(" i can find the room!!!!!!!!!1");

            int i = random.Next(0, 2);
            if (i == 1)
            {
                hashMap[key][3] = 1;
                door[2] = 1;
            }
        }
        else
        {

        }

        return door;
    }

    public Dictionary<int[], int[]> genMap(int z,int roomNumb)
    {

        List<int[]> xy = new List<int[]>();
        int[] initxy = new int[] { 0, 0, z };
        xy.Add(initxy);

        int initR = 0;

        while (initR < roomNumb)
        {
            //xy = orderList(xy);
            xy =  FunctionUnity<int[]>.orderList(xy);
            int[] gRoom = xy[0];

            if (checkRoom(hashMap, gRoom) != null)
            {
                continue;
            }
            else
            {


                if (gRoom[0] == 0 && gRoom[1] == 0)
                {
                    hashMap.Add(gRoom, new int[] { 0, 0, 0, 0 });
                }
                else
                {
                    int[] door = findDoor(gRoom);

                    if (door[0] != 0 || door[1] != 0 || door[2] != 0 || door[3] != 0)
                    {

                        hashMap.Add(gRoom, door);
                        initR++;
                    }
                    else
                    {

                    }
                }

            }
            int[] upR = new int[] { gRoom[0], gRoom[1],z };
            upR[1] = upR[1] + 1;

            int[] dwR = new int[] { gRoom[0], gRoom[1],z };
            dwR[1] = dwR[1] - 1;

            int[] leR = new int[] { gRoom[0], gRoom[1],z };
            leR[0] = leR[0] + 1;

            int[] riR = new int[] { gRoom[0], gRoom[1],z };
            riR[0] = riR[0] - 1;

            xy.Remove(gRoom);
            xy.Add(upR);
            xy.Add(dwR);
            xy.Add(leR);
            xy.Add(riR);

        }

        return hashMap;
    }

    public List<int[]> orderList(List<int[]> ContentList)
    {

        List<int[]> newList = new List<int[]>();
        foreach (int[] item in ContentList)
        {
            newList.Insert(random.Next(newList.Count), item);
        }
        return newList;
    }
}
