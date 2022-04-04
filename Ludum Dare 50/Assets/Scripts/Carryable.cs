using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carryable : MonoBehaviour
{
    public float manaGive;

    public Vector2 pickupOffset;
    public float pickupDelay = 1;

    public Projectile projGive;

    public bool beingCarried = false;

    public Collider2D collision;

    private float currentDelay = 0;


    public bool Pickup()
    {
        bool valid = false;

        if (gameObject.GetComponent<Carryable>().enabled)
        {
            if (currentDelay <= 0)
            {
                beingCarried = true;
                collision.enabled = false;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                valid = true;
            }
        }

        return valid;
    }

    public void Drop()
    {
        currentDelay = pickupDelay;

        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        beingCarried = false;
        collision.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.GetComponent<Enemy>() == null)
        {
            Cauldron cauldScript = collision.gameObject.GetComponent<Cauldron>();
            if (cauldScript != null)
            {
                cauldScript.Brew(gameObject.GetComponent<Carryable>());
            }
        }
    }

    private void Update()
    {
        if (currentDelay > 0)
        {
            currentDelay -= Time.deltaTime;
        }
    }
}
