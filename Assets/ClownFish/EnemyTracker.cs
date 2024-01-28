using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    [SerializeField]
    private GameObject lastDiver;
    [SerializeField] 
    private GameObject pivotPoint;
    private bool arrowShown;
    // Start is called before the first frame update
    void Start()
    {
        //add unity event here for last diver
    }

    // Update is called once per frame
    void Update()
    {
        if (arrowShown && lastDiver == null) HideArrow();
        if (arrowShown)
        {
            pivotPoint.transform.right = lastDiver.transform.position - transform.position;
        }
        
    }

    private void ShowArrow()
    {
        arrowShown = true;
        pivotPoint.SetActive(true);
        lastDiver = FindObjectOfType<EnemyBehaviour>().gameObject;
    }

    private void HideArrow()
    {
        lastDiver = null;
        arrowShown = false;
        pivotPoint.SetActive(false);
    }
}
