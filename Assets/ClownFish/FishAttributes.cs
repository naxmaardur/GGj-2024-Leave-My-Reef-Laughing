using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class FishAttributes : MonoBehaviour
{
    public bool isInTunnel { get; private set; }
    [Tag,SerializeField]
    private string tunnelTag;

    [SerializeField] private int playerHealth = 3;
    [SerializeField] private ParticleSystem DamageParticles;

    public float TimeOutsideOfTunnel { get; private set; }
    public float chaseTime;
    


    [Button]
    public void takeDamage()
    {
        playerHealth -= 1;
        if (playerHealth <= 0) { GameManager.Instance.GameOver(); }
        DamageParticles.Play();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == tunnelTag)
        {
            isInTunnel = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == tunnelTag)
        {
            isInTunnel = false;
        }
    }

    private void Update()
    {
        if (!isInTunnel && TimeOutsideOfTunnel < chaseTime)
        {
            TimeOutsideOfTunnel += Time.deltaTime;
        }
        if(isInTunnel && TimeOutsideOfTunnel > 0)
        {
            TimeOutsideOfTunnel -= Time.deltaTime * 4;
        }
    }

}
