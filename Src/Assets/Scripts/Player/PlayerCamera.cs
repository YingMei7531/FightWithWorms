using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Vector3 angles;
    public float angleSpeed;
    public float speed;
    float lastTime;

    void Awake()
    {
        lastTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        int direction = 0;
        if (Input.GetKey(KeyCode.E)) direction = 1;
        else if (Input.GetKey(KeyCode.Q)) direction = -1;
        angles.y += angleSpeed * direction * (Time.realtimeSinceStartup - lastTime);
        transform.rotation = Quaternion.Euler(angles);
        Vector3 input = Vector3.zero;
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");
        input = Vector3.ClampMagnitude(input, 1f);
        Vector3 lookForward = transform.forward;
        lookForward.y = 0;
        transform.position += Quaternion.LookRotation(lookForward) * input * speed * (Time.realtimeSinceStartup - lastTime);
        lastTime = Time.realtimeSinceStartup;
    }
}
