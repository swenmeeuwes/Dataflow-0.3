using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public abstract class Entity : MonoBehaviour {
    protected Rigidbody rbody;
    protected CapsuleCollider capsuleCollider;

    protected int health = 100;

    protected virtual void Start()
    {
        rbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    protected virtual void Update() {
        // if hit remove health
    }
    protected virtual void OnValidate() { }
}
