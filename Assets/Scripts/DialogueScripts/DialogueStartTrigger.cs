using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStartTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    void Awake() {
        TriggerDialogue();
    }

    public void TriggerDialogue ()
    {
        DialogueManager._instance?.StartDialogue(dialogue);
    }
}
