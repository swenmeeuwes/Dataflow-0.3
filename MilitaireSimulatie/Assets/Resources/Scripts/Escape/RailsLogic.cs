using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;

[RequireComponent(typeof(GuyBehaviour))]
public class RailsLogic : MonoBehaviour
{

    public List<GameObject> waypoints;
    public bool enableDebug = false;

    private GuyBehaviour guyBehavior;
    private int counter = 0;

    // Use this for initialization
    void Start()
    {
        guyBehavior = GetComponent<GuyBehaviour>();
        Waypoint startWaypoint = waypoints[0].GetComponent<Waypoint>();
        startWaypoint.Active = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (enableDebug)
        {
            toggleActive(true);
            debugPath();
        }
        else {
            toggleActive(false);
        }

        logic();

    }

    private void logic() {
        guyBehavior.rotateTowards(waypoints[counter].transform.position);
        guyBehavior.walk();

    }

    private void toggleActive(bool setActive)
    {
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            waypoints[i].SetActive(setActive);
        }
    }

    private void debugPath()
    {
        for (int i = 1; i < waypoints.Count; i++)
        {
            Debug.DrawLine(waypoints[i - 1].transform.position, waypoints[i].transform.position, Color.red);
        }
    }

    public void incrementCounter() {
        counter++;
    }
}
