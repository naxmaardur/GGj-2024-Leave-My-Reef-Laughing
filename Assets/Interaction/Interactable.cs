using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable: MonoBehaviour
{
    [SerializeField]
    private float cooldown;
    private float cooldownTimer;
    [SerializeField]
    private GameObject highlight;
    public InteractionEffect InteractionEffect;

    private void Update()
    {
        if(cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
    public bool IsInteractable()
    {
        return cooldownTimer > 0?false:true;
    }
    public void ToggleHighlight(bool onState)
    {
        highlight.SetActive(onState);
    }
    public void Interacted()
    {
        cooldownTimer = cooldown;
    }
}
