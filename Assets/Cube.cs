using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public int groundDrag = 5;
    public int airDrag = 0;
    private Rigidbody rb;
    private float vertical = 0;
    private float horizontal;
    private bool isGrounded;
    public float speed = 10;

    
    void Start()
    {
        rb =  GetComponent<Rigidbody>();
    }
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.6f); 
        if(isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            transform.Translate(Vector3.down * 3 * Time.deltaTime);
            rb.drag = airDrag;
        }
        
        
        transform.Translate(Vector3.forward * speed  * vertical * Time.deltaTime);
        transform.Rotate(Vector3.up * 100 * horizontal * Time.deltaTime);
    }       
}
