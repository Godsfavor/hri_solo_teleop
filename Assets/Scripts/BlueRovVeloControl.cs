using UnityEngine;
using System;

public class BlueRovVeloControl : MonoBehaviour {
    public float lvx = 0.0f; 
    public float lvy = 0.0f;
    public float lvz = 0.0f;
    public float avz = 0.0f;
    public bool movementActive = false;
    public Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        this.rb = GetComponent<Rigidbody>();
    }
    
    private void moveVelocityRigidbody() {
        Vector3 movement = new Vector3(-lvx * Time.deltaTime, lvz * Time.deltaTime, lvy * Time.deltaTime);
        transform.Translate(movement);
        transform.Rotate(0, avz * Time.deltaTime,0);
    }
    
    public void moveVelocity(RosMessageTypes.Geometry.TwistMsg velocityMessage) {
        this.lvx = (float)velocityMessage.linear.x;
        this.lvy = (float)velocityMessage.linear.y;
        this.lvz = (float)velocityMessage.linear.z;
        this.avz = (float)velocityMessage.angular.z;
        this.movementActive = true;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (movementActive) {
            moveVelocityRigidbody();
            this.movementActive = false;
        }
    }
}
