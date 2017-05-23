using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class APathManager 
{


   

    private Node node;
    private Node currentNode;



    public Stack<Node> findPath(RoomInterface intiRoom, RoomInterface targetNode, RoomContraller roundController) {
         List<Node> openList = new List<Node>();

         List<Node> closeList = new List<Node>();
        Debug.Log(" intiRoom Room is " + intiRoom.getXYZ()[0] + "," + intiRoom.getXYZ()[1]);
        Debug.Log(" targetNode Room is " + targetNode.getXYZ()[0] + "," + targetNode.getXYZ()[1]);
        bool finded = false;
        Node initNode = new Node(intiRoom.getXYZ()[0], intiRoom.getXYZ()[1], intiRoom.getXYZ()[2]);
        initNode.G = 10 ;
        initNode.H = (System.Math.Abs(initNode.xy[0] - targetNode.getXYZ()[0]) + System.Math.Abs(initNode.xy[1] - targetNode.getXYZ()[1])) * 10;
        initNode.F = initNode.G + initNode.H;
         openList.Add(initNode);
     
        while (!finded) {
                //计算起始位置周围的F值
                currentNode = getLessestNode(openList);
            Debug.Log(" currentNode Room is " + currentNode.xy[0] + "," + currentNode.xy[1]);
            closeList.Add(currentNode);
            if (currentNode.xy[0] == targetNode.getXYZ()[0] && currentNode.xy[1] == targetNode.getXYZ()[1])
            {
                Debug.Log("I got the targetRoom!!!!");
                finded = true;
                break;

            }
         
            RoomInterface currentRoom = roundController.findRoomByXYZ(currentNode.xy);
            Debug.Log(" currentRoom Room is " + currentRoom.getXYZ()[0] + "," + currentRoom.getXYZ()[1]);
            if (currentRoom.getNorthDoor().GetComponent<DoorInterface>().getShowFlag()) {
                node = new Node(currentRoom.getXYZ()[0], currentRoom.getXYZ()[1] + 1, currentRoom.getXYZ()[2]);
                if (!this.containClose(node, closeList)) {
                    node.G = 10 + currentNode.G;
                    node.H = (System.Math.Abs(node.xy[0] - targetNode.getXYZ()[0]) + System.Math.Abs(node.xy[1] - targetNode.getXYZ()[1])) * 10;
                    node.F = node.G + node.H;
                    node.parent = currentNode;
                    openList.Add(node);
                }
            }
            if (currentRoom.getSouthDoor().GetComponent<DoorInterface>().getShowFlag())
            {
                node = new Node(currentRoom.getXYZ()[0], currentRoom.getXYZ()[1] - 1, currentRoom.getXYZ()[2]);
                if (!this.containClose(node, closeList))
                {
                    node.G = 10 + currentNode.G;
                    node.H = (System.Math.Abs(node.xy[0] - targetNode.getXYZ()[0]) + System.Math.Abs(node.xy[1] - targetNode.getXYZ()[1])) * 10;
                    node.F = node.G + node.H;
                    node.parent = currentNode;
                    openList.Add(node);
                }
            }
            if (currentRoom.getEastDoor().GetComponent<DoorInterface>().getShowFlag())
            {
                node = new Node(currentRoom.getXYZ()[0] + 1, currentRoom.getXYZ()[1], currentRoom.getXYZ()[2]);
                if (!this.containClose(node, closeList))
                {
                    node.G = 10 + currentNode.G;
                    node.H = (System.Math.Abs(node.xy[0] - targetNode.getXYZ()[0]) + System.Math.Abs(node.xy[1] - targetNode.getXYZ()[1])) * 10;
                node.F = node.G + node.H;
                node.parent = currentNode;
                openList.Add(node);

                }
            }
            if (currentRoom.getWestDoor().GetComponent<DoorInterface>().getShowFlag())
            {

                node = new Node(currentRoom.getXYZ()[0] - 1, currentRoom.getXYZ()[1], currentRoom.getXYZ()[2]);
                if (!this.containClose(node, closeList))
                {
                    node.G = 10 + currentNode.G;
                    node.H = (System.Math.Abs(node.xy[0] - targetNode.getXYZ()[0]) + System.Math.Abs(node.xy[1] - targetNode.getXYZ()[1])) * 10;
                node.F = node.G + node.H;
                node.parent = currentNode;
                openList.Add(node);
                }
            }
            Debug.Log("openList.Count  " + openList.Count);
        }


        Stack<Node> paths = genPath(initNode, currentNode);

        return paths;

    }
   
    private Stack<Node> genPath(Node rootNode, Node tarNode)
    {
        Stack<Node> list = new Stack<Node>();


        while (!(tarNode.xy[0] == rootNode.xy[0] && tarNode.xy[1] == rootNode.xy[1]))
        {

            list.Push(tarNode);
            tarNode = tarNode.parent;
        }
           
        return list;
    }

    private Node getLessestNode(List<Node> openList) {
        Node lessestNode = null;
        if (openList.Count > 0)
        {
            openList.Sort();
            lessestNode = openList[0];
            openList.Remove(lessestNode);
            Debug.Log("getLessestNode  " + lessestNode);
            return lessestNode;
        }
        else {
            Debug.Log(" cant getLessestNode" + lessestNode);
            return lessestNode;
        }
    }

    private bool containClose (Node node, List<Node> closeList) {
        for(int i=0;i<closeList.Count;i++)  
        {  
            if(closeList[i].xy[0] == node.xy[0] && closeList[i].xy[1] == node.xy[1])  
            {  
                return true;  
            }  
        }  
        return false;  
     }
}
