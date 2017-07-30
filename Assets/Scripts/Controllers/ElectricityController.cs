using UnityEngine;

public class ElectricityController : MonoBehaviour {

    public static ElectricityController Instance { get; private set; }

    public float maxElectricity;
    public ProgressBar electricityProgressBar;

    private float _electricity;
	public float Electricity {
        get {
            return _electricity;
        }
        set {
            _electricity = Mathf.Clamp(value, 0, maxElectricity);
            electricityProgressBar.Progress = _electricity / maxElectricity;
            if (_electricity == 0) {
                LightsController.Instance.ToggleInsideLights(false);
                LightsController.Instance.ToggleOutsideLights(false);
                LightsController.Instance.ToggleWarningLights(false);
                AlarmController.Instance.StopAlarm();
            }
        }
    }
    
    private void Start() {
        if (Instance != null) {
            Debug.LogError("Tried to create multiple ElectricityControllers!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Electricity = maxElectricity;
    }
}
