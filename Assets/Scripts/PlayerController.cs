using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public enum weapon {RIFLE, MELEE};
    public bool facingRight = true;
    Rigidbody2D body;
    public GameObject bullet;
    public Transform firePoint;
    public Animator animator;
    private UnityEvent onDeathEvent;
    public weapon equipped = weapon.RIFLE;
    float horizontal;
    float vertical;
    public float runspeed = 10.0f;
    public float movement = 100.0f;
    public float sight = 100.0f;
    public float hear = 100.0f;
    public float armour = 100.0f;
    public float dmgReduction = 10f;
    public float health = 100.0f;
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
            onDeathEvent.Invoke();
            enabled = false;
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
            firePoint.right = point - (Vector2) transform.position;
            //trigger attack animation
            // Debug.Log(transform.right);
            if (equipped == weapon.RIFLE) {
                Shoot();
            } else {
                // melee attack
            }
        } 
    }

    void FixedUpdate() {
        animator.SetFloat("Speed", horizontal + vertical);
        body.velocity = new Vector2(horizontal * runspeed * movement / 100.0f, vertical * runspeed * movement / 100.0f);
        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight)) 
            Flip();
    }

    void Shoot() {
        Instantiate(bullet, firePoint.position, firePoint.transform.rotation);
    }

    // laser attack

    IEnumerator degradeStats() {
        while (true) {
            yield return new WaitForSeconds(degradationTime);
            if (sight > 0)
                sight -= 1.0f;
            if (hear > 0)
                hear -= 1.0f;
            movement -= 1.0f;
            if (movement > 0)
                armour -= 1.0f;
            if (health > 0)
                health -= 1.0f;
        }
    }

    public void deductHealth(float dmg) {
        health -= (dmg - dmgReduction * armour / 100);
    }

    void Flip() {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}