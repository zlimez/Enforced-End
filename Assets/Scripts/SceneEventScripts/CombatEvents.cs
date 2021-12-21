using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CombatEvents : MonoBehaviour
{
    public Animator cyborgAnimator;
    public PlayerController playerController;
    public UnityEvent winPart2;
    public UnityEvent ending;
    public ChangeSceneCrossfade changeSceneCrossfade;
    public void PlayerDied() {
        changeSceneCrossfade.LoadNextLevelWithColor("Title", "#FFFFFF");
    }

    public void WinPart1End() {
        cyborgAnimator.SetTrigger("Dying");
        winPart2.Invoke();
    }

    public void CountdownEnd() {
        cyborgAnimator.SetTrigger("Die");
        StartCoroutine(DelayForceTriggerTransition());
        ending.Invoke();
    }

    IEnumerator DelayForceTriggerTransition() {
        yield return new WaitForSecondsRealtime(1.0f);
        changeSceneCrossfade.ForceTriggerTransition("#FFFFFF");
    }

    public void EndingCheck() {
        // cyborgAnimator.SetTrigger("Die");
        if (playerController.armour > 50) {
            changeSceneCrossfade.LoadNextLevelWithColor("HospitalCutscene", "#FFFFFF");
        } else {
            PlayerDied();
        }
    }
}
