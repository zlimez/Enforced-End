using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBehaviour : AttackBehaviour
{
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
    // Start is called before the first frame update
    void Awake()
    {
        player = player == null ? GameObject.Find("Player") : player;
        body = GetComponent<Rigidbody2D>();
        boss = GetComponent<BossBehaviour>();
        layermask = ~(LayerMask.GetMask("Enemy"));
    }

    override public bool attack() {
        firePoint.transform.right = player.transform.position - transform.position;
        // fire only when there is a clear shot
        RaycastHit hit;
        if (Physics.Linecast (firePoint.position, player.transform.position, out hit)) {
            if(hit.transform.tag == "player") {
                Debug.Log("fire to " + hit.transform.name);
                StartCoroutine(Shoot());
                return true;
            }
        }
        return false;
    }

    public override string getAttackTag()
    {
        return "Ranged";
    }

    IEnumerator Shoot() {
        yield return new WaitForSeconds(targetTime);
        Instantiate(projectile, firePoint.position, firePoint.transform.rotation);
        body.AddForce(-firePoint.right.normalized * forceScale, ForceMode2D.Impulse);
        boss.attackCompleted = true;
        this.enabled = false;
    }
}
