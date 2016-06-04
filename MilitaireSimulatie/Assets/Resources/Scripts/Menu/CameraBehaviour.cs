using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraBehaviour : MonoBehaviour {
    [SerializeField]
    private float lookSpeed = 1.0f;
    [SerializeField]
    private LayerMask buttonLayerMask;

    private Camera mainCamera;
	
	void Start () {
        mainCamera = GetComponent<Camera>();
	}
    
	void Update () {
        var mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        Quaternion quaternion = transform.rotation;
        quaternion.eulerAngles = new Vector3(quaternion.eulerAngles.x - mouseDelta.y, quaternion.eulerAngles.y + mouseDelta.x, 0);
        transform.rotation = quaternion;

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 12f, buttonLayerMask))
        {
            var lookable = hit.transform.gameObject.GetComponent<LookableButton>();
            lookable.looking = true;
        }
    }
}
