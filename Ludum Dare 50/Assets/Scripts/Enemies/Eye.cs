using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : Enemy
{
    public GameObject pupil;
    public float pupilDistance;

    void Update()
    {
        Vector2 playerPos = gameManager.player.transform.position;
        Vector2 eyePos = transform.position;
        Vector2 eyeDir = playerPos - eyePos;

        float radians = Mathf.Atan2(eyeDir.y, eyeDir.x);

        pupil.transform.position = eyePos + new Vector2(pupilDistance * Mathf.Cos(radians), pupilDistance * Mathf.Sin(radians));
    }
}
