using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBehavior : AttackBehaviour
{
    public float meleeWpnRadius = 1.0f;
    public float attackDelay = 0.4f;
    public float damage;
    public BossBehaviour boss;
    private EnemyHealth healthAndNav;
    public bool attacked = false;

    void Awake() {
        boss = GetComponent<BossBehaviour>();
        healthAndNav = GetComponent<EnemyHealth>();
    }

    public override bool attack() {
        if (Vector2.Distance(transform.position, EnemyHealth.player.transform.position) <= meleeWpnRadius) {
            healthAndNav.inAttackSeq = true;
            StartCoroutine(attackSeq());
            return true;
        }
        return false;
    }

    public override string getAttackTag()
    {
        return "Melee";
    }

    IEnumerator attackSeq() {
        yield return new WaitForSeconds(attackDelay);
        // start animation
        boss.animator.SetTrigger("Melee");
        int playerDir = EnemyHealth.determineFace(EnemyHealth.player.transform.position - transform.position);
        // if enemy is facing the correct direction 90 degree quadrants
        if (healthAndNav.face == playerDir)
            EnemyHealth.player.deductHealth(damage);
        boss.attackCompleted = true;
        yield return new WaitForSeconds(0.5f);
        healthAndNav.inAttackSeq = false;
        yield return null;
    }
}