using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class GuyBehaviour : MonoBehaviour {
    public float speedModifier = 1f;
    public float turnSpeedModifier = 30f;
    public float sprintModifier = 3f;

    private Animator animator;
    private Rigidbody rigidbody;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        var vSpeed = Input.GetAxis("Vertical");
        var hSpeed = Input.GetAxis("Horizontal");

        animator.SetFloat("vSpeed", vSpeed);
        animator.SetFloat("hSpeed", hSpeed);

        float turnSpeed = hSpeed * turnSpeedModifier * Time.deltaTime;
        float speed = vSpeed * speedModifier * Time.deltaTime;

        if (speed == 0)
            turnSpeed = 0;

        if(Input.GetKey(KeyCode.LeftShift)) {
            speed *= sprintModifier;
            turnSpeed *= sprintModifier;
            animator.SetBool("Running", true);
        } else
            animator.SetBool("Running", false);

        transform.position += transform.forward * speed;
        transform.Rotate(0, turnSpeed, 0);
    }
}
