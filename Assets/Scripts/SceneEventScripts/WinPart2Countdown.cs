using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class WinPart2Countdown : MonoBehaviour
{
    public Text countdownText;
    public UnityEvent nextEvent;
    public int totalTime = 20;
    private int timeLeft = 1;
    void Start() {
        gameObject.SetActive(false);
    }
    public void StartCountdown()
    {
        gameObject.SetActive(true);
        timeLeft = totalTime + 1;
        StartCoroutine(Countdown());     
    }

    IEnumerator Countdown () {
        while (timeLeft > 0) {
            timeLeft -= 1;
            countdownText.text = "00:" + timeLeft.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        nextEvent.Invoke();
        gameObject.SetActive(false);
    }
}
