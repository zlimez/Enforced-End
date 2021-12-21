using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBehaviour : AttackBehaviour
{
    public GameObject player;
    // private Rigidbody2D body;
    public GameObject projectile;
    public Transform firePoint;
    public float forceScale;
    // for the laser charging animation
    public float targetTime;
    public bool attacked = false;
    private BossBehaviour boss;
    public AudioSource charge;
    // Start is called before the first frame update
    void Awake()
    {
        // body = GetComponent<Rigidbody2D>();
        boss = GetComponent<BossBehaviour>();
    }

    override public bool attack() {
        boss.animator.SetTrigger("ChargeLaser");
        charge.Play();
        StartCoroutine(Shoot());
        return true;
    }

    public override string getAttackTag()
    {
        return "Ranged";
    }

    IEnumerator Shoot() {
        yield return new WaitForSeconds(targetTime);
        charge.Stop();
        boss.animator.SetTrigger("FireLaser");
        Vector3 moveDir = (player.transform.position - transform.position).normalized;
        GameObject go = Instantiate(projectile, firePoint.position, Quaternion.identity);
        go.GetComponent<Projectiles>().SetVelocity(moveDir);
        // body.AddForce(-firePoint.right.normalized * forceScale, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1.0f);
        boss.attackCompleted = true;
        yield return null;
    }
}
