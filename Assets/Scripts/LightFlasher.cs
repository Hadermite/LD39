using UnityEngine;

public class LightFlasher : MonoBehaviour {

    public float secondsPerFlash = 0.5f;

    private new Light light;
    private float baseIntensity;
    private bool turningOn;

    private void Awake() {
        light = GetComponent<Light>();
        baseIntensity = light.intensity;
    }

    private void Update() {
        if (!light.enabled) return;
        float amount = (baseIntensity / secondsPerFlash) * Time.deltaTime;
        light.intensity += amount * (turningOn ? 1 : -1);
        if (turningOn && light.intensity >= baseIntensity) {
            light.intensity = baseIntensity;
            turningOn = false;
        }
        if (!turningOn && light.intensity <= 0) {
            light.intensity = 0;
            turningOn = true;
        }
    }
}
