using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBehavior : MonoBehaviour
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

    void Update() {
        if (selected && !attacked && Vector2.Distance(transform.position, player.transform.position) <= meleeWpnRadius) {
            Debug.Log("Melee attack init");
            attacked = true;
            healthAndNav.inAttackSeq = true;
            StartCoroutine(attackSeq());
        } 
    }

    IEnumerator attackSeq() {
        yield return new WaitForSeconds(attackDelay);
        // start animation
        int playerDir = EnemyHealth.determineFace(player.transform.position - transform.position);
        // if enemy is facing the correct direction 90 degree quadrants
        Debug.Log("player quadrant " + playerDir + " face quadrant " + healthAndNav.face);
        Debug.Log("player dist " + Vector2.Distance(transform.position, player.transform.position));
        if (healthAndNav.face == playerDir && Vector2.Distance(transform.position, player.transform.position) <= meleeWpnRadius) {
            Debug.Log("successful melee");
            player.GetComponent<PlayerController>().deductHealth(damage);
        }
        boss.attackCompleted = true;
        healthAndNav.inAttackSeq = false;
        deselectAttack();
    }

    public void selectAttack() {
        selected = true;
    }

    void deselectAttack() {
        selected = false;
        attacked = false;
    }
}