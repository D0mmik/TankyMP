using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walk : MonoBehaviour
{
    private float vertical;
    private float horizontal;
    private Rigidbody rb;
    private Vector3 velocity;
    public float gravity = -9.81f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        rb.AddForce(move * 100);

        
        
    }
}
