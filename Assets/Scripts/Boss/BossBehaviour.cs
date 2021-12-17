using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    private EnemyHealth healthAndNav;
    private MeleeBehavior melee;
    private RangedBehaviour ranged;
    private AOEBehaviour aoe;
    private SummonBehaviour summon;
    public float attackInterval = 1.5f; 
    public float attackTimeCd;
    public bool attackCompleted = true;
    public static List<string> attackTypes = new List<string>();
    // Start is called before the first frame update
    public static List<string> sortedAttack = new List<string>();
    public static double[] dist = new double[] {0.5, 0.2, 0.2, 0.1};
    void Awake() {
        healthAndNav = GetComponent<EnemyHealth>();
        melee = GetComponent<MeleeBehavior>();
        ranged = GetComponent<RangedBehaviour>();
        aoe = GetComponent<AOEBehaviour>();
        summon = GetComponent<SummonBehaviour>();
        attackTypes.Add("Melee");
        attackTypes.Add("Ranged");
        attackTypes.Add("AOE");
        attackTypes.Add("Summon");
        attackTimeCd = attackInterval;
        genAttackPattern();
    }
    // Update is called once per frame
    void Update()
    {
        if (attackTimeCd <= 0) {
            float selected = Random.Range(0f, 1f);
            string chosenAttack = determineAttack(selected);
            healthAndNav.changeBehaviour(chosenAttack);
            attackTimeCd = attackInterval;
            attackCompleted = false;
            switch (chosenAttack) {
                case "Melee":
                melee.enabled = true;
                break;
                case "Ranged":
                ranged.enabled = true;
                break;
                case "AOE":
                aoe.enabled = true;
                break;
                case "Summon":
                summon.enabled = true;
                break;
            }
        } else if (attackCompleted && attackTimeCd > 0) {
            attackTimeCd -= Time.deltaTime;
        }
    }

    // close dist melee
    // middle dist AOE (8 units radius) mid speed movement encourage players to hide behind obstacles
    // laser (targeting mechanism must outrun)
    // Encourage close combat early on with back stab bonus mech?
    public void genAttackPattern() {
        // determine frequency of each attack
        // most frequent 50% 20% 20% 10%
        while (attackTypes.Count > 0) {
            int selected = Random.Range(0, attackTypes.Count - 1);
            sortedAttack.Add(attackTypes[selected]);
            attackTypes.RemoveAt(selected);
        }
        foreach (string attack in sortedAttack) {
            Debug.Log(attack);
        }
        gameObject.transform.GetChild(0).tag = sortedAttack[0];
    }

    public string determineAttack(float percentile) {
        if (percentile <= dist[0]) {
            return sortedAttack[0];
        } else if (percentile <= dist[1]) {
            return sortedAttack[1];
        } else if (percentile <= dist[2]) {
            return sortedAttack[2];
        } else 
            return sortedAttack[3];
    }
}
