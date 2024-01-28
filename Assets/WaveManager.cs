using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int waveCount = 0;

    private int amountOfEnemiesLeft = 0;

    private int activeEnemies = 1;

    [SerializeField] private GameObject winScreen;

    [SerializeField] private List<EnemyBehaviour> wave0;
    [SerializeField] private List<EnemyBehaviour> wave1;
    [SerializeField] private List<EnemyBehaviour> wave2;
    [SerializeField] private List<EnemyBehaviour> wave3;
    [SerializeField] private List<EnemyBehaviour> wave4;

    [SerializeField] private float spawnTimer = 3f;

    private List<EnemyBehaviour> enemiesToSpawn = new List<EnemyBehaviour>();
    
    private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        
        
        if (timer >= spawnTimer && enemiesToSpawn.Count > 0)
        {
            enemiesToSpawn[0].gameObject.SetActive(true);
            enemiesToSpawn.Remove(enemiesToSpawn[0]);
            timer = 0;
        }
    }

    public void EnemyLeft()
    {
        amountOfEnemiesLeft++;

        if (amountOfEnemiesLeft >= activeEnemies)
        {
            waveCount++;
            switch (waveCount)
            {
                case 1:
                    foreach (var enemy in wave1)
                    {
                        enemiesToSpawn.Add(enemy);
                    }
                    activeEnemies += wave1.Count;
                    break;
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
                case 5:
                    winScreen.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }


}
