using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBehaviour : MonoBehaviour
{
    public bool selected = false;
    public static GameObject player;
    private Rigidbody2D body;
    public GameObject projectile;
    public Transform firePoint;
    public float forceScale;
    // for the laser charging animation
    public float targetTime;
    public bool attacked = false;
    public int layermask;
    private BossBehaviour boss;

    void Awake()
    {
        player = player == null ? GameObject.Find("Player") : player;
        body = GetComponent<Rigidbody2D>();
        boss = GetComponent<BossBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selected && !attacked) {
            firePoint.transform.right = player.transform.position - firePoint.transform.position;
            // fire only when there is a clear shot
            Debug.Log("ready to fire");
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right);
            Debug.Log("fire to " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.GetComponent<PlayerController>() != null) {
                attacked = true;
                StartCoroutine(Shoot());
            }
        }
    }

    IEnumerator Shoot() {
        yield return new WaitForSeconds(targetTime);
        Instantiate(projectile, firePoint.position, firePoint.transform.rotation);
        // body.AddForce(-firePoint.right.normalized * forceScale, ForceMode2D.Impulse);
        boss.attackCompleted = true;
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
