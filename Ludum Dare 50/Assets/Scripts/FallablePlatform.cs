using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallablePlatform : MonoBehaviour
{
    public Collider2D dropCollider;
    public float fallTime;

    private float disabled;

    public Game gameManager;

    void Update()
    {
        if (!gameManager.gameOver)
        {
            if (disabled > 0)
            {
                disabled -= Time.deltaTime;
            }
            else
            {
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    disabled = fallTime;
                    dropCollider.enabled = false;
                }
                else
                {
                    dropCollider.enabled = true;
                }
            }
        }
        else
        {
            dropCollider.enabled = false;
        }
    }
}
