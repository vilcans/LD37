using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class SoundTrigger : MonoBehaviour {

    public AudioClip clip;
    public bool final;

    [NonSerialized]
    public bool played = false;

#if UNITY_EDITOR
    void OnDrawGizmos() {
        if(clip == null) {
            return;
        }
        float length = clip.length;
        Gizmos.color = new Color(.8f, .0f, .1f, .3f);
        Gizmos.DrawLine(transform.position - Vector3.right * length, transform.position + Vector3.right * length);
        Handles.Label(transform.position, clip.name);
    }
#endif
}
