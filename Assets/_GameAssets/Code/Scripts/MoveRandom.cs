using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandom : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float range;
    [SerializeField] float maxDistance;

    Vector2 wayPoint;

    public float Speed { get => speed; set => speed = value; }
    public float Range { get => range; set => range = value; }
    public float MaxDistance { get => maxDistance; set => maxDistance = value; }

    void Start()
    {
        SetNewDestination(); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, wayPoint) < range)
        {
            SetNewDestination();
        }
    }

    void SetNewDestination()
    {
        wayPoint = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-range, maxDistance)); 
    }
}
