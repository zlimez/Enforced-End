using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartScript : MonoBehaviour
{
    public string tooltipText = "Start Mission";
    void Start() {
        enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            enabled = false;
            StartLevel();
            TooltipManager._instance.HideToolTip();
        }
    }

    void StartLevel() {

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
