using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MeleeMinionBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator attackRoutine;
    public float damage = 10.0f;
    public float attackInterval = 1f;
    public Animator animator;
    public EnemyHealth minionHealth;
    // minion destroyed will cause boss to lose health
    public static EnemyHealth bossHealth;
    public float discount = 0.4f; // ratio of dmg to enforcer health when minion is destroyed
    public Transform contactPointChosen;
    public static List<Transform> contactPoints;
    public static bool contactsInitiated = false;
    public static int contactPointCounter = 0;
    public static PlayerController player;
    // private bool cooldownDone = true;

    void Awake() {
        if (!contactsInitiated)
            InitTargets();
        minionHealth = GetComponent<EnemyHealth>();
        bossHealth = bossHealth == null ? GameObject.Find("Boss").GetComponent<EnemyHealth>() : bossHealth;
    }

    void InitTargets() {
        contactPoints = new List<Transform>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        Transform contactHolder = player.transform.GetChild(3);
        for (int child = 0; child < contactHolder.childCount; child++) {
            contactPoints.Add(contactHolder.GetChild(child));
        }
        contactsInitiated = true;
    }

    public static Transform GetContactPoint() {
        Transform contact = contactPoints[contactPointCounter % player.transform.GetChild(3).childCount];
        contactPointCounter++;
        return contact;
    }
   
    void OnCollisionEnter2D(Collision2D col) {
        // Debug.Log("Contact with player");
        if (col.gameObject.name == "Player") {
            attackRoutine = attackPlayer(player);
            animator.SetTrigger("Attack");
            minionHealth.inAttackSeq = true;
            StartCoroutine(attackRoutine);
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        // Debug.Log("Contact stopped");
        if (col.gameObject.name == "Player") {
            StopCoroutine(attackRoutine);
            attackRoutine = null;
            minionHealth.inAttackSeq = false;
        }
    }

    void OnDestroy() {
        if (bossHealth == null) return;
        Debug.Log("Minion died");
        bossHealth.deductHealth(minionHealth.maxHealth * discount);
    }

    // IEnumerator resetCooldown() {
    //     yield return new WaitForSeconds(attackInterval);
    //     cooldownDone = true;
    // }

    IEnumerator attackPlayer(PlayerController player) {
        while (true) {
            // attack animation
            animator.SetTrigger("Attack");
            player.deductHealth(damage);
            yield return new WaitForSeconds(attackInterval);
        }
    }
}