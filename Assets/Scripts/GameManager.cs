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
        CURRENTROOM = 0;
        room[0] = Instantiate(roomPF);
        room[0].GetComponent<Room>().RandomRoom(0, 0);
        room[0].GetComponent<Room>().DrawRoom();
    }

    // *******************************************************************************MAIN UPDATE
    void Update()
    {

    }// ******************************************************************************MAIN UPDATE
}
