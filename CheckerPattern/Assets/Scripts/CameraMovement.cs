using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 1.0f;
    private Vector3 lastMousePosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            transform.position += new Vector3(-delta.x, 0, -delta.y) * speed * Time.deltaTime;
            lastMousePosition = Input.mousePosition;
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.position += new Vector3(0, -scroll, 0) * speed*50 * Time.deltaTime;
    }
}