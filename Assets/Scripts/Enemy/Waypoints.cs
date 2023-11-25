using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Waypoints main;

    public Transform startWaypoint;
    public Transform[] waypoints;

    private void Awake()
    {
        main = this;
    }
}
