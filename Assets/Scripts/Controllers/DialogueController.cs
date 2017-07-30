using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {

    public static DialogueController Instance { get; private set; }

    public Text subtitle;
    public Phrase[] phrases;
    
    private AudioSource source;

    private void Start() {
        if (Instance != null) {
            Debug.LogError("Tried to create multiple DialogueControllers!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        source = gameObject.AddComponent<AudioSource>();
    }

    public void StartDialogue() {
        StartCoroutine(StartProcess());
    }

    IEnumerator StartProcess() {
        int index = 0;
        while (index < phrases.Length) {
            if (!source.isPlaying) {
                yield return new WaitForSeconds(2f);
                source.PlayOneShot(phrases[index].audio, 1f);
                subtitle.text = phrases[index].text;
                index++;
            } else {
                yield return null;
            }
        }

        yield return new WaitForSeconds(3f);
        subtitle.text = "";
        ObjectiveController.Instance.StartObjective(Objective.CheckControllerRoom);
    }
}

[Serializable]
public class Phrase {

    public AudioClip audio;
    public string text;
}