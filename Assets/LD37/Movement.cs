using UnityEngine;

public class Movement : MonoBehaviour {

    public AudioSource audioSource;

    private Transform visibleBody;

    private new Rigidbody2D rigidbody;
    private Vector3 startPosition;

    private const float movementForce = 5f / 2;
    private const float stepForce = 2.5f;
    private const float jumpForce = 1.6f;

    private float timeSinceJumpRequest = 0;
    private float timeSinceJump = 0;
    private float timeSinceGround;
    private bool grounded;

    private float wantedRotation;
    private float rotationVelocity;
    private const float rotationSmoothTime = .1f;

    private const float offsetFromWall = -.2f;
    private float liftVelocity;

    void Awake() {
        startPosition = transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
        visibleBody = transform.Find("VisibleBody");
    }

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
        rigidbody.AddForce(new Vector2(accX, accY));
        grounded = false;

        float velocityX = rigidbody.velocity.x;
        if(Mathf.Abs(accX) > .02f) {
            wantedRotation = accX > 0 ? 0 : -180;
        }
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

    void OnTriggerEnter2D(Collider2D collider) {
        //Debug.Log("hit " + collider);
        SoundTrigger trigger = collider.GetComponent<SoundTrigger>();
        if(trigger == null || trigger.played) {
            return;
        }
        trigger.played = true;
        AudioClip clip = trigger.clip;
        if(clip == null) {
            Debug.LogFormat("No audio clip set on {0}", trigger);
            return;
        }
        audioSource.PlayOneShot(clip);
    }

    void Update() {
        Vector3 currentRotation = visibleBody.localEulerAngles;
        //Debug.LogFormat("currentRotation={0}", currentRotation);
        currentRotation.y = Mathf.SmoothDampAngle(currentRotation.y, wantedRotation, ref rotationVelocity, rotationSmoothTime);
        visibleBody.localEulerAngles = currentRotation;

        Vector3 newPosition = visibleBody.localPosition;
        newPosition.z = Mathf.SmoothDamp(newPosition.z, offsetFromWall - Mathf.Clamp(rigidbody.velocity.y, 0, 1) * .2f, ref liftVelocity, .2f);
        visibleBody.localPosition = newPosition;
    }
}
