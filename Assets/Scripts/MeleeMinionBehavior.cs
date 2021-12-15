using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MeleeMinionBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator coroutine;
    public float damage = 10.0f;
    public float attackInterval = 0.5f;

   
    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log("Contact with player");
        if (col.gameObject.name == "Player") {
            coroutine = attackPlayer(col.gameObject.GetComponent<PlayerController>());
            StartCoroutine(coroutine);
        }
    }

    void OnCollisionExit2D() {
        Debug.Log("Contact stopped");
        StopCoroutine(coroutine);
        coroutine = null;
    }

    IEnumerator attackPlayer(PlayerController player) {
        while (true) {
            // attack animation
            player.health -= damage;
            yield return new WaitForSeconds(attackInterval);
        }
    }
}