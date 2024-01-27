using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class FishAttributes : MonoBehaviour
{
    public bool isInTunnel { get; private set; }
    [Tag,SerializeField]
    private string tunnelTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == tunnelTag)
        {
            isInTunnel = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == tunnelTag)
        {
            isInTunnel = false;
        }
    }
}
