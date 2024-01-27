using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractionEffect", menuName = "ScriptableObjects/InteractionEffect", order = 1)]
public class InteractionEffect : ScriptableObject
{
    public float EffectDuration;
    private Coroutine timerCoroutine;
    public virtual void TriggerEffect(IPickUpAble pickUp, Interactable interactable)
    {
        if (pickUp.UsedInEffects())
        {
            timerCoroutine = GameManager.Instance?.StartCoroutine(EffectTimer(pickUp));
        }
        interactable.Interacted();
    }


    IEnumerator EffectTimer(IPickUpAble pickUp)
    {
        pickUp.Gameobject.SetActive(false);
        yield return new WaitForSeconds(EffectDuration);
        pickUp.Gameobject.SetActive(true);
    }

}
