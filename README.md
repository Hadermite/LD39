using UnityEngine;

public class DialogueController : MonoBehaviour {

    private AudioClip[] phrases;

    private void Start() {
        phrases = (AudioClip[])Resources.LoadAll("Audio/Dialogue", typeof(AudioClip));
        Debug.Log(phrases.Length);
    }
}
