using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class InteractableLevelStart : MonoBehaviour
{
    public string tooltipText = "Start Mission";
    public UnityEvent levelStartEvent;
    void Start() {
        enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            enabled = false;
            levelStartEvent.Invoke();
            TooltipManager._instance.HideToolTip();
        }
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
