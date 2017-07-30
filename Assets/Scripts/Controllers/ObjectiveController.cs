using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveController : MonoBehaviour {

    public static ObjectiveController Instance { get; private set; }

    public Transform player;
    public Transform objectivePanel;
    public Text objectiveText;

    public bool HasBeenInController { get; private set; }

    private bool hasDisplayedDisableThings;
    private List<string> objectivesShown = new List<string>();
    private float timer, startFirstTimer;

    private void Start() {
        if (Instance != null) {
            Debug.LogError("Tried to create multiple ObjectiveControllers!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        objectivePanel.gameObject.SetActive(false);
    }

    private void Update() {
        startFirstTimer += Time.deltaTime;
        if (startFirstTimer >= 6f) {
            StartObjective(Objective.CheckControllerRoom);
        }

        if (hasDisplayedDisableThings) return;
        Vector3 pos = player.position;
        if (pos.x > 250 && pos.x < 275 && pos.z > 287 && pos.z < 305) {
            HasBeenInController = true;
            timer += Time.deltaTime;
            if (timer >= 3f) {
                hasDisplayedDisableThings = true;
                StartObjective(Objective.DisableThings);
            }
        }
    }

    public void StartObjective(string objective) {
        if (objectivesShown.Contains(objective)) return;
        if (objective == Objective.CheckControllerRoom &&
            objectivesShown.Contains(Objective.DisableThings)) return;
        if (objective == Objective.DisableThings &&
            objectivesShown.Contains(Objective.RepairGenerator)) return;

        objectivesShown.Add(objective);
        StartCoroutine(DisplayObjective(objective));
    }

    IEnumerator DisplayObjective(string objective) {
        objectivePanel.gameObject.SetActive(true);
        objectiveText.text = objective;
        yield return new WaitForSeconds(3f);
        objectiveText.text = "";
        objectivePanel.gameObject.SetActive(false);
    }
}

public class Objective {
    public const string CheckControllerRoom = "Check what's wrong in the controller room";
    public const string DisableThings = "Disable things that you don't need in order to save power";
    public const string RepairGenerator = "Repair the generator by pressing E when next to it";
}