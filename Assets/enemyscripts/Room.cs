using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Room> topRooms;

    public List<Room> bottomRooms;

    public List<Room> sideRooms;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 1);
        Gizmos.color = Color.green;
        foreach (Room r in topRooms)
        {
            DrawArrow.ForGizmo(transform.position, Direction(transform.position, r.transform.position)*2);
        }
        foreach (Room r in bottomRooms)
        {
            DrawArrow.ForGizmo(transform.position, Direction(transform.position, r.transform.position)*2);
        }
        foreach (Room r in sideRooms)
        {
            DrawArrow.ForGizmo(transform.position, Direction(transform.position, r.transform.position)*2);
        }
    }
    Vector3 Direction(Vector3 from, Vector3 to)
    {
        return (to - from).normalized;
    }
}
