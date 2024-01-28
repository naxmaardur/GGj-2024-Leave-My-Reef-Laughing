using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private Room currentroom;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float calmSpeed = 0.01f;
    [SerializeField] private float normalSpeed = 3.5f;

    private FieldOfView fov;

    private float currentOxygen = 100f;
    private float maxOxygen = 100f;

    private float timerChoice = 0f;

    private float targetTimeChoice = 1f;

    private NavMeshAgent agent;
    private FishAttributes fishAttributes;
    private bool continueChase;

    private float timerInRange = 0f;

    private float targetTimeInRange = 0.75f;

    public STATES currentState = STATES.idel;


    [SerializeField]
    private GameObject[] EnemyModels;
    [SerializeField]
    private GameObject[] HandBones;
    [SerializeField]
    private GameObject Net;
    private int currentModel;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private bool hasFlipers;
    [SerializeField]
    private bool hasMask;

    [SerializeField]
    private GameObject[] flippers;
    [SerializeField]
    private GameObject[] masks;

    [SerializeField]
    private GameObject faceInteractable;
    [SerializeField]
    private GameObject FeetInteractable;
    [SerializeField]
    private ParticleSystem LaughParticle;

    [SerializeField]
    private AudioClip[] laughs;

    private AudioSource source;
    bool canChase = true;



    void Start()
    {
        transform.parent = null;
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        fov = GetComponentInChildren<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
        fishAttributes = FindObjectOfType<FishAttributes>();
        SetEnemyModel(3);

        if (hasFlipers)
        {
            foreach (GameObject g in flippers)
            {
                g.SetActive(true);
            }
            FeetInteractable.SetActive(false);
        }
        if (hasMask)
        {
            foreach (GameObject g in masks)
            {
                g.SetActive(true);
            }
            faceInteractable.SetActive(false);
        }
    }

    void Update()
    {
        animator.SetFloat("Velocity", agent.velocity.magnitude);
        animator.speed = Mathf.Lerp(1, 3, agent.velocity.magnitude / normalSpeed);
        if (currentState == STATES.Dead)
        {
            if (Vector3.Distance(transform.position, agent.destination) < 0.5f)
            {
                GameManager.Instance.DiverExit();
                Destroy(gameObject);
            }
            return;
        }
        if (currentOxygen <= 0)
        {
            //go to the upmost top
            currentState = STATES.Dead;
            agent.SetDestination(GameManager.Instance.NpcDeathPoint);
            SetEnemyModel(0);
            return;
        }

        switch (currentModel)
        {
            case 3:
                if (currentOxygen < 75)
                {
                    SetEnemyModel(2);
                }
                break;
            case 2:
                if (currentOxygen < 50)
                {
                    SetEnemyModel(1);
                }
                break;
        }


        if (!fishAttributes.isInTunnel && fishAttributes.TimeOutsideOfTunnel > fishAttributes.chaseTime && canChase)
        {
            continueChase = true;
        }
        if (continueChase && fishAttributes.TimeOutsideOfTunnel <= 0)
        {
            continueChase = false;
        }

        if (continueChase && currentState != STATES.Dead && !fishAttributes.isInTunnel && !agent.isStopped && canChase && (Vector3.Distance(transform.position, fishAttributes.transform.position) <= fov.viewRadius) && (!Physics2D.Linecast(transform.position, fishAttributes.transform.position, LayerMask.GetMask("Wall"))) && (fov.CheckForObjectBetweenAngle(fishAttributes.transform.position)))
        {
            currentState = STATES.Chase;
            Net.SetActive(true);
        }
        if (currentState == STATES.Chase && fishAttributes.isInTunnel)
        {
            StartMoveToRoom();
            Net.SetActive(false);
            currentState = STATES.move;
        }


        switch (currentState)
        {
            case STATES.idel:
                IdelBehavior();
                break;
            case STATES.move:
                MoveBehavior();
                break;
            case STATES.Chase:
                ChaseBehavior();
                break;
            case STATES.Dead:

                break;
            default:
                IdelBehavior();
                break;
        }
    }

    private void MoveBehavior()
    {
        if (Vector3.Distance(transform.position, agent.destination) < 0.5f)
        {
            currentState = STATES.idel;
        }
    }

    private void StartMoveToRoom()
    {
        int choice = Random.Range(0, 100);
        if (choice > currentOxygen)
        {
            //move a room up
            if (currentroom.topRooms.Count > 0)
            {
                MoveToRoom(currentroom.topRooms[Random.Range(0, currentroom.topRooms.Count - 1)]);
                return;
            }
        }
        MakeADescicion(choice);
        timerChoice = 0;
        //generate a random time 
        targetTimeChoice = Random.Range(0f, 7f);
        currentState = STATES.move;
    }

    private void IdelBehavior()
    {
        timerChoice += Time.deltaTime;
        if (timerChoice > targetTimeChoice)
        {
            StartMoveToRoom();
            currentState = STATES.move;
            return;
        }
        //agent.SetDestination(transform.position);
    }

    private void ChaseBehavior()
    {
        //go chase player
        agent.speed = normalSpeed;
        agent.SetDestination(fishAttributes.transform.position);

        if (Vector3.Distance(transform.position, agent.destination) < 2f)
        {
            if (timerInRange < targetTimeInRange)
            {
                timerInRange += Time.deltaTime;

                if (timerInRange >= targetTimeInRange)
                {
                    Attack();
                }
            }
        }
        else
        {
            if (timerInRange > 0)
            {
                timerInRange -= Time.deltaTime;
            }
        }
    }

    void Attack()
    {
        timerInRange = 0;
        fishAttributes.takeDamage();
        animator.SetTrigger("Swing");
        StartCoroutine(PauseForTime(2.5f));
    }

    IEnumerator PauseForTime(float time)
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(time);
        agent.isStopped = false;
    }
    IEnumerator PauseChase(float time)
    {
        canChase = false;
        yield return new WaitForSeconds(time);
        canChase = true;
    }

    void MoveToRoom(Room room)
    {
        agent.speed = normalSpeed;
        Vector2 point = Random.insideUnitCircle * 5;
        Vector3 worldPoint = room.transform.position + new Vector3(point.x, point.y, 0);

        agent.SetDestination(worldPoint);
        currentroom = room;
    }

    [Button]
    public void test()
    {
        TryAddFunny(25);
    }

    public void TryAddFunny(float f)
    {
        if (currentState == STATES.Dead)
        {
            return;
        }
        if (f > 1)
        {
            LaughParticle.Play();
            source.PlayOneShot(laughs[Random.Range(0, laughs.Length)]);
            StartCoroutine(PauseChase(7));
            Net.SetActive(false);
        }
        currentOxygen -= f;
        currentState = STATES.idel;
        StartCoroutine(PauseForTime(2));
        
    }

    public void TrySeeFunny(float f, Vector3 position)
    {
        if ((Vector3.Distance(transform.position, position) <= fov.viewRadius) && (!Physics2D.Linecast(transform.position, position, LayerMask.GetMask("Wall"))) && (fov.CheckForObjectBetweenAngle(position)))
        {
            TryAddFunny(f);
        }
    }

    void SlowMovement()
    {
        agent.speed = calmSpeed;
        Vector2 point = Random.insideUnitCircle * 5;
        Vector3 worldPoint = currentroom.transform.position + new Vector3(point.x, point.y, 0);

        agent.SetDestination(worldPoint);
    }

    void MakeADescicion(int choice)
    {
        switch (choice)
        {
            case < 50:
                //move in current room
                SlowMovement();
                break;
            case < 70:
                //move to a side room
                if (currentroom.sideRooms.Count > 0)
                {
                    MoveToRoom(currentroom.sideRooms[Random.Range(0, currentroom.sideRooms.Count - 1)]);
                }

                break;
            case < 90:
                //move down
                if (currentroom.bottomRooms.Count > 0)
                {
                    MoveToRoom(currentroom.bottomRooms[Random.Range(0, currentroom.bottomRooms.Count - 1)]);
                }
                break;
            default:
                //move up
                if (currentroom.topRooms.Count > 0)
                {
                    MoveToRoom(currentroom.topRooms[Random.Range(0, currentroom.topRooms.Count - 1)]);
                }
                break;

        }
    }


    private void SetEnemyModel(int i)
    {
        foreach (GameObject g in EnemyModels)
        {
            g.SetActive(false);
        }
        EnemyModels[i].SetActive(true);
        Net.transform.parent = HandBones[i].transform;
        currentModel = i;
    }


}

public enum STATES
{
    idel,
    move,
    moveToRoom,
    Chase,
    Dead
}
