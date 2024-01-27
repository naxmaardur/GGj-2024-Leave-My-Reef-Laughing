using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractionEffect", menuName = "ScriptableObjects/InteractionEffect", order = 1), System.Serializable]
public class InteractionEffect : ScriptableObject
{
    public float effectDuration;
    public float funnyValue;
    public float range;
    public GameObject effectVisuals;

    public virtual void TriggerEffect(IPickUpAble pickUp, Interactable interactable)
    {
        if (pickUp.UsedInEffects())
        {
             GameManager.Instance?.StartCoroutine(EffectTimer(pickUp));
        }
        interactable.Interacted();
        if(effectVisuals != null)
        {
            Instantiate(effectVisuals, interactable.transform.position, interactable.transform.rotation);
        }
        AddFunnyToNPCsInRange(interactable);
    }

    protected void AddFunnyToNPCsInRange(Interactable interactable)
    {
        Collider2D[] npcs = Physics2D.OverlapCircleAll(interactable.transform.position, range, LayerMask.NameToLayer("NPC"));
        foreach (Collider2D collider in npcs)
        {
            if(collider.transform == interactable.transform.parent) { continue; }
            EnemyBehaviour npcControler = collider.GetComponent<EnemyBehaviour>();
            npcControler.TrySeeFunny(funnyValue,interactable.transform.position);
        }
    }


    public IEnumerator EffectTimer(IPickUpAble pickUp)
    {
        pickUp.Gameobject.SetActive(false);
        yield return new WaitForSeconds(effectDuration);
        pickUp.Gameobject.SetActive(true);
    }

}
