using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Vector2 gameBounds;

    public float timeElapsed;

    public bool gameStarted;
    public Enemy[] enemies;

    public float worldSapping;
    public float breakingPoint;
    public Text sapText;

    public bool dying;
    public float deathTime;
    public Animator deathAnimator;

    public GameObject player;

    public int rampUpTime;
    public int numberOfRamps = 0;
    private int lastRamp = -1;
    Dictionary<int, Enemy> chanceDict;

    public float[] spawnRates;

    private float nextSpawnTime;

    public Attacks attacks;
    public GameObject tutorialUI;

    public List<Enemy> enemieList;

    public GameObject gameOverUI;
    public Animator gameOverAnimator;
    public Text timeSurvivedText;
    public Text enemyKillText;
    public Text timesShotText;
    public Text lasersShotText;
    public Text wavesShotText;


    // Random Statistics
    public int enemiesKilled;
    public int timesShot;
    public int lasersShot;
    public int wavesShot;

    public bool gameOver;

    public void StartGame()
    {
        attacks.laserCount = 0;
        attacks.waveCount = 0;
        attacks.SetText();

        Destroy(tutorialUI);

        gameStarted = true;
        timeElapsed = 0;
    }

    private void SpawnEnemy(Enemy enemy, Vector2 spawnPos)
    {
        if (!gameOver)
        {
            if (enemieList == null)
            {
                enemieList = new List<Enemy>();
            }

            Enemy newEnemy = Instantiate(enemy.gameObject).GetComponent<Enemy>();

            enemieList.Add(newEnemy);

            newEnemy.Spawn(spawnPos, gameObject.GetComponent<Game>());
        }
    }

    private void Update()
    {
        if (!gameOver)
        {
            if (gameStarted)
            {
                timeElapsed += Time.deltaTime;

                if (timeElapsed > (rampUpTime * (numberOfRamps + 1)))
                {
                    numberOfRamps++;

                }

                if (timeElapsed >= nextSpawnTime)
                {
                    Enemy chosenEnemy = PickRandomEnemy();

                    float spawnX = Random.Range(-gameBounds.x, gameBounds.x) + transform.position.x;
                    float spawnY = Random.Range(-gameBounds.y, gameBounds.y) + transform.position.y;

                    Vector2 spawnPos = new Vector2(spawnX, spawnY);

                    SpawnEnemy(chosenEnemy, spawnPos);
                    if (numberOfRamps < spawnRates.Length)
                    {
                        nextSpawnTime += spawnRates[numberOfRamps];
                    }
                    else
                    {
                        nextSpawnTime += spawnRates[spawnRates.Length - 1];
                    }
                }
            }
            else
            {
                attacks.laserCount = 1;
                attacks.waveCount = 1;
                attacks.SetText();
            }


            if (dying)
            {
                if (deathTime >= 5)
                {
                    player.GetComponent<Animator>().SetBool("Lost", true);
                    Destroy(player.GetComponent<Movement>());
                    Destroy(player.GetComponent<Attacks>());
                    player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    player.GetComponent<Rigidbody2D>().gravityScale = 4;
                    gameOver = true;

                    timeSurvivedText.text = timeSurvivedText.text + Mathf.Floor(timeElapsed).ToString() + " Seconds";
                    enemyKillText.text = enemyKillText.text + enemiesKilled.ToString();
                    timesShotText.text = timesShotText.text + timesShot.ToString();
                    lasersShotText.text = lasersShotText.text + lasersShot.ToString();
                    wavesShotText.text = wavesShotText.text + wavesShot.ToString();

                    gameOverAnimator.SetTrigger("Display");
                    gameOverUI.SetActive(true);

                    print("death");
                }
                deathTime += Time.deltaTime;
            }
        }
    }

    public void SpawnRandomAtPoint(Vector2 spawnPos)
    {
        Enemy chosenEnemy = PickRandomEnemy();

        SpawnEnemy(chosenEnemy, spawnPos);
    }

    public void IncreaseSap(float amount)
    {
        worldSapping += amount;

        if (worldSapping < 0)
        {
            worldSapping = 0;
        }

        sapText.text = string.Format("{0}/{1}", worldSapping, breakingPoint);

        if (worldSapping > breakingPoint)
        {
            dying = true;
            deathAnimator.SetBool("Dying", true);
        }
        else
        {
            deathTime = 0;
            dying = false;
            deathAnimator.SetBool("Dying", false);
        }
    }

    private Enemy PickRandomEnemy()
    {
        if (lastRamp < numberOfRamps || chanceDict == null)
        {
            // Build new Chance Dict
            chanceDict = new Dictionary<int, Enemy>();
            int nextDictInd = 0;

            for (int i = 0; i < enemies.Length; i++)
            {
                Enemy checkEnemy = enemies[i];

                int numberOfRolls = 0;
                if (checkEnemy.spawnWeights.Length > numberOfRamps)
                {
                    numberOfRolls = checkEnemy.spawnWeights[numberOfRamps];
                }
                else
                {
                    numberOfRolls = checkEnemy.spawnWeights[checkEnemy.spawnWeights.Length - 1];
                }

                for (int j = 0; j < numberOfRolls; j++)
                {
                    chanceDict[nextDictInd] = checkEnemy;

                    nextDictInd++;
                }
            }
        }

        // Pick Enemy
        int enemyInd = Random.Range(0, chanceDict.Count);
        Enemy chosenEnemy = chanceDict[enemyInd];

        return chosenEnemy;
    }

    public void CountDeath()
    {
        enemiesKilled++;
    }

    public void CountShot()
    {
        if (gameStarted)
        {
            timesShot++;
        }
    }

    public void CountLaser()
    {
        if (gameStarted)
        {
            lasersShot++;
        }
    }

    public void CountWave()
        {
        if (gameStarted)
        {
            wavesShot++;
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
