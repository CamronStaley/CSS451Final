using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirection : MonoBehaviour
{

    public Transform cameraAxis;
    public Transform theWorld;
    public Transform ball;
    private bool lookAtWorld = true;
    public float moveFactor;
    // Update is called once per frame

    void Update()
    {
        LookAt(transform, cameraAxis);

        if (Input.GetKeyUp(KeyCode.F))
        {
            if (lookAtWorld)
            {
                lookAtWorld = false;
                cameraAxis = ball;
            } else
            {
                lookAtWorld = true;
                cameraAxis = theWorld;
            }
        }

        if(Input.GetKey(KeyCode.LeftAlt))
        {
            float wheelInput = Input.GetAxis("Mouse ScrollWheel");
            if (wheelInput != 0)
            {
                Vector3 dir = cameraAxis.localPosition - transform.localPosition;
                float distance = dir.magnitude;
                float move = (wheelInput * distance) / moveFactor;
                transform.localPosition += (move * dir.normalized);              
            }

            if (Input.GetMouseButton(0)) //orbit
            {
                float deltax = Input.GetAxis("Mouse X") * (moveFactor / 2);
                float deltay = Input.GetAxis("Mouse Y") * (-moveFactor / 2);
                Tumble(deltax, transform.up); //left and right
                Tumble(deltay, transform.right); // up and down

            }
        }
    }

    void Tumble(float delta, Vector3 axis)
    {
        // 1. Rotation of the viewing direction by right axis
        Quaternion q = Quaternion.AngleAxis(delta, axis);

        // 2. we need to rotate the camera position
        Matrix4x4 r = Matrix4x4.TRS(Vector3.zero, q, Vector3.one);
        Matrix4x4 invP = Matrix4x4.TRS(-cameraAxis.localPosition, Quaternion.identity, Vector3.one);
        r = invP.inverse * r * invP;
        Vector3 newCameraPos = r.MultiplyPoint(transform.localPosition);

        transform.localPosition = newCameraPos;
        LookAt(transform, cameraAxis);
    }

    void LookAt(Transform curr, Transform target)
    {
        Vector3 V = target.position - curr.position; //forward vector
        curr.up = Vector3.up;
        curr.forward = V;
    }

    void SetCameraRotation()
    {
        Vector3 V = cameraAxis.localPosition - transform.localPosition;
        Vector3 W = Vector3.Cross(-V, transform.up);
        Vector3 U = Vector3.Cross(W, -V);

        transform.forward = V;
        transform.up = U;
    }
}
