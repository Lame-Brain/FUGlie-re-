using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float Camera_Speed;

    // Update is called once per frame
    void Update()
    {
        float x = Camera.main.transform.position.x; float y = Camera.main.transform.position.y; float z = Camera.main.transform.position.z;
        float CS = -1 * (z + 1); Camera_Speed = CS * 0.01f;

        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0f)
        {
            //Mouse wheel scroll up
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f) z = z + .5f;
            //Mouse wheel scroll down
            if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f) z = z - .5f;
        }

        if (Input.GetMouseButton(2) || Input.GetMouseButton(1)) //Middle Mouse Button or Right Mouse Button
        {
            if (Input.GetAxis("Mouse X") < 0) x = x + Camera_Speed * 2f; //mouse moves left
            if (Input.GetAxis("Mouse X") > 0) x = x - Camera_Speed * 2f; //mouse moves right
            if (Input.GetAxis("Mouse Y") < 0) y = y + Camera_Speed * 2f; //mouse moves down
            if (Input.GetAxis("Mouse Y") > 0) y = y - Camera_Speed * 2f; //mouse moves up
        }

        //WASD or ArrowKeys
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) y = y + Camera_Speed;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) x = x - Camera_Speed;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) y = y - Camera_Speed;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) x = x + Camera_Speed;

        //bound camera movement
        int mx = GameManager.GAME.room[GameManager.CURRENTROOM].GetComponent<Room>().xSize + 2;
        int my = GameManager.GAME.room[GameManager.CURRENTROOM].GetComponent<Room>().ySize + 2;
        if (x > mx) x = mx; if (x < -2) x = -2;
        if (y > my) y = my; if (y < -2) y = -2;
        if (z > -2) z = -2; if (z < -15) z = -15;

        Camera.main.transform.position = new Vector3(x, y, z);
    }
}
