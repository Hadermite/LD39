using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    private float _progress = 1f;
    public float Progress {
        get {
            return _progress;
        }
        set {
            _progress = Mathf.Clamp01(value);
            progressImage.fillAmount = _progress;
        }
    }

    private Image progressImage;

    private void Awake() {
        progressImage = transform.Find("Progress").GetComponent<Image>();
    }
}
