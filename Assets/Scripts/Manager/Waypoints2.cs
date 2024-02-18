using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints2 : MonoBehaviour
{
    public static Waypoints2 main;

    public Transform startWaypoint;
    public Transform[] waypoints;

    private void Awake()
    {
        main = this;
    }
}
