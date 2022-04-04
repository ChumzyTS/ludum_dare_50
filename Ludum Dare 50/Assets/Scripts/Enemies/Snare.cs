using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snare : Enemy
{
    public float snareTime;

    public bool playerSnared;
    public Vector2 playerOffset;

    public GameObject player;

    private float currentTimeSnared;

    public Animator animator;
    
    public bool canSnap;


    private Movement movement;

    private void Awake()
    {
        player = FindObjectOfType<Movement>().gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!playerSnared && canSnap)
        {
            movement = collision.GetComponent<Movement>();
            if (movement)
            {
                movement.LockMovement();
                animator.SetBool("HasPlayer", true);
                playerSnared = true;
                health = 100000;
                currentTimeSnared = 0;
                movement.gameObject.transform.position = (Vector2)transform.position + playerOffset;
            }
        }
    }

    private void Update()
    {
        if (playerSnared)
        {

            health = 100000;
            currentTimeSnared += Time.deltaTime;

            if (currentTimeSnared >= snareTime)
            {
                movement.UnlockMovement();
                playerSnared = false;
                
                GameObject newPop = Instantiate(pop);
                newPop.transform.position = transform.position;

                Destroy(gameObject);
            }
        }
    }
}
