using UnityEngine;

public class FollowAvatar : MonoBehaviour {

    public Transform following;
    public bool follow = true;

    private const float smoothTime = 1f;

    private Vector3 delta;
    private Vector3 currentVelocity;

    private Vector3 currentLookVelocity;
    private Vector3 lookAtPosition;
    private Transform myTransform;

    void Awake() {
        myTransform = transform;
        lookAtPosition = following.position;
        delta = following.position - myTransform.position;
    }

    void Update() {
        Vector3 wantedPosition;
        Vector3 wantedLookAt;

        if(follow) {
            wantedPosition = following.position - delta;
            wantedPosition.x *= .8f;  // make him a bit lazy about moving
            wantedLookAt = following.position;
        }
        else {
            wantedPosition = myTransform.position;
            wantedLookAt = myTransform.position + delta;
        }
        //Vector3 newPosition = Vector3.MoveTowards(myTransform.position, wantedPosition, moveSpeed * Time.unscaledDeltaTime);
        Vector3 newPosition = Vector3.SmoothDamp(myTransform.position, wantedPosition, ref currentVelocity, smoothTime);
        lookAtPosition = Vector3.SmoothDamp(lookAtPosition, wantedLookAt, ref currentLookVelocity, smoothTime);
        myTransform.position = newPosition;
        myTransform.LookAt(lookAtPosition);
    }
}
