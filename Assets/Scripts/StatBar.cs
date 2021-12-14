using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    public Slider slider;
    public static PlayerController player;
    // Start is called before the first frame update
    void Awake() {
        player = player == null ? GameObject.Find("Player").GetComponent<PlayerController>() : player;
        Debug.Log(player.gameObject.name);
        slider = GetComponent<Slider>();
    }
    void OnEnable() {
        switch (gameObject.name) {
            case "Movement bar":
            Debug.Log("movement bar");
            Debug.Log(player.gameObject.name);
            slider.value = player.movement;
            break;
            case "Armour bar":
            Debug.Log("armour bar");
            slider.value = player.armour;
            break;
            case "Hearing bar":
            Debug.Log("hearing bar");
            slider.value = player.hear;
            break;
            case "Sight bar":
            Debug.Log("sight bar");
            slider.value = player.sight;
            break;
        }
    }
}
