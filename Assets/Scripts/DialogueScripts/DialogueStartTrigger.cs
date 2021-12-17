using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStartTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool started = false;

    void Awake() {
        TriggerDialogue();
    }

    public void TriggerDialogue ()
    {
        if (started || DialogueManager._instance == null) return;
        started = true;
        DialogueManager._instance?.StartDialogue(dialogue);
    }
}
