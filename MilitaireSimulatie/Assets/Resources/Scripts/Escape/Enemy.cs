using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour {
    public GameObject target;
    public float distanceFromTarget = float.Epsilon;
    public float speed = 1;
    public float turnSpeed = 30;

    Rigidbody rbody;
    CapsuleCollider capsuleCollider;
    Animator animator;
	// Use this for initialization
	void Start () { 
        rbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();

        if (target != null)
            StartCoroutine(MoveToTarget());
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator MoveToTarget()
    {
        var remainingDistance = (target.transform.position - transform.position).magnitude;
        while(remainingDistance > distanceFromTarget)
        {
            var speedDelta = speed * Time.deltaTime;
            var turnSpeedDelta = turnSpeed * Time.deltaTime;

            var newPosition = transform.position + transform.forward * speedDelta;
            var newRotation = Vector3.RotateTowards(transform.forward, target.transform.position - transform.position, turnSpeedDelta, 0.0f);

            animator.SetFloat("vSpeed", 1); // Should be speedDelta but this is not working?
            animator.SetFloat("hSpeed", 0); //turnSpeedDelta

            rbody.MoveRotation(Quaternion.LookRotation(newRotation));
            rbody.MovePosition(newPosition);

            remainingDistance = (target.transform.position - transform.position).magnitude;

            yield return null;
        }

        animator.SetFloat("vSpeed", 0);
        animator.SetFloat("hSpeed", 0);
    }
}
