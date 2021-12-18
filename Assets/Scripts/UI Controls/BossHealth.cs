using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyHealth boss;
    public Slider slider;
    void Awake()
    {
        slider = GetComponent<Slider>();
        boss = GameObject.Find("Boss").GetComponent<EnemyHealth>();
    }

    void Start() {
        slider.maxValue = boss.maxHealth;
        slider.value = boss.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = boss.health;
    }
}
