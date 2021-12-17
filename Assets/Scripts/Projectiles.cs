using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public static GameObject target;
    public float speed = 5.0f;
    // public GameObject parentEnemy;
    public float damage = 20.0f;
    public Rigidbody2D rb;

    void Start() {
        target = target == null ? GameObject.Find("Player") : target;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (target.transform.position - transform.position).normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D col) {
        PlayerController player = col.gameObject.GetComponent<PlayerController>();
        if (player != null) 
            player.deductHealth(damage);
        Destroy(gameObject);
    }
}
