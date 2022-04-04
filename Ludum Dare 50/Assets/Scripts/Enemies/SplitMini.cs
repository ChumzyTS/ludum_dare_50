using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitMini : Enemy
{
    public float minLaunchSpeed;
    public float maxLaunchSpeed;

    public float minPlantTime;
    public float maxPlantTime;

    public float growTime;

    private float currentPlantTime;
    private float currentGrowTime;

    public override void Spawn(Vector2 position, Game _gameManager)
    {
        gameManager = _gameManager;

        // Regular Stuff
        gameObject.transform.position = position;
        if (carryable != null)
        {
            carryable.enabled = false;
        }

        // Launch
        float launchAngle = Random.Range(0, 360);
        float launchSpeed = Random.Range(minLaunchSpeed, maxLaunchSpeed);

        float radians = launchAngle * Mathf.Deg2Rad;

        Vector2 vectorLaunch = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * launchSpeed;
        rigidBody.velocity = vectorLaunch;

        currentPlantTime = Random.Range(minPlantTime, maxPlantTime);
    }

    private void Grow()
    {
        gameManager.SpawnRandomAtPoint(transform.position);
        Destroy(gameObject);
    }

    private void Plant()
    {
        rigidBody.gravityScale = 0;
        rigidBody.velocity = Vector2.zero;
        spawned = true;
    }

    private void Update()
    {
        if (!spawned)
        {
            if (currentPlantTime > 0)
            {
                currentPlantTime -= Time.deltaTime;
            }
            else
            {
                Plant();
            }
        }
        else
        {
            if (currentGrowTime > growTime)
            {
                Grow();
            }
            currentGrowTime += Time.deltaTime;
        }
    }
}
