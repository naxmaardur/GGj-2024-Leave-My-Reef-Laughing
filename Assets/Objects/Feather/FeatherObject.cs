using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class FeatherObject : MonoBehaviour, IPickUpAble
{
    public GameObject Gameobject { get; set; }
    public Transform FollowPoint { get; set; }
    [SerializeField]
    bool placable = false;

    void Start()
    {
        Gameobject = gameObject;
    }
    void FixedUpdate()
    {
        if(FollowPoint != null)
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
        gameObject.layer = LayerMask.NameToLayer("PickUpAble");
    }

    public void PickUp(Transform followPoint)
    {
        FollowPoint = followPoint;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public bool PlaceObject()
    {
        LetGo();
        throw new System.NotImplementedException();
    }

    public void ToggleHighlight(bool onState)
    {
        throw new System.NotImplementedException();
    }
}
