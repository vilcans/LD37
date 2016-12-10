using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class Movement : MonoBehaviour {

    private const float movementForce = 5f;
    private const float stepForce = 2.5f;
    private const float jumpForce = 1f;
    private Vector2 velocity;

    private bool grounded;

    void FixedUpdate() {
        float accX = Input.GetAxis("Horizontal") * movementForce * Time.deltaTime;
        //Debug.LogFormat("accX={0}", accX);
        float accY = 0;
        if(grounded) {
            accY += Mathf.Abs(accX) * stepForce;
        }
        if(Input.GetButton("Jump") && grounded) {
            accY += jumpForce;
        }
        GetComponent<Rigidbody2D>().AddForce(new Vector2(accX, accY));
        grounded = false;
    }

    void OnCollisionStay2D(Collision2D collision) {
        Debug.LogFormat("Collided with {0}, velocity {1}", collision.gameObject, collision.relativeVelocity);
        ContactPoint2D[] contacts = collision.contacts;
        for(int i = 0; i < contacts.Length; ++i) {
            Debug.LogFormat("Contact {0} normal {1}", i, contacts[i].normal);
            if(contacts[i].normal.y > 0) {
                grounded = true;
                return;
            }
        }
    }
}
