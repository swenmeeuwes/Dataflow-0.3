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

    public bool toggleThirdPerson = false;
    public LayerMask withoutPlayer;
    public LayerMask withPlayer;

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

        if (Input.GetKeyUp(KeyCode.KeypadEnter)) {
            toggleThirdPerson = !toggleThirdPerson;
            if (toggleThirdPerson)
            {
                Camera.main.cullingMask = withPlayer;
                Camera.main.transform.localPosition = new Vector3(0, 2.5f, -1.6f);
                Camera.main.transform.localRotation = Quaternion.Euler(new Vector3(17.30f, 0, 0));
            }
            else {
                Camera.main.cullingMask = withoutPlayer;
                Camera.main.transform.localPosition = new Vector3(0, 1.68f, 0);
                Camera.main.transform.localRotation = Quaternion.Euler(new Vector3(5f, 0, 0));
            }
        }
    }
}
