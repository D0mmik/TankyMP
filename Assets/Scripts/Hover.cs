using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public Transform[] forcePoints = new Transform[4];
    private RaycastHit[] hits = new RaycastHit[4];
    private float vertical;
    private float horizontal;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void SetRb()
    {
        rb.mass = 100;
        rb.drag = 2;
        rb.angularDrag = 2.1f;
        rb.useGravity = true;
    }
    void FixedUpdate()
    {
        for(int i = 0; i < 4; i++)
        {
            ApplyForce(forcePoints[i], hits[i]);
        }

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        rb.AddForce(vertical * speed* transform.forward);
    }
    void Update()
    {
        transform.Rotate(Vector3.up * 100 * horizontal * Time.deltaTime);
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
