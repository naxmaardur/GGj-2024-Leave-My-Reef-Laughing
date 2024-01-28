using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public Interactable interactable;
    
    public PickUpObject PickupTrap;

    public int range = 5;

    private bool checkForEnemy = true;

    [SerializeField]
    private float startdelay;

    [SerializeField]
    private SpriteRenderer sprite;

    private IEnumerator Start()
    {
        Color c = sprite.color;
        c.a = 0.5f;
        sprite.color = c;
        checkForEnemy = false;
        yield return new WaitForSeconds(startdelay);
        checkForEnemy = true;
        c.a = 1f;
        sprite.color = c;
    }


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
