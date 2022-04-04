using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Carryable carryable;

    public int health = 1;

    public float sapAmount;

    public GameObject mask;

    public int[] spawnWeights;
    public GameObject pop;

    public bool alwaysDrop;

    [HideInInspector]
    public bool spawned;
    [HideInInspector]
    public Game gameManager;

    public virtual void Spawn(Vector2 position, Game _gameManager)
    {
        gameObject.transform.position = position;
        if (carryable != null)
        {
            carryable.enabled = false;
        }
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        rigidBody.gravityScale = 0;
        spawned = true;
        gameManager = _gameManager;

        gameManager.IncreaseSap(sapAmount);
    }

    public void Hurt(int damage, bool destoryEnemy)
    {
        health -= damage;
        if (health <= 0)
        {
            Die(destoryEnemy);
        }
    }

    public virtual void Die(bool destoryEnemy)
    {
        GameObject newPop = Instantiate(pop);
        newPop.transform.position = transform.position;

        if (!alwaysDrop)
        {
            gameManager.CountDeath();
        }

        if ((!destoryEnemy && carryable != null) || alwaysDrop)
        {
            carryable.enabled = true;
            rigidBody.gravityScale = 1;
            rigidBody.constraints = RigidbodyConstraints2D.None;
            if (sapAmount > 0)
            {
                gameManager.IncreaseSap(-sapAmount);
            }

            if (mask)
            {
                Destroy(mask);
            }

            Destroy(gameObject.GetComponent<Enemy>());
        }
        else
        {
            gameManager.IncreaseSap(-sapAmount);
            Destroy(gameObject);
        }
    }
}
