using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public Transform playerPos;

    [Space]
    public float jumpForce;
    public float playerSpeed;
    private bool isOnGround;
    public bool IsOnGround { get => isOnGround; }
    public float positionRadius;
    public LayerMask ground;

    [Space]
    [SerializeField] private int playerID;

    private delegate float MoveInput();
    private MoveInput GetMoveDir;



    // Start is called before the first frame update
    void Start()
    {
        Collider2D[] colliders = transform.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colliders.Length; i++) 
        {
            for (int k = i+1; k < colliders.Length; k++)
            {
                Physics2D.IgnoreCollision(colliders[i], colliders[k]);
            }
        }

        if (playerID == 1)
        {
            GetMoveDir = GameInput.Instance.GetMoveDirPlayer1;
            GameInput.Instance.OnPlayer1Jump += HandleJump;
        }
        else if (playerID == 2)
        {
            GetMoveDir = GameInput.Instance.GetMoveDirPlayer2;
            GameInput.Instance.OnPlayer2Jump += HandleJump;

        }
    }




    // Update is called once per frame
    void Update()
    {
        if (GetMoveDir() != 0)
        {
            if (GetMoveDir() > 0)
            {
                anim.Play("Walk");

                rb.velocity = new Vector2(playerSpeed * Time.deltaTime, rb.velocity.y);
            }
            else
            {
                anim.Play("WalkBack");

                rb.velocity = new Vector2(-playerSpeed * Time.deltaTime, rb.velocity.y);
            }
        }
        else
        {
            anim.Play("Idle");
        }

        isOnGround = Physics2D.OverlapCircle(playerPos.position, positionRadius, ground);

    }


    private void HandleJump(object sender, System.EventArgs e)
    {
        if (isOnGround == true)
        {
            //rb.AddForce(Vector2.up * jumpForce);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

}
