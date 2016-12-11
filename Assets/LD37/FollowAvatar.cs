using UnityEngine;

public class FollowAvatar : MonoBehaviour {

    public Transform following;
    public bool follow = true;

    private Vector3 delta;
    private Vector3 currentVelocity;

    private Vector3 currentLookVelocity;
    private Vector3 lookAtPosition;
    private Transform myTransform;

    private Transform forcedMoveTarget;

    public void MoveTo(Transform pose) {
        forcedMoveTarget = pose;
    }

    void Awake() {
        myTransform = transform;
        lookAtPosition = following.position;
        delta = following.position - myTransform.position;
    }

    void Update() {
        Vector3 wantedPosition;
        Vector3 wantedLookAt;

        float smoothTime;
        if(forcedMoveTarget != null) {
            wantedPosition = forcedMoveTarget.position;
            wantedLookAt = wantedPosition + forcedMoveTarget.forward;
            smoothTime = 12;
        }
        else if(follow) {
            wantedPosition = following.position - delta;
            wantedPosition.x *= .8f;  // make him a bit lazy about moving
            wantedLookAt = following.position;
            smoothTime = 1;
        }
        else {
            wantedPosition = myTransform.position;
            wantedLookAt = myTransform.position + delta;
            smoothTime = 1;
        }
        //Vector3 newPosition = Vector3.MoveTowards(myTransform.position, wantedPosition, moveSpeed * Time.unscaledDeltaTime);
        Vector3 newPosition = Vector3.SmoothDamp(myTransform.position, wantedPosition, ref currentVelocity, smoothTime);
        lookAtPosition = Vector3.SmoothDamp(lookAtPosition, wantedLookAt, ref currentLookVelocity, smoothTime);
        myTransform.position = newPosition;
        myTransform.LookAt(lookAtPosition);
    }
}
