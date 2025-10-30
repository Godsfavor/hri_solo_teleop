using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System

public class BlueRovVelocityControl : MonoBehaviour
{
    
    public float lvx = 0.0f;
    public float lvy = 0.0f;
    public float lvz = 0.0f;
    public float avz = 0.0f;
    public bool movementActive = false;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    private void moveVelocityRigidbody()
    {
        Vector3 movement = new Vector3(-lvx * Time.deltaTime, lvz * Time.deltaTime, lvy * Time.deltaTime);
        transform.Translate(movement);
        transform.Rotate(0, avz * Time.deltaTime);
    }

    public void moveVelocity(RosMessagesTypes.Geometry.TwistMsg velocityMessage){

        this.lvx = (float)velocityMessage.linear.x;
        this.lvy = (float)velocityMessage.linear.y;
        this.lvz = (float)velocityMessage.linear.z;
        this.avz = (float)velocityMessage.angular.z;
        // If all velocities are zero, stop moving
        if (Mathf.Approximately(lvx, 0f) &&
            Mathf.Approximately(lvy, 0f) &&
            Mathf.Approximately(lvz, 0f) &&
            Mathf.Approximately(avz, 0f))
        {
            movementActive = false;
        }
        else
        {
            movementActive = true;
        }    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movementActive){
            moveVelocityRigidbody()
        }
    }

    
}
