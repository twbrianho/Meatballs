using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody m_Rigidbody;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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
