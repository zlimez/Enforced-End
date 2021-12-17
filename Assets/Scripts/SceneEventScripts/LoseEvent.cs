using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LoseEvent : MonoBehaviour
{
    public EnemyHealth cyborgHealthController;
    public UnityEvent loseFull;
    public UnityEvent loseHalf;
    public UnityEvent loseQuarter;

    void onLose() {
        float healthPercent = cyborgHealthController.health / cyborgHealthController.maxHealth;
        if (healthPercent > 0.5f) {
            loseFull.Invoke();
        }
        else if (healthPercent > 0.25f) {
            loseHalf.Invoke();
        }
        else {
            loseQuarter.Invoke();
        }
    }
}
