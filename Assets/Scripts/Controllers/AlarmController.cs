using System.Collections;
using UnityEngine;

public class AlarmController : MonoBehaviour {

    public static AlarmController Instance { get; private set; }

    public AudioClip generatorsBreakingSound;
    public AudioClip alarmSound;
    public Transform warningPanel;

    private AudioSource source;
    private float timer;
    private bool warningLightsStarted;

    private void Start() {
        if (Instance != null) {
            Debug.LogError("Tried to create multiple AlarmControllers!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        source = gameObject.AddComponent<AudioSource>();
        StartCoroutine("StartBreakingSound");
    }

    public void StopAlarm() {
        StopCoroutine("StartBreakingSound");

        LightsController.Instance.ToggleWarningLights(false);
        warningPanel.gameObject.SetActive(false);
    }

    IEnumerator StartBreakingSound() {
        yield return new WaitForSeconds(2f);
        source.PlayOneShot(generatorsBreakingSound, 0.5f);

        while (true) {
            if (!source.isPlaying) {
                if (!warningLightsStarted) {
                    warningLightsStarted = true;
                    LightsController.Instance.DimLights(true);
                    LightsController.Instance.ToggleWarningLights(true);
                    DialogueController.Instance.StartDialogue();
                }
                timer += Time.deltaTime;
                if (timer >= 0.1f) {
                    source.PlayOneShot(alarmSound, 0.05f);
                    timer = 0;
                }
            }
            yield return null;
        }
    }
}
