using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	private void Start() {
        transform.Find("Text").GetComponent<Text>().text = Scenes.message;
        Cursor.lockState = CursorLockMode.None;
	}
}
