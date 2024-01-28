using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int waveCount = 1;

    private int amountOfEnemiesLeft = 0;

    private int activeEnemies = 5;

    [SerializeField] private List<EnemyBehaviour> wave1;
    [SerializeField] private List<EnemyBehaviour> wave2;
    [SerializeField] private List<EnemyBehaviour> wave3;
    [SerializeField] private List<EnemyBehaviour> wave4;

    [SerializeField] private float spawnTimer = 3f;

    private List<EnemyBehaviour> enemiesToSpawn;
    
    private float timer = 0;

    private void Start()
    {
        foreach (EnemyBehaviour enemy in wave1)
        {
            enemiesToSpawn.Add(enemy);
        }
        
        enemiesToSpawn[0].gameObject.SetActive(true);
        enemiesToSpawn.Remove(enemiesToSpawn[0]);
    }

    public void EnemyLeft()
    {
        amountOfEnemiesLeft++;

        if (timer >= spawnTimer && enemiesToSpawn.Count > 0)
        {
            timer = 0;
            enemiesToSpawn[0].gameObject.SetActive(true);
            enemiesToSpawn.Remove(enemiesToSpawn[0]);
        }
        
        if (amountOfEnemiesLeft >= activeEnemies)
        {
            waveCount++;
            switch (waveCount)
            {
                case 2:
                    foreach (var enemy in wave2)
                    {
                        enemiesToSpawn.Add(enemy);
                    }
                    activeEnemies += wave2.Count;
                    break;
                case 3: 
                    foreach (var enemy in wave3)
                    {
                        enemiesToSpawn.Add(enemy);
                    }
                    activeEnemies += wave3.Count;
                    break;
                case 4:
                    foreach (var enemy in wave4)
                    {
                        enemiesToSpawn.Add(enemy);
                    }
                    activeEnemies += wave4.Count;
                    break;
                default:
                    break;
            }
        }
    }


}
