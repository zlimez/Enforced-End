using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum weapon {RIFLE, MELEE};
    Rigidbody2D body;
    public GameObject bullet;
    public Transform firePoint;
    public weapon equipped = weapon.RIFLE;
    float horizontal;
    float vertical;
    public float runspeed = 10.0f;
    public float movement = 100.0f;
    public float sight = 100.0f;
    public float hear = 100.0f;
    public float armour = 100.0f;
    public float health = 100.0f;
    public float meleeDamage = 10.0f;
    public float degradationTime = 1.0f;
    public float maxShootAngleDev = 10.0f; 
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        // collider = GetComponent<BoxCollider2D>();
        StartCoroutine(degradeStats());
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {
            // Game Over
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.E)) {
            if (equipped == weapon.RIFLE) {
                equipped = weapon.MELEE;
            } else
                equipped = weapon.RIFLE;

        }
        if (Input.GetMouseButtonDown(0)) {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.right = point - (Vector2) transform.position;
            //trigger attack animation
            Debug.Log(transform.right);
            if (equipped == weapon.RIFLE) {
                Shoot();
            } else {
                // melee attack
            }
        } 
    }

    void FixedUpdate() {
        body.velocity = new Vector2(horizontal * runspeed * movement / 100.0f, vertical * runspeed * movement / 100.0f);
    }

    // void OnCollisionStay2D(Collision2D col) {
    //     if (Input.GetMouseButton(0)) {
    //         // trigger attack animation
    //         if (col.gameObject.tag == "Enemy")
    //             col.gameObject.GetComponent<EnemyHealth>().deductHealth(meleeDamage);
    //     }
    // }

    void Shoot() {
        Instantiate(bullet, firePoint.position, firePoint.transform.rotation);
    }

    // laser attack

    IEnumerator degradeStats() {
        while (true) {
            yield return new WaitForSeconds(degradationTime);
            sight -= 1.0f;
            hear -= 1.0f;
            movement -= 1.0f;
            armour -= 1.0f;
            health -= 1.0f;
        }
    }

    private void sacrificeForBoost(string sacrificed, string boosted, float amount) {
        switch (sacrificed) {
            case "sight": 
            sight -= amount;
            break;
            case "hear":
            hear -= amount;
            break;
            case "movement":
            movement -= amount;
            break;
            case "armour":
            armour -= amount;
            break;
        }

        switch (boosted) {
            case "sight": 
            sight += amount;
            break;
            case "hear":
            hear += amount;
            break;
            case "movement":
            movement += amount;
            break;
            case "armour":
            armour += amount;
            break;
        }
    }
}