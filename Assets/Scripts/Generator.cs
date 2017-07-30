using UnityEngine;
using UnityEngine.SceneManagement;

public class Generator : MonoBehaviour {

    public Transform player;

    private bool hasRestarted;

    private void Update() {
        if (hasRestarted) return;
        if (Vector3.Distance(player.position, transform.position) < 6f) {
            ObjectiveController.Instance.StartObjective(Objective.RepairGenerator);
            if (Input.GetKeyDown(KeyCode.E)) {
                hasRestarted = true;
                AlarmController.Instance.StopAlarm();
                Scenes.message = "You managed to restart the generator! Congratulations :)";
                SceneManager.LoadScene("MenuScene");
                return;
            }
        }
    }
}
