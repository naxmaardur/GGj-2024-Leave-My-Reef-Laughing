using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VisualsRotation : MonoBehaviour
{
    NavMeshAgent agent;
    EnemyBehaviour enemy;

    float characterXsize;
    Quaternion lastRotation;

    private void Start()
    {
        enemy = GetComponentInParent<EnemyBehaviour>();
        agent = GetComponentInParent<NavMeshAgent>();
        characterXsize = transform.localScale.x;
        lastRotation = transform.rotation;
    }

    private void Update()
    {
        lastRotation = transform.rotation;
        Vector3 desitedDirection;
        if(enemy.currentState == STATES.idel)
        {
            desitedDirection = Vector3.up;
        }
        else
        {
            desitedDirection = agent.desiredVelocity;
        }
        float angle = Mathf.Atan2(desitedDirection.x, desitedDirection.y) * Mathf.Rad2Deg;
        Quaternion newRot = Quaternion.Euler(new Vector3(0, 0, -angle));
        transform.rotation = Quaternion.Lerp(lastRotation, newRot, 0.01f);

        if (angle > 90 || angle < -90)
        {
            Vector3 scale = transform.localScale;
            scale.x = characterXsize;
            transform.localScale = scale;
        }
        else
        {
            Vector3 scale = transform.localScale;
            scale.x = -characterXsize;
            transform.localScale = scale;
        }
    }
}
