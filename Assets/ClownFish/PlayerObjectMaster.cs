using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectMaster : MonoBehaviour
{
    IPickUpAble currentObject;

    IPickUpAble pickUpAbleTarget;

    Interactable interactableTarget;

    [SerializeField]
    Transform pickUpPoint;

    [SerializeField]
    LayerMask pickUpLayer;
    [SerializeField]
    LayerMask interactableLayer;

    [SerializeField]
    BoxCollider2D boxCollider;

    FishAttributes fishAttributes;



    void Start()
    {
        fishAttributes = GetComponent<FishAttributes>();
    }

    void Update()
    {
        CheckForObjects();
        CheckForInteractables();

        if (PlayerInputHandler.Instance.PickUpPressed)
        {
            TriggerPickUpAction();
        }
        if (PlayerInputHandler.Instance.UsePressed)
        {
            TriggerUse();
        }
    }


    private void TriggerPickUpAction()
    {
        if (currentObject != null)
        {
            //drop object
            currentObject.LetGo();
            currentObject = null;
            PlayerInputHandler.Instance.ActionPickUpUsed();
            boxCollider.enabled = false;
            return;
        }
        if (pickUpAbleTarget != null)
        {
            //pick up object
            currentObject = pickUpAbleTarget;
            currentObject.PickUp(pickUpPoint);
            PlayerInputHandler.Instance.ActionPickUpUsed();
            boxCollider.enabled = true;
        }
    }

    private void TriggerUse()
    {
        if (currentObject == null) { return; }
        if ((interactableTarget != null && !currentObject.IsPlacable()))
        {
            if (!interactableTarget.IsInteractable()) { return; }
            currentObject.UseInteraction(interactableTarget);
            PlayerInputHandler.Instance.ActionUseUsed();
            currentObject = null;
            boxCollider.enabled = false;
            return;
        }
        if (currentObject.IsPlacable())
        {
            currentObject.PlaceObject();
            currentObject = null;
            PlayerInputHandler.Instance.ActionUseUsed();
            boxCollider.enabled = false;
        }
    }

    private void CheckForObjects()
    {
        if (fishAttributes.isInTunnel)
        {
            if (pickUpAbleTarget != null)
            {
                pickUpAbleTarget.ToggleHighlight(false);

            }
            pickUpAbleTarget = null;
            return;
        }
        Collider2D[] pickUps = Physics2D.OverlapCircleAll(pickUpPoint.position, 0.3f, pickUpLayer);

        Collider2D pickUp = null;
        float dist = Mathf.Infinity;
        foreach (Collider2D c in pickUps)
        {
            float newDist = Vector2.Distance(pickUpPoint.position, c.transform.position);
            if (dist > newDist)
            {
                pickUp = c;
                dist = newDist;
            }
        }

        if (pickUp != null)
        {
            IPickUpAble iPickUp = pickUp.GetComponent<IPickUpAble>();
            if (pickUpAbleTarget != iPickUp && pickUpAbleTarget != null)
            {
                pickUpAbleTarget.ToggleHighlight(false);
            }
            pickUpAbleTarget = iPickUp;
            pickUpAbleTarget.ToggleHighlight(true);
        }
        else
        {
            if (pickUpAbleTarget != null)
            {
                pickUpAbleTarget.ToggleHighlight(false);

            }
            pickUpAbleTarget = null;
        }
    }


    private void CheckForInteractables()
    {
        Collider2D[] interactables = Physics2D.OverlapCircleAll(pickUpPoint.position, 0.3f, interactableLayer);

        Collider2D interactable = null;
        float dist = Mathf.Infinity;
        foreach (Collider2D c in interactables)
        {
            float newDist = Vector2.Distance(pickUpPoint.position, c.transform.position);
            if (dist > newDist)
            {
                interactable = c;
                dist = newDist;
            }
        }

        if (interactable != null)
        {
            Interactable iInteractable = interactable.GetComponent<Interactable>();
            if (interactableTarget != iInteractable && interactableTarget != null)
            {
                interactableTarget.ToggleHighlight(false);
            }
            interactableTarget = iInteractable;
            if (currentObject != null && !currentObject.IsPlacable() && currentObject.CheckInteractionCompatibility(interactableTarget))
            {
                interactableTarget.ToggleHighlight(true);
            }
        }
        else
        {
            if (interactableTarget != null)
            {
                interactableTarget.ToggleHighlight(false);
            }
            interactableTarget = null;
        }
    }
}
