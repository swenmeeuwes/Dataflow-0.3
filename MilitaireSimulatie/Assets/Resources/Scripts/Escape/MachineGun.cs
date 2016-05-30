using UnityEngine;
using System.Collections;

public class MachineGun : MonoBehaviour  
{

    public Transform tracerPrefab;
    public Transform target;
    public float shootingSpeed = .08f;
    public LayerMask rayMask;

    private AudioSource audioSource;
    private AudioClip burstSound;



    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        burstSound = Resources.Load("Sounds/M249_burst") as AudioClip;
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
        for (int i = 0; i < 6; i++) {
            RaycastHit hitInfo;
            Vector3 barrelEndPosition = transform.localPosition;
            barrelEndPosition.z -= 2.5f;

            Vector3 shootDirection = -(transform.position - target.position).normalized;
            Vector3 hitOffset = new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f));

            Physics.Raycast(barrelEndPosition, shootDirection + hitOffset, out hitInfo, 800, rayMask);

            drawTracer(barrelEndPosition, shootDirection + hitOffset);

            //Debug.DrawRay(barrelEndPosition, (shootDirection + hitOffset) * 1000, Color.red);
            
            yield return new WaitForSeconds(shootingSpeed);
        }
        audioSource.PlayOneShot(burstSound);
        yield return new WaitForSeconds(2.5f);
        StartCoroutine("shoot");
    }

    private void drawTracer(Vector3 barrelEndPosition, Vector3 shootDirection)
    {
            

        Quaternion qShootDirection = Quaternion.LookRotation(shootDirection, Vector3.forward);
        qShootDirection *= Quaternion.Euler(90, 0, 0);
        Transform tracer = (Transform)Instantiate(tracerPrefab, barrelEndPosition, qShootDirection);


    }
}
