using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBehavior : MonoBehaviour
{
    // minion melee attack
    private IEnumerator coroutine;
    public float meleeWpnRadius = 1.0f;
    public float attackDelay = 0.2f;
    public BossBehaviour boss;

    void Awake() {
        boss = GetComponent<BossBehaviour>();
    }
    void Update() {
        if (Vector2.Distance(transform.position, BossBehaviour.player.transform.position) <= meleeWpnRadius) {
            // attack animation
            boss.attackCompleted = true;
        } 
    }

    // IEnumerator attackSeq() {
    //     player 
    //     yield return new WaitForSeconds(attackDelay);
    // }

}