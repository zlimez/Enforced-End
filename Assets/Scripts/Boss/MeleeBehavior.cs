using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBehavior : MonoBehaviour
{
    public float meleeWpnRadius = 1.0f;
    public float attackDelay = 0.4f;
    public float damage;
    public BossBehaviour boss;
    private EnemyHealth healthAndNav;

    void Awake() {
        boss = GetComponent<BossBehaviour>();
        healthAndNav = GetComponent<EnemyHealth>();
    }
    void Update() {
        if (Vector2.Distance(transform.position, EnemyHealth.player.transform.position) <= meleeWpnRadius) {
            healthAndNav.inAttackSeq = true;
            StartCoroutine(attackSeq());
        } 
    }

    IEnumerator attackSeq() {
        yield return new WaitForSeconds(attackDelay);
        // start animation
        int playerDir = EnemyHealth.determineFace(EnemyHealth.player.transform.position - transform.position);
        // if enemy is facing the correct direction 90 degree quadrants
        if (healthAndNav.face == playerDir)
            EnemyHealth.player.deductHealth(damage);
        boss.attackCompleted = true;
        healthAndNav.inAttackSeq = false;
    }

}