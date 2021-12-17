using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MeleeMinionBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator coroutine;
    public float damage = 10.0f;
    public float attackInterval = 0.5f;
    public Animator animator;

    private bool cooldownDone = true;

   
    void OnCollisionEnter2D(Collision2D col) {
        // Debug.Log("Contact with player");
        if (col.gameObject.name == "Player" && cooldownDone) {
            // coroutine = attackPlayer(col.gameObject.GetComponent<PlayerController>());
            animator.SetTrigger("Attack");
            col.gameObject.GetComponent<PlayerController>().deductHealth(damage);
            StartCoroutine(resetCooldown());
        }
    }

    // void OnTriggerEnter2D(Collider2D col) {
    //     if (col.gameObject.name == "Player" && cooldownDone) {
    //         // coroutine = attackPlayer(col.gameObject.GetComponent<PlayerController>());
    //         animator.SetTrigger("Attack");
    //         col.gameObject.GetComponent<PlayerController>().deductHealth(damage);
    //         StartCoroutine(resetCooldown());
    //     }
    // }

    // void OnCollisionExit2D(Collision2D col) {
    //     // Debug.Log("Contact stopped");
    //     if (col.gameObject.name == "Player") {
    //         StopCoroutine(coroutine);
    //         coroutine = null;
    //     }
    // }

    IEnumerator resetCooldown() {
        yield return new WaitForSeconds(attackInterval);
        cooldownDone = true;
    }

    IEnumerator attackPlayer(PlayerController player) {
        while (true) {
            // attack animation
            animator.SetTrigger("Attack");
            player.deductHealth(damage);
            yield return new WaitForSeconds(attackInterval);
        }
    }
}