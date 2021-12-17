using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float speed = 5.0f;
    // public GameObject parentEnemy;
    public float damage = 15.0f;

    // void Awake() {
    //     parentEnemy = GameObject.Find("Mouth");
    // }
    // void Start() {
    //     GetComponent<Rigidbody2D>().velocity = -transform.right * speed;
    // }

    public void SetVelocity(Vector3 velocity) {
        GetComponent<Rigidbody2D>().velocity = velocity * speed;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.GetComponent<EnemyHealth>() != null) return;
        PlayerController player = col.gameObject.GetComponent<PlayerController>();
        if (player != null) 
            player.health -= damage;
        Destroy(gameObject);
    }
}
