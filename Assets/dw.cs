using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class dw : MonoBehaviour
{
    private Rigidbody rb;
    private RaycastHit rH;
    [SerializeField]
    private float angSpd=5;
    [SerializeField]
    private float spd =5;
    private Vector3 finalSpd;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
 
    // Update is called once per frame
    void Update()
    {
        rotacionarCamera();
        movimentar();
    }
 
    private void rotacionarCamera()
    {
        rb.angularVelocity=new Vector3(0,Input.GetAxis("Mouse X")*angSpd,0);
    }
    private void movimentar()
    {
        if(Input.GetKey(KeyCode.W))
        {
            Physics.Raycast(transform.position,-transform.up,out rH,Mathf.Infinity);
            finalSpd=Vector3.ProjectOnPlane(transform.forward, rH.normal);
            finalSpd=Vector3.Normalize(finalSpd);
            rb.velocity = finalSpd*spd;
        }
        else
        {
            rb.velocity = new Vector3(0,0,0) ;
        }
    }
}
 