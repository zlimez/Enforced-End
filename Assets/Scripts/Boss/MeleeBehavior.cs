using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBehavior : AttackBehaviour
{
    public static GameObject player;
    public bool selected = false;
    public float meleeWpnRadius = 1.5f;
    public float attackDelay = 0.5f;
    public float damage;
    public BossBehaviour boss;
    private EnemyHealth healthAndNav;
    public bool attacked = false;

    void Awake() {
        boss = GetComponent<BossBehaviour>();
        healthAndNav = GetComponent<EnemyHealth>();
        player = player == null ? GameObject.Find("Player") : player;
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
        Debug.Log("player quadrant " + playerDir + " face quadrant " + healthAndNav.face);
        Debug.Log("player dist " + Vector2.Distance(transform.position, player.transform.position));
            Debug.Log("successful melee");
            player.GetComponent<PlayerController>().deductHealth(damage);
        }
        boss.attackCompleted = true;
        yield return new WaitForSeconds(0.5f);
        healthAndNav.inAttackSeq = false;
        yield return null;
    }
}