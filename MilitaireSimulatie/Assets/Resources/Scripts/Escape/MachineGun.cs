using UnityEngine;
using System.Collections;

public class MachineGun : MonoBehaviour
{

    public Transform tracerPrefab;
    public Transform target;
    public float shootingSpeed = .08f;
    public LayerMask rayMask;
    


    // Use this for initialization
    void Start()
    {
        Debug.Log("test");
        StartCoroutine("shoot");
    }

    public void startShooting()
    {
        StartCoroutine("shoot");
    }

    public void stopShooting()
    {
        StopCoroutine("shoot");
    }

    IEnumerator shoot()
    {
        RaycastHit hitInfo;

        Vector3 barrelEndPosition = transform.localPosition;
        barrelEndPosition.z -= 2.5f;

        Vector3 shootDirection = -(transform.position - target.position).normalized;
        Vector3 hitOffset = new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f));

        Physics.Raycast(barrelEndPosition, shootDirection, out hitInfo, 800, rayMask);
        drawTracer(barrelEndPosition, shootDirection);

        Debug.DrawRay(barrelEndPosition, (shootDirection + hitOffset) * 1000, Color.red);
        Debug.Log(hitInfo.point);

        yield return new WaitForSeconds(shootingSpeed);
        StartCoroutine("shoot");
    }

    private void drawTracer(Vector3 barrelEndPosition, Vector3 shootDirection) {
        //Instantiate(tracerPrefab, barrelEndPosition, ));

    }
}
