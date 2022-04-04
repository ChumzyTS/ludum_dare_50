using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jumpStrength;
    public float maxFlySpeed;
    public float flyAcceleration;
    public float flyUsage;
    public float flyXSpeedBoost;

    public float jumpMemoryTime;
    private float jumpCurrMemory;

    public float groundMemoryTime;
    private float groundCurrMemory;

    public float jumpGravityScale;
    public float fallGravityScale;

    public float fallThreshold;

    public Collider2D groundCollider;

    private Rigidbody2D rigidBody;
    public bool jumpAvailable;
    public bool flying;

    public SpriteRenderer headRenderer;
    public SpriteRenderer armRenderer;
    public SpriteRenderer bodyRenderer;

    public Animator animator;

    private float flySpeed;    

    void Awake()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal Movement
        float xMovement = Input.GetAxisRaw("Horizontal");

        if (flying)
        {
            rigidBody.velocity = new Vector2(xMovement * speed * flyXSpeedBoost, rigidBody.velocity.y);

        }
        else
        {
            rigidBody.velocity = new Vector2(xMovement * speed, rigidBody.velocity.y);
        }

        // Dumb Visuals
        if (xMovement < 0)
        {
            animator.SetBool("Direction", false);
            animator.SetBool("Walking", true);
        }
        else if(xMovement > 0)
        {
            animator.SetBool("Direction", true);
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }


        // Jump
        bool onGround = groundCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));

        jumpCurrMemory -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumpCurrMemory = jumpMemoryTime;
        }


        if (onGround)
        {
            groundCurrMemory = groundMemoryTime;
            jumpAvailable = true;
            flying = false;
        }
        else
        {
            if (groundCurrMemory <= 0)
            {
                jumpAvailable = false;
            }
            else
            {
                groundCurrMemory -= Time.deltaTime;
            }
        }

        if (jumpAvailable)
        {
            // Jump
            if (jumpCurrMemory > 0) {
                //rigidBody.AddForce(new Vector2(0, jumpStrength * 10));
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpStrength);
                jumpAvailable = false;
            }
        }
        else
        {
            if (gameObject.GetComponent<Mana>().amount > 0)
            {
                // Start Fly
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    flySpeed = rigidBody.velocity.y;
                    flying = true;
                }

                // Continue Flying
                if (flying && (Input.GetKey(KeyCode.Space) || Input.GetAxisRaw("Vertical") > 0))
                {
                    flySpeed += flyAcceleration * Time.deltaTime;
                    if (flySpeed > maxFlySpeed)
                    {
                        flySpeed = maxFlySpeed;
                    }

                    gameObject.GetComponent<Mana>().AddMana(-flyUsage * Time.deltaTime);

                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, flySpeed);
                }
                else
                {
                    flying = false;
                }
            }
        }

        if (((Input.GetKey(KeyCode.Space) || Input.GetAxisRaw("Vertical") > 0) && rigidBody.velocity.y > fallThreshold) || flying) 
        {
            rigidBody.gravityScale = jumpGravityScale;
        }
        else
        {
            rigidBody.gravityScale = fallGravityScale;
        }
    }

    public void LockMovement()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void UnlockMovement()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
