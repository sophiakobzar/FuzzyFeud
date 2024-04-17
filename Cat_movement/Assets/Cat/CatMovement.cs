using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Third_person_cat_move : MonoBehaviour
{
    public CharacterController controller;
<<<<<<< HEAD
    public float speed = 8f;
=======
    public float speed = 6f;
>>>>>>> 15700dc88321bbaafd96410bad25edef16d12830

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // left and right arrows
        float horizontal = Input.GetAxisRaw("Horizontal");
        // up and down arrows
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(vertical, 0f, horizontal).normalized;

        if(direction.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            controller.Move(direction * speed * Time.deltaTime);

        }
    }
}
