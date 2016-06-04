using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Renderer))]
public class Waypoint : MonoBehaviour
{

    private Vector3 position { get; set; }
    private float speed { get; set; }
    private bool waitHere { get; set; }
    private float waitTime { get; set; }
    private bool next = false;

    private Renderer rend;

    void Start()
    {
        waitHere = false;
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
        if (other.gameObject.tag == "Player")
        {
            //waypoint reached
            RailsLogic railsLogic = other.GetComponent<RailsLogic>();
            railsLogic.incrementCounter();
        }
    }
}
