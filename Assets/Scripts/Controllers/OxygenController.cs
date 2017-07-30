using UnityEngine;
using UnityEngine.SceneManagement;

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
            if (_oxygen == 0) {
                Scenes.message = "You ran out of oxygen :(";
                SceneManager.LoadScene("MenuScene");
                return;
            }
        }
    }

    public bool OxygenGeneratorOn { get; set; }

    public ProgressBar oxygenProgressBar;

    private void Start() {
        if (Instance != null) {
            Debug.LogError("Tried to create multiple OxygenControllers!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Oxygen = maxOxygen;
        OxygenGeneratorOn = true;
    }

    private void Update() {
        if (!ObjectiveController.Instance.HasBeenInController) return;
        if (OxygenGeneratorOn && ElectricityController.Instance.Electricity > 0) {
            Oxygen += maxOxygen / 10 * Time.deltaTime;
        }
        Oxygen -= oxygenConsumption * Time.deltaTime;
    }

    public bool ToggleOxygenGenerator() {
        OxygenGeneratorOn = !OxygenGeneratorOn;
        return OxygenGeneratorOn;
    }
}
