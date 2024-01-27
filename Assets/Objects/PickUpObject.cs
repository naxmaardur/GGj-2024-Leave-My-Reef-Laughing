using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Unity.Mathematics;

[RequireComponent(typeof(CircleCollider2D))]
public class PickUpObject : MonoBehaviour, IPickUpAble
{
    public GameObject Gameobject { get; set; }
    public Transform FollowPoint { get; set; }
    [SerializeField]
    bool placable = false;
    [SerializeField]
    GameObject Highlight;
    [SerializeField]
    bool usedInEffect;
    [SerializeField] 
    private TrapScript trapPrefab;
    Rigidbody2D rb;

    [Expandable]
    [SerializeReference] InteractionEffect[] effects;

    void Start()
    {
        Gameobject = gameObject;
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (FollowPoint != null)
        {
            transform.position = FollowPoint.position;
        }
    }

    public bool IsPlacable()
    {
        return placable;
    }

    public void LetGo()
    {
        FollowPoint = null;
        rb.isKinematic = false;
        gameObject.layer = LayerMask.NameToLayer("PickUpAble");
    }

    public void PickUp(Transform followPoint)
    {
        FollowPoint = followPoint;
        rb.isKinematic = true;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public bool PlaceObject()
    {
        if (placable)
        {
            TrapScript trap = Instantiate(trapPrefab, transform.position, quaternion.identity);
            trap.PickupTrap = this;
            LetGo();
            this.gameObject.SetActive(false);
            return true;
        }
        else
        {
            LetGo();
            return false;
        }
    }

    public void ToggleHighlight(bool onState)
    {
        Highlight.SetActive(onState);
    }

    public void UseInteraction(Interactable interactable)
    {
        LetGo();
        interactable.InteractionEffect.TriggerEffect(this, interactable);
    }

    public bool CheckInteractionCompatibility(Interactable interactable)
    {
        foreach (InteractionEffect interactionEffect in effects)
        {
            if (interactionEffect == interactable.InteractionEffect)
            {
                return true;
            }
        }
        return false;
    }

    public bool UsedInEffects()
    {
        return usedInEffect;
    }

    public InteractionEffect ThisEffect()
    {
        return effects[0];
    }
}
