using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    public float lifeSpan;

    public float spawnOffset;
    public int damage;
    public bool pierce;
    public bool destroyEnemy;

    private bool alive;
    private float aliveTime;

    private Rigidbody2D rigidBody;

    public void Spawn(Vector2 _position, Vector2 _direction)
    {
        float radians = Mathf.Atan2(_direction.y, _direction.x);
        float angle = radians * Mathf.Rad2Deg;
        
        transform.position = _position + new Vector2(spawnOffset * Mathf.Cos(radians), spawnOffset * Mathf.Sin(radians));

        rigidBody = gameObject.AddComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
        rigidBody.velocity = _direction * speed;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        alive = true;
    }

    private void Update()
    {
        if (alive)
        {
            aliveTime += Time.deltaTime;

            if (aliveTime > lifeSpan)
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy != null )
        {
            enemy.Hurt(damage, destroyEnemy);
            if (!pierce)
            {
                Destroy(gameObject);
            }
        }
    }
}
