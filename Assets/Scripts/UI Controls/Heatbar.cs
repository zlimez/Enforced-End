using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heatbar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public PlayerController player;
    void Awake()
    {
        slider = GetComponent<Slider>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.baseTemp;
    }
}
