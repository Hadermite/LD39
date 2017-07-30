using UnityEngine;

public class OxygenController : MonoBehaviour {

    public static OxygenController Instance { get; private set; }

    public float maxOxygen;
    public float oxygenConsumption;

    private float _oxygen;
	public float Oxygen {
        get {
            return _oxygen;
        }
        set {
            _oxygen = Mathf.Clamp(value, 0, maxOxygen);
            oxygenProgressBar.Progress = _oxygen / maxOxygen;
        }
    }

    public ProgressBar oxygenProgressBar;

    private void Start() {
        if (Instance != null) {
            Debug.LogError("Tried to create multiple OxygenControllers!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Oxygen = maxOxygen;
    }

    private void Update() {
        Oxygen -= oxygenConsumption * Time.deltaTime;
    }
}
