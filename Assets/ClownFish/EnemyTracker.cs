using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    [SerializeField]
    private GameObject lastDiver;
    [SerializeField]
    private GameObject pivotPoint;
    private bool arrowShown = false;
    private float enemydistance;
    [SerializeField]
    private float minDistance = 10;
    // Start is called before the first frame update
    void Start()
    {
        //add unity event here for last diver
    }

    // Update is called once per frame
    void Update()
    {
        if (lastDiver != null)
        {
            enemydistance = Vector2.Distance(transform.position, lastDiver.transform.position);
            Debug.Log(enemydistance);

        }

        if (GameManager.Instance.OneEnemyActive && !arrowShown)
        {
            ShowArrow();
        }
        if (!GameManager.Instance.OneEnemyActive && arrowShown)
        {
            HideArrow();
        }

        if (arrowShown && lastDiver == null) HideArrow();
        if (lastDiver != null && enemydistance >= minDistance)
        {
            pivotPoint.SetActive(true);
        }
        else if(lastDiver == null || enemydistance < minDistance)
        {
            pivotPoint.SetActive(false);
        }
        if (arrowShown)
        {
            pivotPoint.transform.right = lastDiver.transform.position - transform.position;
        }

    }

    private void ShowArrow()
    {
        arrowShown = true;
        lastDiver = FindObjectOfType<EnemyBehaviour>().gameObject;
    }

    private void HideArrow()
    {
        lastDiver = null;
        arrowShown = false;
    }
}
