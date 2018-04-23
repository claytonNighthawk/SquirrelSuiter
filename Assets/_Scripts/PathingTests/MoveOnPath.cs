using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPath : MonoBehaviour {


    public EditorPath Path;
    GameObject squirrel;
    public int currentWayPoint = 0;
    public float speed;
    public float reachDist = 1.0f;
    public float rotationSpeed = 10.0f;

    private Vector3 last_pos;
    private Vector3 current_pos;


    void Start () {
        last_pos = transform.position;
        squirrel = this.transform.GetChild(0).gameObject;

    }
    
    void FixedUpdate () {
        Vector3 destination = Path.path_objs[currentWayPoint].position;

        Quaternion rotation = Quaternion.LookRotation(destination - transform.position);
        squirrel.transform.rotation = Quaternion.Slerp(squirrel.transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        float dist = Vector3.Distance(destination, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if (dist < reachDist) {
            currentWayPoint = (currentWayPoint + 1) % Path.path_objs.Count;
        }
    }
}
