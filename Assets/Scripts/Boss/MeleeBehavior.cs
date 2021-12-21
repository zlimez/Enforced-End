using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBehavior : AttackBehaviour
{
    public float meleeWpnRadius = 1.0f;
    public float attackDelay = 0.4f;
    public float damage;
    private BossBehaviour boss;
    private EnemyHealth healthAndNav;
    public bool attacked = false;
    public AudioSource source;
    public PlayerController player;

    void Awake() {
        boss = GetComponent<BossBehaviour>();
        healthAndNav = GetComponent<EnemyHealth>();
    }

    public override bool attack() {
        if (Vector2.Distance(transform.position, healthAndNav.player.transform.position) <= meleeWpnRadius) {
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
        source.Play();
        int playerDir = EnemyHealth.determineFace(player.transform.position - transform.position);
        // if enemy is facing the correct direction 90 degree quadrants, enables dodging
        if (healthAndNav.face == playerDir && Vector2.Distance(transform.position, player.transform.position) <= meleeWpnRadius) {
            // Debug.Log("Boss facing " + playerDir + " Distance " + Vector2.Distance(transform.position, healthAndNav.player.transform.position));
            player.deductHealth(damage);
        }
        // Debug.Log("Boss facing " + healthAndNav.face + " Player at " + playerDir + "Distance " + Vector2.Distance(transform.position, healthAndNav.player.transform.position));
        boss.attackCompleted = true;
        yield return new WaitForSeconds(0.5f);
        healthAndNav.inAttackSeq = false;
        yield return null;
    }
}