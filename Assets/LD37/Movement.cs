using UnityEngine;

public class Movement : MonoBehaviour {

    private const float movementForce = 5f / 2;
    private const float stepForce = 2.5f;
    private const float jumpForce = 3f / 2;

    private float timeSinceJumpRequest = 0;
    private float timeSinceJump = 0;
    private float timeSinceGround;
    private bool grounded;

    void FixedUpdate() {
        if(grounded) {
            timeSinceGround = 0;
        }
        else {
            timeSinceGround += Time.deltaTime;
        }
        if(Input.GetButtonDown("Jump")) {
            timeSinceJumpRequest = 0;
        }
        else {
            timeSinceJumpRequest += Time.deltaTime;
        }

        float accX = Input.GetAxis("Horizontal") * movementForce * Time.deltaTime;
        //Debug.LogFormat("accX={0}", accX);
        float accY = 0;
        if(grounded) {
            accY += Mathf.Abs(accX) * stepForce;
        }

        if(timeSinceJumpRequest < .20f && timeSinceGround < .04f && timeSinceJump >= .2f) {
            timeSinceJump = 0;
            accY += jumpForce;
        }
        else {
            timeSinceJump += Time.deltaTime;
        }
        GetComponent<Rigidbody2D>().AddForce(new Vector2(accX, accY));
        grounded = false;
    }

    void OnCollisionStay2D(Collision2D collision) {
        //Debug.LogFormat("Collided with {0}, velocity {1}", collision.gameObject, collision.relativeVelocity);
        ContactPoint2D[] contacts = collision.contacts;
        for(int i = 0; i < contacts.Length; ++i) {
            //Debug.LogFormat("Contact {0} normal {1}", i, contacts[i].normal);
            if(contacts[i].normal.y > 0) {
                grounded = true;
                return;
            }
        }
    }
}
