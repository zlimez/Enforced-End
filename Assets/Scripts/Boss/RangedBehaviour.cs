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
    // Start is called before the first frame update
    void Start()
    {
        player = player == null ? GameObject.Find("Player") : player;
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        firePoint.transform.right = player.transform.position - transform.position;
        // fire only when there is a clear shot
        Debug.Log("ready to fire");
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right);
        if (hit.collider.gameObject.GetComponent<PlayerController>() != null) {
            Debug.Log("fire to " + hit.transform.name);
            Shoot();
            this.enabled = false;
        }
    }

    void Shoot() {
        Instantiate(projectile, firePoint.position, firePoint.transform.rotation);
        body.AddForce(-firePoint.right.normalized * forceScale, ForceMode2D.Impulse);
    }
}
