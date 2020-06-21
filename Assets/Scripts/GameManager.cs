using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GAME;
    public static int CURRENTROOM;

    public int mazeXsize, mazeYsize;

    public GameObject roomPF;

    [HideInInspector]
    public GameObject[] room;

    private void Awake()
    {
        //Singleton
        if (GAME != null && GAME != this)
        {
            Destroy(this.gameObject);
            return;
        }
        GAME = this;
        DontDestroyOnLoad(GAME);
    }

    void Start()
    {
        mazeXsize = 3; mazeYsize = 3;
        room = new GameObject[10];
        CURRENTROOM = 0;
        room[0] = Instantiate(roomPF);
        room[0].GetComponent<Room>().RandomRoom(0, 0);
        room[0].GetComponent<Room>().DrawRoom();
        room[0].GetComponent<Room>().AddBuilding("Hut", Random.Range(2f, room[0].GetComponent<Room>().xSize - 1), Random.Range(2f, room[0].GetComponent<Room>().ySize - 1));
        room[0].GetComponent<Room>().AddBuilding("Healer", Random.Range(2f, room[0].GetComponent<Room>().xSize - 1), Random.Range(2f, room[0].GetComponent<Room>().ySize - 1));
        room[0].GetComponent<Room>().AddBuilding("Sign", Random.Range(2f, room[0].GetComponent<Room>().xSize - 1), Random.Range(2f, room[0].GetComponent<Room>().ySize - 1));
        room[0].GetComponent<Room>().AddBuilding("Totem", Random.Range(2f, room[0].GetComponent<Room>().xSize - 1), Random.Range(2f, room[0].GetComponent<Room>().ySize - 1));
        room[0].GetComponent<Room>().AddBuilding("Garden", Random.Range(2f, room[0].GetComponent<Room>().xSize - 1), Random.Range(2f, room[0].GetComponent<Room>().ySize - 1));
        room[0].GetComponent<Room>().AddBuilding("Workshop", Random.Range(2f, room[0].GetComponent<Room>().xSize - 1), Random.Range(2f, room[0].GetComponent<Room>().ySize - 1));

        room[1] = Instantiate(roomPF);
        room[1].GetComponent<Room>().RandomRoom(1, 0);
        room[1].GetComponent<Room>().DrawRoom();
        room[1].transform.position = new Vector3(room[1].transform.position.x + 75, room[1].transform.position.y, 0);


        room[2] = Instantiate(roomPF);
        room[2].GetComponent<Room>().RandomRoom(0, 1);
        room[2].GetComponent<Room>().DrawRoom();
        room[2].transform.position = new Vector3(room[2].transform.position.x, room[1].transform.position.y + 75, 0);

    }

    // *******************************************************************************MAIN UPDATE
    void Update()
    {

    }// ******************************************************************************MAIN UPDATE
}
