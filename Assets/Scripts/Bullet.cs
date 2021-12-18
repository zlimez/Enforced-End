using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5.0f;
    // public static GameObject rifle;
    public float damage = 15.0f;
    private PlayerController player;

    void Awake() {
        // rifle = GameObject.Find("Rifle");
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Start() {
        // as sight decreases higher chance for bullet to fly off intended route
        float maxDegreeDev = player.maxShootAngleDev * (100 - player.sight) / 100;
        float shotDev = Random.Range(-maxDegreeDev, maxDegreeDev);
        Vector2 adjBulletDir = Quaternion.AngleAxis(shotDev, Vector3.forward) * transform.right;
        GetComponent<Rigidbody2D>().velocity = adjBulletDir.normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.GetComponent<PlayerController>() != null) return;
        EnemyHealth hitEnemy = col.gameObject.GetComponent<EnemyHealth>();
        if (hitEnemy != null) 
            hitEnemy.deductHealth(damage);
        Destroy(gameObject);
    }
}
