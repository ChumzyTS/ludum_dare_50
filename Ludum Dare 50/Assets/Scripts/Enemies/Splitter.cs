using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : Enemy
{
    public float timeToSplit;
    public float aliveTime;

    public Enemy splitMini;
    public int minSplits;
    public int maxSplits;


    private void Update()
    {
        if (spawned)
        {
            aliveTime += Time.deltaTime;
            if (aliveTime >= timeToSplit)
            {
                print("Split");

                int spawnAmount = Random.Range(minSplits, maxSplits + 1);

                for (int i = 0; i < spawnAmount; i++)
                {
                    Enemy newSplit = Instantiate(splitMini.gameObject).GetComponent<Enemy>();
                    newSplit.Spawn(transform.position, gameManager);
                }

                Destroy(gameObject);
            }
        }
    }
}
