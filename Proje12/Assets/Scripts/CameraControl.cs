using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float cameraSpeed;

    public float minXRot;
    public float maxXRot;

    private float curXRot;

    public float minZoom;
    public float maxZoom;

    public float zoomSpeed;
    public float rotateSpeed;

    private float curZoom;

    private Camera camera1;
    void Start()
    {
        camera1 = Camera.main;
        curZoom = camera1.transform.localPosition.y;
        curXRot = -50;
    }
    void Update()
    {
        curZoom += Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed;
        curZoom = Mathf.Clamp(curZoom, minZoom, maxZoom);

        camera1.transform.localPosition = Vector3.up * curZoom;

        if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            curXRot += -y * rotateSpeed;
            curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);
            transform.eulerAngles = new Vector3(curXRot, transform.eulerAngles.y + (x * rotateSpeed), 0.0f);

            Vector3 forward = camera1.transform.forward;
            forward.y = 0.0f;
            forward.Normalize();
            Vector3 right = camera1.transform.right;
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");

            Vector3 direction = forward * moveZ + right * moveX;

            direction.Normalize();
            direction *= cameraSpeed * Time.deltaTime;
            transform.position += direction;
        }
    }
}
