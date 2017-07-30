using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour {

    public static string message = "This is one of my first games made in Unity, so I hope you like it! Unfortunatey I lost all my motivation in the end so it may seem kind of rushed.";

	public void GoToScene(string scene) {
        SceneManager.LoadScene(scene);
    }
}
