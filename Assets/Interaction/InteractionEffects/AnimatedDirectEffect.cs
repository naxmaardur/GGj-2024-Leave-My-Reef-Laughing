using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimatedDirectEffect", menuName = "ScriptableObjects/AnimatedDirectEffect", order = 1),System.Serializable]
public class AnimatedDirectEffect : DirectEffect
{
    public string AnimationTrigger;
    public override void TriggerEffect(IPickUpAble pickUp, Interactable interactable)
    {
        if (pickUp.UsedInEffects())
        {
            GameManager.Instance?.StartCoroutine(EffectTimer(pickUp));
        }
        interactable.Interacted();

        Animator animator = interactable.GetComponentInParent<Animator>();
        if (animator == null)
        {
            animator = interactable.GetComponent<Animator>();
        }

        animator.SetTrigger(AnimationTrigger);
        AddFunnyToInteractableOwner(interactable);
    }
}
