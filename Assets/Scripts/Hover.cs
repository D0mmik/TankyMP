using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    private Rigidbody rb;
    public float multiplier;
    public float moveForce;
    public float turnTorque;
    public Transform[] forcePoints = new Transform[4];
    private RaycastHit[] hits = new RaycastHit[4];
    private float vertical;
    private float horizontal;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        for(int i = 0; i < 4; i++)
        {
            ApplyForce(forcePoints[i], hits[i]);
        }

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        rb.AddForce(vertical * moveForce * transform.forward);
        rb.AddTorque(horizontal * turnTorque * transform.up);


    }
    void ApplyForce(Transform forcePoint, RaycastHit hit)
    {
        if(Physics.Raycast(forcePoint.position, -forcePoint.up, out hit))
        {
            float force = 0;
            force = Mathf.Abs(1 /(hit.point.y - transform.position.y));
            rb.AddForceAtPosition(transform.up * force * 2.5f, forcePoint.position, ForceMode.Acceleration);
        }

    }
}
