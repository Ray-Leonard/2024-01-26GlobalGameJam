using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LeftRight { Left, Right };

public class Arms : MonoBehaviour
{
    private PlayerController player;
    private Rigidbody2D rb;

    [SerializeField] private LeftRight armDir;

    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    int speed = 300;

    // Update is called once per frame
    void Update()
    {
        Vector3 cursorPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

        Vector3 difference = cursorPos - transform.position;
        float rotationZ = Mathf.Atan2(difference.x, -difference.y) * Mathf.Rad2Deg;




        if(GameInput.Instance.GetHandPressed(armDir, player.PlayerID))
        {
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ, speed * Time.fixedDeltaTime));
        }
    }
}
