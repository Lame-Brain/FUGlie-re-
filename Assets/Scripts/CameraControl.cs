using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float Camera_Speed;
    public GameObject Camera_Target;
    public float zoom;
    private GameObject Test_Target;


    void Start()
    {
        Test_Target = new GameObject("Dummy");        
    }

    // Update is called once per frame
    void Update()
    {
        //reset move x and move y variables (and zoom)
        float mx = 0, my = 0;
        zoom = Mathf.Abs(Camera.main.transform.position.z - Camera_Target.transform.position.z);
        Camera_Speed = 0.025f * zoom;

        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0f) //The mousewheel has scrolled
        {
            //Mouse wheel scroll up
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f) { zoom = zoom - 0.5f; }//zoom in
            //Mouse wheel scroll down
            if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f) { zoom = zoom + 0.5f; } //zoom out
        }

        if (Input.GetMouseButton(2) || Input.GetMouseButton(1)) //Middle Mouse Button or Right Mouse Button
        {
            if (Input.GetAxis("Mouse X") < 0) mx = Camera_Speed; //mouse moves left
            if (Input.GetAxis("Mouse X") > 0) mx = -Camera_Speed; //mouse moves right
            if (Input.GetAxis("Mouse Y") < 0) my = Camera_Speed; ; //mouse moves down
            if (Input.GetAxis("Mouse Y") > 0) my = -Camera_Speed; ; //mouse moves up
        }

        //WASD or ArrowKeys
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) my = Camera_Speed;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) mx = -Camera_Speed;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) my = -Camera_Speed;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) mx = Camera_Speed;

        //camera bounding code. I place a test object... 
        Test_Target.transform.Translate(mx, my, 0, Space.Self);
        float tx = Test_Target.transform.position.x, ty = Test_Target.transform.position.y;
        float maxX = GameManager.GAME.room[GameManager.CURRENTROOM].GetComponent<Room>().xSize, maxY = GameManager.GAME.room[GameManager.CURRENTROOM].GetComponent<Room>().ySize;
        if (tx > -2 && tx < maxX + 2 && ty > -2 && ty < maxY + 2) //... and if it is in bounds....
        {
            //..then I set the position of the real camera
            Camera_Target.transform.Translate(mx, my, 0, Space.Self);
            Camera.main.transform.LookAt(Camera_Target.transform.position, transform.up);
            Camera.main.transform.localPosition = new Vector3(0, -zoom, -zoom);
        }
        //then I reset the positon of the test object
        Test_Target.transform.position = Camera_Target.transform.position;
    }
}
