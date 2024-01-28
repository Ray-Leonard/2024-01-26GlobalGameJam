using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public Transform playerPos;
    public AudioClip audioClipWalk;
    public AudioClip audioClipJump;
    public AudioSource audioSource;

    [Space]
    private float jumpForce;
    private float playerSpeed;
    public float jumpForceShort;
    public float playerSpeedShort;
    public float jumpForceLong;
    public float playerSpeedLong;
    private bool isOnGround;
    public bool IsOnGround { get => isOnGround; }
    public float positionRadius;
    public LayerMask ground;

    [Space]
    [SerializeField] private int playerID;
    public int PlayerID { get => playerID; set => playerID = value; }


    // Start is called before the first frame update
    void Start()
    {
        jumpForce = jumpForceShort;
        playerSpeed = playerSpeedShort;
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
            GameInput.Instance.OnPlayer1Jump += HandleJump;
        }
        else if (playerID == 2)
        {
            GameInput.Instance.OnPlayer2Jump += HandleJump;

        }
    }

    private void OnDestroy()
    {
        if (playerID == 1)
        {
            GameInput.Instance.OnPlayer1Jump -= HandleJump;
        }
        else if (playerID == 2)
        {
            GameInput.Instance.OnPlayer2Jump -= HandleJump;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (GameInput.Instance.GetMoveDir(playerID) != 0)
        {
            if (GameInput.Instance.GetMoveDir(playerID) > 0)
            {
                anim.Play("Walk");
                rb.AddForce(Vector2.right * playerSpeed * Time.deltaTime);
                rb.velocity = new Vector2(playerSpeed * Time.deltaTime, rb.velocity.y);
            }
            else
            {
                anim.Play("WalkBack");

                //rb.AddForce(Vector2.left * playerSpeed * Time.deltaTime);
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
            rb.AddForce(Vector2.up * jumpForce);
            //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void ChangeMovementSpeed(bool isLongLegs){
        if(isLongLegs){
            jumpForce = jumpForceLong;
            playerSpeed = playerSpeedLong;
        }
        else{
            jumpForce = jumpForceShort;
            playerSpeed = playerSpeedShort;
        }
    }

}
