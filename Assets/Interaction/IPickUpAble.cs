using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPickUpAble
{
    public GameObject Gameobject { get; set; }
    public Transform FollowPoint { get; set; }
    public bool IsPlacable();
    public bool PlaceObject();
    public void UseInteraction(Interactable interactable);
    public void PickUp(Transform followPoint);
    public void LetGo();
    public bool CheckInteractionCompatibility(Interactable interactable);
    public bool UsedInEffects();

    public void ToggleHighlight(bool onState);
}
