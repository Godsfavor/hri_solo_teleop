using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;  // For TwistMsg

public class KeyboardCmdVelPublisher : MonoBehaviour
{
    public string topic = "/bluerov1/cmd_vel";
    public float forwardSpeed = 0.5f;  // tune to your liking
    public float yawSpeed = 0.5f;      // optional (A/D turn)

    private ROSConnection ros;

    void Start()
    {
        // Get or create the ROS connection and register as a publisher
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<TwistMsg>(topic);
    }

    void Update()
    {
        // We will send cmd_vel while keys are pressed
        var twist = new TwistMsg();
        bool send = false;

        // Exercise requirement: move when W is pressed
        if (Input.GetKey(KeyCode.W))
        {
            twist.linear.z = forwardSpeed;
            send = true;
        }

        // Extra (optional): S backwards, A/D yaw
        if (Input.GetKey(KeyCode.S))
        {
            twist.linear.z = -forwardSpeed;
            send = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            twist.angular.z = yawSpeed;
            send = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            twist.angular.z = -yawSpeed;
            send = true;
        }

        // Only publish if any key is pressed
        if (send)
        {
            ros.Publish(topic, twist);
        }
    }
}


