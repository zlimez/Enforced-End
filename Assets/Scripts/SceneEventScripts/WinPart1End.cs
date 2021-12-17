using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinPart1End : MonoBehaviour
{
    public Animator cyborgAnimator;
    public UnityEvent nextEvent;
    public void TriggerNextEvent() {
        cyborgAnimator.SetTrigger("Dying");
        nextEvent.Invoke();
    }
}
