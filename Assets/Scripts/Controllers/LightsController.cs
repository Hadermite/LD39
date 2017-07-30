using UnityEngine;

public class LightsController : MonoBehaviour {

    public static LightsController Instance { get; private set; }

    public float insideConsumption, outsideConsumption, warningConsumption;

    private Light[] insideLights, outsideLights, warningLights;
    private bool insideLightsOn, outsideLightsOn, warningLightsOn;

    private void Start() {
        if (Instance != null) {
            Debug.LogError("Tried to create multiple LightsControllers!");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        AddLights();

        insideLightsOn = true;
        outsideLightsOn = true;
        ToggleWarningLights(false);
    }

    private void Update() {
        ElectricityController ec = ElectricityController.Instance;
        if (insideLightsOn) ec.Electricity -= insideConsumption * Time.deltaTime;
        if (outsideLightsOn) ec.Electricity -= outsideConsumption * Time.deltaTime;
        if (warningLightsOn) ec.Electricity -= warningConsumption * Time.deltaTime;
    }

    private void AddLights() {
        Transform insideLightsTransform = GameObject.Find("Inside Lights").transform;
        Transform outsideLightsTransform = GameObject.Find("Outside Lights").transform;
        Transform warningLightsTransform = GameObject.Find("Warning Lights").transform;

        insideLights = new Light[insideLightsTransform.childCount];
        outsideLights = new Light[outsideLightsTransform.childCount];
        warningLights = new Light[warningLightsTransform.childCount];

        for (int i = 0; i < insideLightsTransform.childCount; i++) {
            insideLights[i] = insideLightsTransform.GetChild(i).GetComponent<Light>();
        }

        for (int i = 0; i < outsideLightsTransform.childCount; i++) {
            outsideLights[i] = outsideLightsTransform.GetChild(i).GetComponent<Light>();
        }

        for (int i = 0; i < warningLightsTransform.childCount; i++) {
            warningLights[i] = warningLightsTransform.GetChild(i).GetComponent<Light>();
        }
    }

    public bool ToggleInsideLights() {
        ToggleInsideLights(!insideLightsOn);
        return insideLightsOn;
    }

    public void ToggleInsideLights(bool on) {
        foreach (Light light in insideLights) {
            light.enabled = on;
        }
        insideLightsOn = on;
    }

    public bool ToggleOutsideLights() {
        ToggleOutsideLights(!outsideLightsOn);
        return outsideLightsOn;
    }

    public void ToggleOutsideLights(bool on) {
        foreach (Light light in outsideLights) {
            light.enabled = on;
        }
        outsideLightsOn = on;
    }

    public void ToggleWarningLights(bool on) {
        foreach (Light light in warningLights) {
            light.enabled = on;
        }
        warningLightsOn = on;
    }

    public void DimLights(bool dim) {
        foreach (Light light in insideLights) {
            if (dim) {
                light.intensity = 0.5f;
            } else {
                light.intensity = 1f;
            }
        }
    }
}
