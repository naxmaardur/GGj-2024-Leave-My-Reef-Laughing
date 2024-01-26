using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectMaster : MonoBehaviour
{
    IPickUpAble currentObject;

    IPickUpAble pickUpAbleTarget;

    Iinteractable interactableTarget;

    [SerializeField]
    Transform pickUpPoint;

    [SerializeField]
    LayerMask pickUpLayer;
    [SerializeField]
    LayerMask interactableLayer;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
            return;
        }
        if (pickUpAbleTarget != null)
        {
            //pick up object
            currentObject = pickUpAbleTarget;
            currentObject.PickUp(pickUpPoint);
            PlayerInputHandler.Instance.ActionPickUpUsed();
        }
    }

    private void TriggerUse()
    {
        if (currentObject == null) { return; }
        if ((interactableTarget != null && !currentObject.IsPlacable()))
        {
            if (!interactableTarget.IsInteractable()) { return; }
            interactableTarget.UseInteraction(currentObject);
            PlayerInputHandler.Instance.ActionUseUsed();
        }
        if (currentObject.IsPlacable())
        {
            currentObject.PlaceObject();
            currentObject = null;
            PlayerInputHandler.Instance.ActionUseUsed();
        }
    }

    private void CheckForObjects()
    {
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
            if (pickUpAbleTarget != iPickUp)
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
                pickUpAbleTarget.ToggleHighlight(true);

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
            Iinteractable iInteractable = interactable.GetComponent<Iinteractable>();
            if (interactableTarget != iInteractable)
            {
                interactableTarget.ToggleHighlight(false);
            }
            interactableTarget = iInteractable;
            if (currentObject != null && !currentObject.IsPlacable())
            {
                interactableTarget.ToggleHighlight(true);
            }
        }
        else
        {
            if (interactableTarget != null)
            {
                interactableTarget.ToggleHighlight(true);
            }
            interactableTarget = null;
        }
    }
}
