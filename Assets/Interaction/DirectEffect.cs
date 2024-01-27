using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DirectEffect", menuName = "ScriptableObjects/DirectEffect", order = 1), System.Serializable]
public class DirectEffect : InteractionEffect
{
    public override void TriggerEffect(IPickUpAble pickUp, Interactable interactable)
    {
        if (pickUp.UsedInEffects())
        {
             GameManager.Instance?.StartCoroutine(EffectTimer(pickUp));
        }
        if (effectVisuals != null)
        {
            Instantiate(effectVisuals, interactable.transform.position, interactable.transform.rotation);
        }
        interactable.Interacted();
        AddFunnyToInteractableOwner(interactable);
    }

    protected void AddFunnyToInteractableOwner(Interactable interactable)
    {
        EnemyBehaviour npcControler = interactable.GetComponentInParent<EnemyBehaviour>();
        npcControler.TryAddFunny(funnyValue);
    }
}
