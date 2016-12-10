using System;
using UnityEngine;

public class SoundTrigger : MonoBehaviour {

    public AudioClip clip;

    [NonSerialized]
    public bool played = false;
}
