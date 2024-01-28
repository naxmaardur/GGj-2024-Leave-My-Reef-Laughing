using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AnimatedInteractionEffect", menuName = "ScriptableObjects/AnimatedInteractionEffect", order = 1), System.Serializable]
public class AnimatedInteraction : InteractionEffect
{
    public string AnimationTrigger;
    public override void TriggerEffect(IPickUpAble pickUp, Interactable interactable)
    {
        if (pickUp.UsedInEffects())
        {
            GameManager.Instance?.StartCoroutine(EffectTimer(pickUp));
        }
        interactable.Interacted();

        Animator animator = interactable.transform.root.GetComponent<Animator>();
        if(animator == null)
        {
            animator = interactable.GetComponent<Animator>();
        }

        animator.SetTrigger(AnimationTrigger);
        AddFunnyToNPCsInRange(interactable);
    }
}
