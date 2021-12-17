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
    private AttackBehaviour currentChosenAttack;
    public Animator animator;
    public float attackInterval = 2.0f; 
    public float attackTimeCd;
    public float attackStuckInterval = 9.0f;
    public float attackStuckTimeCd;
    public bool attackInProgress = false;
    public bool attackCompleted = true;
    private List<AttackBehaviour> attackTypes = new List<AttackBehaviour>();
    // Start is called before the first frame update
    private List<AttackBehaviour> sortedAttack = new List<AttackBehaviour>();
    public double[] distribution = new double[] {0.4, 0.8, 0.9, 1.0};
    void Awake() {
        healthAndNav = GetComponent<EnemyHealth>();
        melee = GetComponent<MeleeBehavior>();
        ranged = GetComponent<RangedBehaviour>();
        aoe = GetComponent<AOEBehaviour>();
        summon = GetComponent<SummonBehaviour>();
        attackTypes.Add(melee);
        attackTypes.Add(ranged);
        attackTypes.Add(aoe);
        attackTypes.Add(summon);
        attackTimeCd = attackInterval;
        attackStuckTimeCd = attackStuckInterval;
        genAttackPattern();
        ReselectAttack();
    }
    // Update is called once per frame
    void Update()
    {
        if (attackTimeCd <= 0) {
            ReselectAttack();
        } else if (attackStuckTimeCd <= 0) {
            ReselectAttack();
        } else if (attackCompleted && attackTimeCd > 0) {
            attackTimeCd -= Time.deltaTime;
        } else {
            attackStuckTimeCd -= Time.deltaTime;
        }

        if (!attackInProgress) {
            attackInProgress = currentChosenAttack.attack();
        }
    }

    void ReselectAttack() {
        float selected = Random.Range(0f, 1f);
        currentChosenAttack = determineAttack(selected);
        healthAndNav.changeBehaviour(currentChosenAttack.getAttackTag());
        attackInProgress = false;
        attackCompleted = false;
        attackTimeCd = attackInterval;
        attackStuckTimeCd = attackStuckInterval;
        Debug.Log(currentChosenAttack.getAttackTag());
    }

    // close distance melee
    // middle distance AOE (8 units radius) mid speed movement encourage players to hide behind obstacles
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
        gameObject.transform.GetChild(0).tag = sortedAttack[0].getAttackTag();
    }

    public AttackBehaviour determineAttack(float percentile) {
        if (percentile <= distribution[0]) {
            return sortedAttack[0];
        } else if (percentile <= distribution[1]) {
            return sortedAttack[1];
        } else if (percentile <= distribution[2]) {
            return sortedAttack[2];
        } else 
            return sortedAttack[3];
    }
}
