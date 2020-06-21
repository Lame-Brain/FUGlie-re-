using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float Camera_Speed;
    public float roomX, roomY, roomZ;

    // Update is called once per frame
    void Update()
    {
        //Get the x,y,z location of camera
        float x = Camera.main.transform.position.x; float y = Camera.main.transform.position.y; float z = Camera.main.transform.position.z;
        //get the z location of the target, then set camera speed relative to how near the camera is to the target object. Closer is slower, Further is faster.
        float zed = GameManager.GAME.room[GameManager.CURRENTROOM].transform.position.z; float CS = ((zed-z) + 1); Camera_Speed = CS * 0.01f; 

        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0f) //The mousewheel has scrolled
        {
            //Mouse wheel scroll up
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f) z = z + .5f; //zoom in
            //Mouse wheel scroll down
            if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f) z = z - .5f; //zoom out
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

        //bound camera movement, relative to target object
        float minX = GameManager.GAME.room[GameManager.CURRENTROOM].transform.position.x;
        float minY = GameManager.GAME.room[GameManager.CURRENTROOM].transform.position.y;        
        float maxX = minX + GameManager.GAME.room[GameManager.CURRENTROOM].GetComponent<Room>().xSize;
        float maxY = minY + GameManager.GAME.room[GameManager.CURRENTROOM].GetComponent<Room>().ySize;        
        if (x > maxX + 2) x = maxX + 2; if (x < minX - 2) x = minX - 2;
        if (y > maxY + 2) y = maxY + 2; if (y < minY - 2) y = minY - 2;
        if (z > zed - 2) z = zed - 2; if (z < zed - 15) z = zed - 15;

        roomX = minX; roomY = minY; roomZ = zed;

        Camera.main.transform.position = new Vector3(x, y, z);
    }
}
