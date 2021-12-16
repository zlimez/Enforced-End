using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    public Slider slider;
    public static PlayerController player;
    public static Slider[] allBars = new Slider[4];
    // Start is called before the first frame update
    void Awake() {
        player = player == null ? GameObject.Find("Player").GetComponent<PlayerController>() : player;
        Debug.Log(player.gameObject.name);
        slider = GetComponent<Slider>();
        switch (gameObject.name) {
            case "Movement bar":
            allBars[0] = slider;
            break;
            case "Armour bar":
            allBars[1] = slider;
            break;
            case "Hearing bar":
            allBars[2] = slider;
            break;
            case "Sight bar":
            allBars[3] = slider;
            break;
        }
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
