using UnityEngine;
using System.Collections;

public class Enemy : Entity
{
    public float speed;

    [SerializeField]
    Vector3 finishPoint;
    [SerializeField]
    float allowedDistance;

    Vector3 direction;

    public void MoveTo(Vector3 finishPoint)
    {
        direction = (finishPoint - transform.position).normalized * speed;

        StartCoroutine(SmoothMovement(finishPoint));
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        MoveTo(finishPoint);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    IEnumerator SmoothMovement(Vector3 destination)
    {
        var remainingDistance = (destination - transform.position).magnitude;
        while (remainingDistance > allowedDistance)
        {
            rbody.velocity = new Vector3(direction.x * Time.deltaTime, rbody.velocity.y, direction.z * Time.deltaTime);
            remainingDistance = (destination - transform.position).magnitude;
            yield return null;
        }
    }
}