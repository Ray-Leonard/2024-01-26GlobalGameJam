using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour
{
    int speed = 300;
    public Rigidbody2D rb;
    public Camera cam;
    public KeyCode mousebutton;

    public bool useController = false;

    // Update is called once per frame
    void Update()
    {
        if (useController)
        {
            if (Mathf.Abs(  Input.GetAxisRaw("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1f ){
                float rotationZ = Mathf.Atan2(Input.GetAxisRaw("Horizontal"), -Input.GetAxisRaw("Vertical")) * Mathf.Rad2Deg;
                rb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ, speed * Time.fixedDeltaTime));
            }


        }
        else
        {
            Vector3 playerpos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
            Vector3 difference = playerpos - transform.position;
            float rotationZ = Mathf.Atan2(difference.x, -difference.y) * Mathf.Rad2Deg;
            if (Input.GetKey(mousebutton))
            {
                rb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ, speed * Time.fixedDeltaTime));
            }
        }


    }
}
