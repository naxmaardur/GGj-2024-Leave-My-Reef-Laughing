using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private Room currentroom;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float calmSpeed = 0.01f;

    private float currentOxygen = 100f;
    private float maxOxygen = 100f;

    private float timerChoice = 0f;

    private float targetTimeChoice = 1f;

    private bool chasing = false;

    private bool moving = false;


    void Start()
    {
        
    }

    void Update()
    {
        print(Vector3.Angle(transform.position, new Vector3(0,0,0)));
        if (currentOxygen <= 0)
        {
            //go to the upmost top
        }
        
        //condition to start chasing

        if (!chasing)
        {
            timerChoice += Time.deltaTime;

            if (moving)
                Movement();

            if (timerChoice > targetTimeChoice)
            {
                moving = false;
                
                int choice = Random.Range(0, 100);

                if (choice > currentOxygen)
                {
                    //move a room up
                    if (currentroom.topRooms.Count > 0)
                    {
                        MoveToRoom(currentroom.topRooms[Random.Range(0, currentroom.topRooms.Count - 1)]);
                    }
                }
                else
                {
                    MakeADescicion(choice);
                }

                timerChoice = 0;
                //generate a random time 
                targetTimeChoice = Random.Range(0f, 2f);
            }
        }
        else
        {
            //go chase player
        }
    }

    void MoveToRoom(Room room)
    {
        this.transform.position = room.transform.position; //replace with better movement
        currentroom = room;
    }



    public void TryAddFunny(float f)
    {
        throw new System.NotImplementedException();
    }

    public void TrySeeFunny(float f, Vector3 position)
    {
        throw new System.NotImplementedException();
        if (Vector3.Angle(transform.right, position) > 10)
        {
            
        }
    }

    void Movement()
    {
        if (Vector3.Distance(transform.position, currentroom.transform.position) < radius)
        {
            transform.position += transform.up * calmSpeed;
        }
    }

    void MakeADescicion(int choice)
    {
        switch (choice)
        {
            case < 50:
                //move in current room
                transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                moving = true;
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
}
