using UnityEngine;

public class FollowAvatar : MonoBehaviour {

    public Transform following;
    private const float smoothTime = 1f;

    private Vector3 delta;
    private Vector3 currentVelocity;
    private Transform myTransform;

    void Awake() {
        myTransform = transform;
        delta = following.position - myTransform.position;
    }

    void Update() {
        Vector3 wantedPosition = following.position - delta;
        //Vector3 newPosition = Vector3.MoveTowards(myTransform.position, wantedPosition, moveSpeed * Time.unscaledDeltaTime);
        Vector3 newPosition = Vector3.SmoothDamp(myTransform.position, wantedPosition, ref currentVelocity, smoothTime);
        myTransform.position = newPosition;
    }
}
