using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Vector3 offset = new Vector3(0, 0, -5f);
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    public Transform cam;

    // Update is called once per frame
    void Update()
    {
        Vector3 target = cam.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
    }
}
