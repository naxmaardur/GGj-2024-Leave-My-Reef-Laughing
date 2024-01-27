using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public Interactable interactable;
    
    public PickUpObject PickupTrap;

    public int range = 5;

    private bool checkForEnemy = true;

    void Update()
    {
        if (checkForEnemy)
            if (CheckForEnemy())
            {
                interactable.InteractionEffect.TriggerEffect(PickupTrap, interactable);
                IEnumerator coroutine = ConvertToPickupTrap();
                StartCoroutine(coroutine);
                checkForEnemy = false;
            }
    }

    bool CheckForEnemy()
    {
        Collider2D[] npcs = Physics2D.OverlapCircleAll(interactable.transform.position, range, LayerMask.GetMask("NPC"));
        return npcs.Length > 0;
    }

    IEnumerator ConvertToPickupTrap()
    {
        yield return new WaitForSeconds(interactable.InteractionEffect.effectDuration);
        PickupTrap.gameObject.SetActive(true);
        Destroy(this.gameObject);
    }
}
