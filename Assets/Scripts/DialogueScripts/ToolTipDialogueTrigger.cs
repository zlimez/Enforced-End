using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipDialogueTrigger : MonoBehaviour
{
    public string tooltipText;
    public Dialogue dialogue;

    void Start() {
        dialogue.dialogueEndEvent.AddListener(enableInteract);
    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            TriggerDialogue();
            TooltipManager._instance.HideToolTip();
            enabled = false;
        }
    }

    public void TriggerDialogue ()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name == "Player") {
            enableInteract();
        }
    }

    void enableInteract() {
        TooltipManager._instance.SetAndShowTooltip(tooltipText, transform.position);
        enabled = true;
    }
    
    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.name == "Player") {
            TooltipManager._instance.HideToolTip();
            enabled = false;
        }
    }
}
