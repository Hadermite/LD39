using UnityEngine;
using UnityEngine.UI;

public class TextFlasher : MonoBehaviour {

    public float secondsPerFlash = 0.5f;

    private Text text;
    private float timer;

    private void Start() {
        text = GetComponent<Text>();
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer >= secondsPerFlash) {
            timer = 0;
            text.enabled = !text.enabled;
        }
    }
}
