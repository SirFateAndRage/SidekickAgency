using UnityEngine;
using System.Collections;
using System.Linq;

public class WaypointAgent : MonoBehaviour {

    public WaypointSystem waySystem;
    public int wayIndex;
    public float speed;
    public int wayDirection;
    public bool isMoving;

    float _distanceToNextNode;
    float _goalDistanceToNextNode;

    public enum WayType
    {
        Single,
        PingPong,
        Loop
    }
    public WayType currentWayType = WayType.Loop;

	// Use this for initialization
	void Start ()
    {
        isMoving = true;

        int nearestNode = 0;
        for (int i = 0; i < waySystem.allNodes.Length; i++)
        {
            if(Vector3.Distance(transform.position, waySystem.allNodes[i].position) < 
               Vector3.Distance(transform.position, waySystem.allNodes[nearestNode].position))
            {
                nearestNode = i;
            }
        }

        wayIndex = nearestNode;
	}

	// Update is called once per frame
	void Update ()
    {
        if(isMoving)
        {
            Vector3 distance = waySystem.allNodes[wayIndex].position - this.transform.position;
            //Si la distancia es mayor al módulo de movimiento (o sea a "cuánto se mueve por frame").
            if (distance.magnitude > speed * Time.deltaTime)
            {
                this.transform.position += distance.normalized * speed * Time.deltaTime;
                _distanceToNextNode += speed * Time.deltaTime;
                this.transform.forward = Vector3.Lerp(transform.forward, distance.normalized, _distanceToNextNode / _goalDistanceToNextNode);
            }
            else
            {
                this.transform.position = waySystem.allNodes[wayIndex].position;
                wayIndex += wayDirection;

                if (wayIndex >= waySystem.allNodes.Length || wayIndex < 0)
                {
                    if (currentWayType == WayType.PingPong)
                    {
                        wayDirection *= -1;
                        wayIndex += wayDirection;
                    }
                    else if (currentWayType == WayType.Loop)
                    {
                        if (wayDirection > 0)
                        {
                            wayIndex = 0;
                        }
                        else
                        {
                            wayIndex = waySystem.allNodes.Length - 1;
                        }
                    }
                    else if (currentWayType == WayType.Single)
                    {
                        isMoving = false;
                    }
                }

                _goalDistanceToNextNode = Vector3.Distance(transform.position, waySystem.allNodes[wayIndex].position);
                _distanceToNextNode = 0;
            }
        }
	}
}
