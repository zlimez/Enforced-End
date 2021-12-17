using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableLevelStart : MonoBehaviour
{
    public string tooltipText = "Start Mission";
    public string scenename = "SkyDiving";
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
        SceneManager.LoadScene(scenename);
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
