using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public GameObject player;

    public Projectile heartProj;

    private Attacks attacks;

    public ParticleSystem bubbleParticles;
    public ParticleSystem heartParticles;

    public Carryable startCrystal;
    public Game gameManager;

    private void Start()
    {
        attacks = player.GetComponent<Attacks>();
    }

    public void Brew(Carryable carryable)
    {
        if (carryable == startCrystal)
        {
            heartParticles.Emit(500);
            gameManager.StartGame();
        }

        player.GetComponent<Mana>().AddMana(carryable.manaGive);

        if (carryable.projGive != null) {
            if (carryable.projGive == heartProj)
            {
                heartParticles.Emit(400);
                attacks.ShootProjectile(heartProj);
            }
            else
            {
                bubbleParticles.Emit(50);
                attacks.AddAttack(carryable.projGive);
            }
        }
        

        Destroy(carryable.gameObject);
    }
}
