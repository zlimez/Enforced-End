using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBehaviour : MonoBehaviour
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

    void OnEnabled() {
        attacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacked) {
            attacked = true;
            firePoint.transform.right = player.transform.position - transform.position;
            // fire only when there is a clear shot
            Debug.Log("ready to fire");
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, layermask);
            if (hit.collider.gameObject.GetComponent<PlayerController>() != null) {
                Debug.Log("fire to " + hit.transform.name);
                StartCoroutine(Shoot());
            }
        }
    }

    IEnumerator Shoot() {
        yield return new WaitForSeconds(targetTime);
        Instantiate(projectile, firePoint.position, firePoint.transform.rotation);
        body.AddForce(-firePoint.right.normalized * forceScale, ForceMode2D.Impulse);
        boss.attackCompleted = true;
        this.enabled = false;
    }
}
