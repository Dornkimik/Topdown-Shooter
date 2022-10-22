using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyScript : MonoBehaviour, IDamageable
{
    private SpawnWaves waveSpawner;

    private AudioSource hitSound;
    public EnemySO enemySO;
    [SerializeField] private GameObject hitParticle;

    private int health;

    private SpriteRenderer spriteRenderer;

    private GameObject player;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hitSound = GameObject.Find("HitSoundManager").GetComponent<AudioSource>();
        waveSpawner = GameObject.Find("Enemy Spawner").GetComponent<SpawnWaves>();
    }

    void Start()
    {
        health = enemySO.health;
        spriteRenderer.color = enemySO.color;

        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        FollowPlayer();
        DeathConditions();
    }

    private void DeathConditions()
    {
        if (health <= 0)
        {
            waveSpawner.activeEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void FollowPlayer()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.transform.position) > 0.8f)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemySO.speed * waveSpawner.speedOverTime * Time.deltaTime);
            }
            else
            {
                Destroy(player);
            }
        }
    }

    void IDamageable.Damage(int damageAmount)
    {
        health -= damageAmount;
        GameObject spawnedParticle = Instantiate(hitParticle, transform.position, Quaternion.identity);

        spawnedParticle.transform.SetParent(gameObject.transform);

        var main = spawnedParticle.GetComponent<ParticleSystem>().main;
        main.startColor = enemySO.color;

        hitSound.Play();

        Destroy(spawnedParticle, 2f);
        print(health);
    }
}
