using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[RequireComponent(typeof(Renderer))]
public class Waypoint : MonoBehaviour
{

    private Vector3 position;
    private float speed;
    private bool waitHere = false;
    private float waitTime;
    private bool next = false;

    private Renderer rend;

    public Vector3 Position
    {
        get
        {
            return position;
        }

        set
        {
            position = value;
        }
    }
    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }
    public bool WaitHere
    {
        get
        {
            return waitHere;
        }

        set
        {
            waitHere = value;
        }
    }
    public float WaitTime
    {
        get
        {
            return waitTime;
        }

        set
        {
            waitTime = value;
        }
    }
    public bool Next
    {
        get
        {
            return next;
        }

        set
        {
            next = value;
            rend = GetComponent<Renderer>();
            if (next)
            {   
                rend.material.SetColor("_Color", Color.red);
            }
            else {
                rend.material.SetColor("_Color", Color.white);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //waypoint reached
        RailsLogic railsLogic = other.GetComponent<RailsLogic>();
        railsLogic.incrementCounter();
    }
}
