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

        if (Input.GetMouseButton(1)) //Right Mouse Button Moves camera
        {
            if (Input.GetAxis("Mouse X") < 0) mx = Camera_Speed; //mouse moves left
            if (Input.GetAxis("Mouse X") > 0) mx = -Camera_Speed; //mouse moves right
            if (Input.GetAxis("Mouse Y") < 0) my = Camera_Speed; ; //mouse moves down
            if (Input.GetAxis("Mouse Y") > 0) my = -Camera_Speed; ; //mouse moves up
        }

        if(Input.GetMouseButton(2)) //Middle mouse button rotates camera;
        {
            if (Input.GetAxis("Mouse X") < 0) Test_Target.transform.Rotate(Vector3.forward * 90 * Time.deltaTime); //rotate left
            if (Input.GetAxis("Mouse X") > 0) Test_Target.transform.Rotate(-Vector3.forward * 90 * Time.deltaTime); //rotate right
            if (Input.GetAxis("Mouse Y") < 0) zoom = zoom + 0.5f; //Zoom out
            if (Input.GetAxis("Mouse Y") > 0) zoom = zoom - 0.5f; //zoom in
    }

        //WASD or ArrowKeys
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) my = Camera_Speed;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) mx = -Camera_Speed;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) my = -Camera_Speed;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) mx = Camera_Speed;

        //Q & E or Delete & Insert keys to rotate camera
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Delete)) Test_Target.transform.Rotate(Vector3.forward * 90 * Time.deltaTime); //rotate left
        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Insert)) Test_Target.transform.Rotate(-Vector3.forward * 90 * Time.deltaTime); //rotate right

        //camera bounding code. I place a test object... 
        Test_Target.transform.Translate(mx, my, 0, Space.Self);
        float tx = Test_Target.transform.position.x, ty = Test_Target.transform.position.y;
        float maxX = GameManager.GAME.room[GameManager.CURRENTROOM].GetComponent<Room>().xSize - 1, maxY = GameManager.GAME.room[GameManager.CURRENTROOM].GetComponent<Room>().ySize - 1;
        if (tx < 1) tx = 1;
        if (tx > maxX) tx = maxX;
        if (ty < 1) ty = 1;
        if (ty > maxY) ty = maxY;
        if (zoom < 2) zoom = 2;
        if (zoom > 15) zoom = 15;
        Test_Target.transform.position = new Vector3(tx, ty, Test_Target.transform.position.z);

        //..then I set the position of the real camera                
        Camera_Target.transform.position = Test_Target.transform.position; Camera_Target.transform.rotation = Test_Target.transform.rotation;
        Camera.main.transform.LookAt(Camera_Target.transform.position, transform.up);
        Camera.main.transform.localPosition = new Vector3(0, -zoom, -zoom);        
    }
}
