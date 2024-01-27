using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;
    
    public List<Transform> visibleTargets = new List<Transform>();
    

    public bool CheckForObjectBetweenAngle(Vector3 target)
    {
        Vector3 dirToTarget = (target - transform.position).normalized;
        return (Vector3.Angle(transform.up, dirToTarget) < viewAngle / 2);
    }
    
    
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}
