using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Third_person_cat_move : MonoBehaviour
{
    public CharacterController controller;
    // speed of the character controlled by the player
    public float speed = 8.0f;

    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        // Get input from horizontal and vertical axes
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Get the forward direction of the camera without vertical component
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        // Calculation to make the direction of player align with the direction of the camera
        Vector3 direction = (cameraForward * vertical + Camera.main.transform.right * horizontal).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Calculate the target angle for rotation based on movement direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            // Moves the player
            controller.Move(direction * speed * Time.deltaTime);
        }
    }
}
