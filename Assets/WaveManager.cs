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

    public void EnemyLeft()
    {
        amountOfEnemiesLeft++;
        if (amountOfEnemiesLeft >= activeEnemies)
        {
            waveCount++;
            switch (waveCount)
            {
                case 2:
                    foreach (var enemy in wave2)
                    {
                        enemy.gameObject.SetActive(true);
                    }
                    activeEnemies += wave2.Count;
                    break;
                case 3: 
                    foreach (var enemy in wave3)
                    {
                        enemy.gameObject.SetActive(true);
                    }
                    activeEnemies += wave3.Count;
                    break;
                case 4:
                    foreach (var enemy in wave4)
                    {
                        enemy.gameObject.SetActive(true);
                    }
                    activeEnemies += wave4.Count;
                    break;
                default:
                    break;
            }
        }
    }


}
