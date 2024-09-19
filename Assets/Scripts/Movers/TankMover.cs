using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : Mover
{
    // Variable for our Rigidbody
    private Rigidbody rb;

    // Variable for our Transform
    private Transform tf;
    
    // Start is called before the first frame update
    public override void Start()
    {
        // Initialize rb
        rb = GetComponent<Rigidbody>();
        // Initialize tf
        tf = transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Move(Vector3 direction, float speed)
    {
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;

        rb.MovePosition(rb.position + moveVector);
    }

    public void Rotate(float turnSpeed)
    {
        tf.Rotate(0, turnSpeed * Time.deltaTime, 0);
    }
}
