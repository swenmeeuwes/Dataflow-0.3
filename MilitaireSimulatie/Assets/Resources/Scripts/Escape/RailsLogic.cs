using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(GuyBehaviour))]
public class RailsLogic : MonoBehaviour
{
    
    public List<GameObject> waypoints;
    public bool enableDebug = false;

    private GuyBehaviour guyBehavior;
    private int counter = 0;

    [SerializeField]
    private GameObject saveUI, safetyUI;

    private MachineGun machineGun;
    private Enemy unluckyTeammate;
    private GameObject teammate;

    // Use this for initialization
    void Start()
    {

        guyBehavior = GetComponent<GuyBehaviour>();
        Waypoint startWaypoint = waypoints[0].GetComponent<Waypoint>();
        startWaypoint.Next = true;
        GameObject m249 = GameObject.Find("M249");
        machineGun = m249.GetComponent<MachineGun>();
        teammate = GameObject.Find("Teammate 3");
        unluckyTeammate = teammate.GetComponent<Enemy>();

        //saveUI = GameObject.Find("SaveUI");
        //safetyUI = GameObject.Find("SafetyUI");
        safetyUI.SetActive(false);
        saveUI.SetActive(false);

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

    private void logic()
    {
        if (counter < waypoints.Count)
        {
            guyBehavior.rotateTowards(waypoints[counter].transform.position);
            guyBehavior.walk();
        }
        else
        {
            Debug.Log("done?");
            guyBehavior.stopWalking();
        }
    }

    private void toggleActive(bool setActive)
    {
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            Renderer rend = waypoints[i].GetComponent<Renderer>();
            rend.enabled = setActive;
        }
    }

    private void debugPath()
    {
        for (int i = 1; i < waypoints.Count; i++)
        {
            Debug.DrawLine(waypoints[i - 1].transform.position, waypoints[i].transform.position, Color.red);
        }
    }

    public void incrementCounter()
    {
        onWaypointEnter(counter);
        counter++;
        if (counter < waypoints.Count)
        {
            Waypoint nextWaypoint = waypoints[counter].GetComponent<Waypoint>();
            nextWaypoint.Next = true;
            Waypoint previousWaypoint = waypoints[counter - 1].GetComponent<Waypoint>();
            previousWaypoint.Next = false;
        }


    }

    public void onWaypointEnter(int count)
    {
        switch (count)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                machineGun.startShooting();
                break;
            case 3:
                unluckyTeammate.setWounded(true);
                break;
            case 4:
                safetyUI.SetActive(true);
                saveUI.SetActive(true);
                //safeTeammate();
                break;
            case 5:
                break;
            case 6:
                machineGun.stopShooting();
                break;

        }
    }

    public void safeTeammate() {
        teammate.AddComponent<Waypoint>();
        waypoints.Insert(counter + 1, teammate);

        safetyUI.SetActive(false);
        saveUI.SetActive(false);
    }

    public void safeYourself()
    {
        safetyUI.SetActive(false);
        saveUI.SetActive(false);
    }
}
