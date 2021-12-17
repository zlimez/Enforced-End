using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBehaviour : AttackBehaviour
{
    public GameObject player;
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
        body = GetComponent<Rigidbody2D>();
        boss = GetComponent<BossBehaviour>();
        layermask = ~(LayerMask.GetMask("Enemy"));
    }

    override public bool attack() {
        boss.animator.SetTrigger("ChargeLaser");
        StartCoroutine(Shoot());
        return true;
    }

    public override string getAttackTag()
    {
        return "Ranged";
    }

    IEnumerator Shoot() {
        yield return new WaitForSeconds(targetTime);
        boss.animator.SetTrigger("FireLaser");
        Instantiate(projectile, firePoint.position, firePoint.transform.rotation);
        body.AddForce(firePoint.right.normalized * forceScale, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1.0f);
        boss.attackCompleted = true;
        yield return null;
    }
}
