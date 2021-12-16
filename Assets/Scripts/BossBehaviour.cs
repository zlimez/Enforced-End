using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public float attackInterval = 1.5f; 
    public float attackTimeCd;
    public static List<string> attackTypes = new List<string>();
    // Start is called before the first frame update
    public static List<string> sortedAttack = new List<string>();
    public static double[] dist = new double[] {0.5, 0.2, 0.2, 0.1};
    void Awake() {
        attackTypes.Add("melee");
        attackTypes.Add("ranged");
        attackTypes.Add("aoe");
        attackTypes.Add("minions");
        attackTimeCd = attackInterval;
        genAttackPattern();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimeCd <= 0) {
            float selected = Random.Range(0f, 1f);
            attackTimeCd = attackInterval;
            switch (determineAttack(selected)) {
                case "melee":
                break;
                case "ranged":
                break;
                case "aoe":
                break;
                case "minions":
                break;
            }
        }
    }

    // melee (cyborg jump slash 4 units) high speed t ofrce close combat player need to dodge)
    // middle dist AOE (8 units radius) mid speed movement encourage players to hide behind obstacles
    // laser (targeting mechanism must outrun)
    // Encourage close combat early on with back stab bonus mech?
    public void genAttackPattern() {
        // determine frequency of each attack
        // most frequent 50% 20% 20% 10%
        while (attackTypes.Count > 0) {
            int selected = Random.Range(0, attackTypes.Count - 1);
            sortedAttack.Add(attackTypes[selected]);
        }
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
