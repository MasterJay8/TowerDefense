using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    //[SerializeField] Waypoints[] Wpoints;
    Waypoints[] Wpoints = new Waypoints[2];

    int waypointIndex;
    int path;

    void Start()
    { 
        Wpoints[0] = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
        Wpoints[1] = GameObject.FindGameObjectWithTag("Waypoints2").GetComponent<Waypoints>();
    }
    public void Initialize(int path)
    {
        this.path = path;
    }

    void Update()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Ghost")) return;
        transform.position = Vector2.MoveTowards(transform.position, Wpoints[path].waypoints[waypointIndex].position, speed * Time.deltaTime);

        /*Vector3 dir = Wpoints.waypoints[waypointIndex].position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/

        if (Vector2.Distance(transform.position, Wpoints[path].waypoints[waypointIndex].position) < 0.1f)
        {
            if (waypointIndex < Wpoints[path].waypoints.Length - 1) waypointIndex++;
            else
            {
                Manager.main.TakeBaseHealth();
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
            }
        }

    }
    public float GetDistanceTraveled()
    {
        float distanceTraveled = 0f;

        for (int i = 0; i < Mathf.Min(waypointIndex, Wpoints[path].waypoints.Length - 1); i++)
        {
            distanceTraveled += Vector2.Distance(Wpoints[path].waypoints[i].position, Wpoints[path].waypoints[i + 1].position);
        }

        if (waypointIndex > 0 && waypointIndex < Wpoints[path].waypoints.Length)
        {
            distanceTraveled += Vector2.Distance(Wpoints[path].waypoints[waypointIndex - 1].position, transform.position);
        }
        else if (waypointIndex == 0)
        {
            distanceTraveled += Vector2.Distance(transform.position, Wpoints[path].waypoints[0].position);
        }

        return distanceTraveled;
    }
}
