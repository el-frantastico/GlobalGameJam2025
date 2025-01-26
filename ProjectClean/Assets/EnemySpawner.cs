using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    public GameObject enemyPrefab;
    public float startDelay;
    public float minDelay;
    public float delayDecrease;

    public Transform[] spawnPositions;

    public AudioSource _audioSound;

    private bool isSpawning = false;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
        isSpawning = false;
    }
    public void StartSpawning()
    {
        if(isSpawning)
        {
            return;
        }
        isSpawning = true;
        _audioSound.Play();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        float currentDelay = startDelay;

        while(true)
        {
            //find spawn location
            Vector3 spawnLocation = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
            Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);

            yield return new WaitForSeconds(currentDelay);
            currentDelay -= delayDecrease;
            currentDelay = Mathf.Clamp(currentDelay, minDelay, startDelay);
        }

    }

    
}
