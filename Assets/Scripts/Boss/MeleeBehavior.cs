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
    public bool attacked = false;

    void Awake() {
        boss = GetComponent<BossBehaviour>();
        healthAndNav = GetComponent<EnemyHealth>();
    }

    void OnEnabled() {
        attacked = false;
    }
    void Update() {
        if (!attacked && Vector2.Distance(transform.position, EnemyHealth.player.transform.position) <= meleeWpnRadius) {
            attacked = true;
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
        this.enabled = false;
    }
}