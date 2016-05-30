using UnityEngine;
using System.Collections;

public class TracerMovement : MonoBehaviour {

    public float bulletSpeed;

	void Update () {
        transform.Translate(Vector3.up * Time.deltaTime * bulletSpeed);
        Destroy(gameObject, 2);
	}
}
