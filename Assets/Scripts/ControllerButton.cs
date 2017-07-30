using UnityEngine;

public class ControllerButton : MonoBehaviour {

    private MeshRenderer indicator;

    private void Start() {
        Transform indicatorTransform = transform.Find("Indicator");
        if (indicatorTransform == null) return;
        indicator = indicatorTransform.GetComponent<MeshRenderer>();
        indicator.material.color = Color.green;
    }

    public void Click() {
        // Bad code, replace if I have time later on.
        bool on = false;
        switch (transform.name) {
            case "Inside Lights Button":
                on = LightsController.Instance.ToggleInsideLights();
                break;
            case "Outside Lights Button":
                on = LightsController.Instance.ToggleOutsideLights();
                break;
            case "Suppress Alarm Button":
                AlarmController.Instance.StopAlarm();
                break;
            case "Oxygen Generator Button":
                // TODO: Toggle oxygen generator.
                break;
            default:
                Debug.LogError("Unknown button! Add to Switch statement! (" + transform.name + ")");
                break;
        }
        if (indicator == null) return;
        if (on) indicator.material.color = Color.green;
        else indicator.material.color = Color.red;
    }
}
