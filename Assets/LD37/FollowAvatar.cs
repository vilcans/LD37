using UnityEngine;

public class FollowAvatar : MonoBehaviour {

    public Transform following;
    public bool follow = true;

    private const float smoothTime = 1f;

    private Vector3 delta;
    private Vector3 currentVelocity;
    private Transform myTransform;

    void Awake() {
        myTransform = transform;
        delta = following.position - myTransform.position;
    }

    void Update() {
        Vector3 wantedPosition;

        if(follow) {
            wantedPosition = following.position - delta;
        }
        else {
            wantedPosition = myTransform.position;
        }
        //Vector3 newPosition = Vector3.MoveTowards(myTransform.position, wantedPosition, moveSpeed * Time.unscaledDeltaTime);
        Vector3 newPosition = Vector3.SmoothDamp(myTransform.position, wantedPosition, ref currentVelocity, smoothTime);
        myTransform.position = newPosition;
    }
}
