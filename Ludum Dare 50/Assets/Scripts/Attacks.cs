using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attacks : MonoBehaviour
{
    private Carry carry;

    public Projectile mainShot;

    public Projectile laserShot;
    public Projectile waveShot;

    public int laserCount;
    public int waveCount;

    public Text laserUIText;
    public Text waveUIText;

    public GameObject arm;
    public GameObject body;
    public float armDistance;
    public Vector2 armOffset;

    public Vector2 holdOffset;

    public Game gameManager;

    // Start is called before the first frame update
    void Start()
    {
        carry = gameObject.GetComponent<Carry>();
    }

    // Update is called once per frame
    void Update()
    {
        if (carry.heldCarryable == null)
        {
            // Set Arm
            Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            float radians = Mathf.Atan2(direction.y, direction.x);
            float angle = radians * Mathf.Rad2Deg;

            arm.transform.position = (Vector2)body.transform.position + new Vector2(armDistance * Mathf.Cos(radians), armDistance * Mathf.Sin(radians)) + armOffset;
            arm.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.F))
            {
                gameManager.CountShot();
                ShootProjectile(mainShot);
            }
            if (Input.GetKeyDown(KeyCode.Q) && laserCount > 0)
            {
                laserCount--;
                laserUIText.text = laserCount.ToString();
                gameManager.CountLaser();
                ShootProjectile(laserShot);
            }
            if (Input.GetKeyDown(KeyCode.E) && waveCount > 0)
            {
                waveCount--;
                waveUIText.text = waveCount.ToString();
                gameManager.CountWave();
                ShootProjectile(waveShot);
            }
        }
        else
        {
            arm.transform.position = (Vector2)body.transform.position + holdOffset;
            arm.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
    }

    public void ShootProjectile(Projectile proj)
    {
        Projectile newProj = Instantiate(proj.gameObject).GetComponent<Projectile>();

        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);

        direction = direction / (Mathf.Abs(direction.x) + Mathf.Abs(direction.y));

        newProj.Spawn(arm.transform.position, direction);
    }

    public void AddAttack(Projectile proj)
    {
        if (proj == laserShot)
        {
            laserCount += 1;
        }
        else if (proj == waveShot)
        {
            waveCount += 1;
        }

        SetText();
    }

    public void SetText()
    {
        laserUIText.text = laserCount.ToString();
        waveUIText.text = waveCount.ToString();
    }
}
