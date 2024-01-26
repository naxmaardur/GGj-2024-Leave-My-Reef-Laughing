using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iinteractable
{
    public GameObject Gameobject { get; set; }
    public bool IsInteractable();

    public void UseInteraction(IPickUpAble pickUpAble);
    public void ToggleHighlight(bool onState);
}
