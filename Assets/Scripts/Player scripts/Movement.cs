using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody m_Rigidbody;
    public float speed = 1f;

    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        if (Input.GetKey(KeyCode.A))
            m_Rigidbody.AddForce(-speed, 0, 0);
        if (Input.GetKey(KeyCode.D))
            m_Rigidbody.AddForce(speed, 0, 0);
        if (Input.GetKey(KeyCode.W))
            m_Rigidbody.AddForce(0, 0, speed);
        if (Input.GetKey(KeyCode.S))
            m_Rigidbody.AddForce(0, 0, -speed);
    }
}
