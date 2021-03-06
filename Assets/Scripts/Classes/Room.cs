﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public string displayName; //the name displayed by the room, can be edited by player
    public bool explored; //whether or not the room has been explored. Unexplored rooms cannot be viewed by the player
    public int xSize, ySize, rSize; //length, width, and overall size of the room.
    public int doorNorth, doorEast, doorSouth, doorWest; //The x value of the north door, the y value of the east door, etc... if Value is 0, there is no door leading that direction.
    public int[,] floorTile, floorDeco; //tracks the color variation and flourishes on floor tiles so that they remain the same when loading.
    public bool[,] reverseTile; //Some decorations are flipped on the X-Axis. This tracks that.
    public Color roomColor; //Every room has a random color-hue.

    //Other
    public int stars, circles, triangles; //Room's resource inventory

    //PREFABS
    public GameObject[] tilePF, decoPF; //These are the prefabs used to instantiate floorTile and floorDeco
    public GameObject doorPF, wallPF, gardenPF, healHutPF, hutPF, signPF, totemPF, workshopPF, cavePF; //These are the prefabs for everything else

    //Queues and Lists
    [HideInInspector] public Queue orderList;
    [HideInInspector] public List<GameObject> buildings;

    public void RandomRoom(int roomX, int roomY)
    {
        float r = Random.Range(0, 130); float g = Random.Range(0, 130); float b = Random.Range(0, 130);
        if ((r + g + b) < 60) { r = 50f; g = 50; ; b = 50; }
        roomColor = new Color(r / 255, g / 255, b / 255);

        xSize = Random.Range(15, 30); ySize = Random.Range(15, 30); rSize = xSize * ySize; //Randomize length and width of room. 
        floorTile = new int[xSize + 2, ySize + 2]; //initialize tile array
        floorDeco = new int[xSize + 1, ySize + 1]; //initialize floor decoration array        
        reverseTile = new bool[xSize + 1, ySize + 1];
        buildings = new List<GameObject>();
        //Define each index in the above two arrays
        for (int y = 1; y <= ySize; y++)
        {
            for(int x = 1; x <= xSize; x++)
            {
                int floorDecoRand = Random.Range(0, 75); if (floorDecoRand > 11) floorDecoRand = 0; //Floor deco can be 0 to 7, but heavily favors 0
                floorTile[x, y] = Random.Range(0, 7);
                floorDeco[x, y] = floorDecoRand;
                reverseTile[x, y] = (Random.Range(0, 100) > 50);
            }
        }
        //North Door
        doorNorth = Random.Range(1, xSize);
        if (roomY == GameManager.GAME.mazeYsize) doorNorth = 0;

        //East Door
        doorEast = Random.Range(1, ySize);
        if (roomX == GameManager.GAME.mazeXsize) doorEast = 0;

        //South Door
        doorSouth = Random.Range(1, xSize);
        if (roomY == 0) doorSouth = 0;

        //West Door
        doorWest = Random.Range(1, ySize);
        if (roomX == 0) doorWest = 0;

        //Add Hut, FOR TESTING, REMOVE LATER
        //buildings.Add(new building(hutPF, Random.Range(1, xSize), Random.Range(1, ySize)));
    }

    public void DrawRoom()
    {
        explored = true;
        //draw walls
        for (int x = 0; x <= xSize + 1; x++)
        {
            GameObject go1, go2;
            if(x == 0 || x != doorSouth)
            {
                go1 = Instantiate(wallPF, new Vector3(x, 0, -.5f), Quaternion.identity);
                go1.transform.parent = gameObject.transform;
                //go1.GetComponent<SpriteRenderer>().color = roomColor;
                go1.GetComponent<MeshRenderer>().material.color = roomColor;
            }
            if (x == 0 || x != doorNorth)
            {
                go2 = Instantiate(wallPF, new Vector3(x, ySize + 1, -.5f), Quaternion.identity);
                //go2.GetComponent<SpriteRenderer>().color = roomColor;
                go2.GetComponent<MeshRenderer>().material.color = roomColor;
                go2.transform.parent = gameObject.transform;
            }
        }
        for (int y = 1; y <= ySize; y++)
        {
            GameObject go1, go2;
            if (y != doorWest)
            {
                go1 = Instantiate(wallPF, new Vector3(0, y, -.5f), Quaternion.identity);
                //go1.GetComponent<SpriteRenderer>().color = roomColor;
                go1.GetComponent<MeshRenderer>().material.color = roomColor;
                go1.transform.parent = gameObject.transform;
            }

            if (y != doorEast)
            { 
                go2 = Instantiate(wallPF, new Vector3(xSize + 1, y, -.5f), Quaternion.identity);
                //go2.GetComponent<SpriteRenderer>().color = roomColor;
                go2.GetComponent<MeshRenderer>().material.color = roomColor;
                go2.transform.parent = gameObject.transform;
            }
        }
        //Draw Floors
        for (int y = 1; y <= ySize; y++)
        {
            for(int x = 1; x <= xSize; x++)
            {
                GameObject go1 = Instantiate(tilePF[floorTile[x, y]], new Vector3(x, y, 0), Quaternion.identity);
                go1.GetComponent<SpriteRenderer>().color = roomColor;
                go1.transform.parent = gameObject.transform;
                GameObject go2 = Instantiate(decoPF[floorDeco[x, y]], new Vector3(x, y, 0), Quaternion.identity);
                if(floorDeco[x,y] < 5 || floorDeco[x,y] > 8) go2.GetComponent<SpriteRenderer>().color = roomColor;
                go2.GetComponent<SpriteRenderer>().flipX = reverseTile[x,y];
                go2.transform.parent = gameObject.transform;
            }
        }
        //Draw doors        
        if (doorNorth > 0)  { GameObject go = Instantiate(cavePF, new Vector3(doorNorth, ySize + 1, -0.5f), Quaternion.identity); go.GetComponent<MeshRenderer>().material.color = roomColor; go.transform.parent = gameObject.transform; go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = roomColor; }
        if (doorEast > 0)   { GameObject go = Instantiate(cavePF, new Vector3(xSize + 1, doorEast, -0.5f), Quaternion.identity); go.GetComponent<MeshRenderer>().material.color = roomColor; go.transform.parent = gameObject.transform; go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = roomColor; }
        if (doorSouth > 0)  { GameObject go = Instantiate(cavePF, new Vector3(doorSouth, 0, -0.5f), Quaternion.identity); go.GetComponent<MeshRenderer>().material.color = roomColor; go.transform.parent = gameObject.transform; go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = roomColor; }
        if (doorWest > 0)   { GameObject go = Instantiate(cavePF, new Vector3(0, doorWest, -0.5f), Quaternion.identity); go.GetComponent<MeshRenderer>().material.color = roomColor; go.transform.parent = gameObject.transform; go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = roomColor; }
    }    

    public void AddBuilding(string b, float x, float y)
    {
        GameObject goPF = null;
        if (b == "Hut") goPF = hutPF;
        if (b == "Healer") goPF = healHutPF;
        if (b == "Garden") goPF = gardenPF;
        if (b == "Totem") goPF = totemPF;
        if (b == "Sign") goPF = signPF;
        if (b == "Workshop") goPF = workshopPF;
        if (goPF != null)
        {
            GameObject go = Instantiate(goPF, new Vector3(x, y, 0), goPF.transform.rotation);
            go.transform.parent = gameObject.transform;
            buildings.Add(go);
        }
    }
}
