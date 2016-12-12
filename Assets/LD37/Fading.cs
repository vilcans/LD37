using UnityEngine;

public class Fading : MonoBehaviour {
    public float targetAlpha = 0;

    public Material material;

    void Awake() {
        material.color = Color.black;
    }

    void Update() {
        Color color = material.color;
        color.a = Mathf.MoveTowards(color.a, targetAlpha, Time.deltaTime);
        material.color = color;
    }
}
