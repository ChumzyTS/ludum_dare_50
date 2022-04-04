using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPop : MonoBehaviour
{
    public ParticleSystem particles;

    private float aliveTime;

    // Start is called before the first frame update
    void Start()
    {
        particles.Emit(30);
    }

    private void Update()
    {
        aliveTime += Time.deltaTime;

        if (aliveTime >= 2)
        {
            Destroy(gameObject);
        }
    }
}
