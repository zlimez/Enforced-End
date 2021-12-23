using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
  public enum weapon {RIFLE, MELEE};
    Rigidbody2D body;
    public GameObject bullet;
    public Transform firePoint;
    public Animator animator;
    public UnityEvent onDeathEvent;
    public weapon equipped = weapon.RIFLE;
    float horizontal;
    float vertical;
    public bool facingRight = true;
    public float runspeed = 10.0f;
    public float movement = 100.0f;
    public float sight = 100.0f;
    public float hear = 100.0f;
    public float armour = 100.0f;
    public float dmgReduction = 10f;
    public float health = 100.0f;
    public float degradationTime = 1.0f;
    public float shotInterval = 0.3f;
    public float shotCd;
    public float overheatThres = 50.0f;
    public float canShootAgnThres = 45.0f;
    public bool overheated = false;
    public float baseTemp = 0f;
    public float perRoundTempInc = 5.0f;
    public float baseCoolingRate = 3.0f; // 25 degree temp diff
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
            animator.SetTrigger("Die");
            onDeathEvent?.Invoke();
            enabled = false;
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        // if (Input.GetKeyDown(KeyCode.E)) {
        //     if (equipped == weapon.RIFLE) {
        //         equipped = weapon.MELEE;
        //     } else
        //         equipped = weapon.RIFLE;
        // }
        shotCd -= Time.deltaTime;
        if (overheated) 
            overheated = baseTemp > canShootAgnThres;
        
        Vector2 mouseDir = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2) transform.position;
        if ((mouseDir.x > 0 && !facingRight) || (mouseDir.x < 0 && facingRight)) 
            Flip();

        if (!Pause.GameIsPaused && Input.GetMouseButton(0) && shotCd <= 0 && !overheated) {
            shotCd = shotInterval;
            firePoint.transform.right = mouseDir;
            // if ((shotDir.x > 0 && !facingRight) || (shotDir.x < 0 && facingRight))
            //     Flip();
            //trigger attack animation
            if (equipped == weapon.RIFLE) {
                Shoot();
            } else {
                // melee attack
            }
        } 
    }

    void FixedUpdate() {
        animator.SetFloat("Speed", horizontal + vertical);
        Vector2 baseVelocity = new Vector2(horizontal, vertical); 
        baseVelocity.Normalize();
        body.velocity = baseVelocity * runspeed * movement / 100.0f;
    }

    void Shoot() {
        Instantiate(bullet, firePoint.position, firePoint.transform.rotation);
        baseTemp += perRoundTempInc;
        overheated = baseTemp >= overheatThres;
    }

    IEnumerator degradeStats() {
        while (true) {
            yield return new WaitForSeconds(degradationTime);
            if (sight >= 1)
                sight -= 1.0f;
            if (hear >= 1)
                hear -= 1.0f;
            if (movement >= 1)
                movement -= 1.0f;
            if (armour >= 1)
                armour -= 1.0f;
            if (health > 0)
                health -= 0.1f;
            if (baseTemp > 0)
                baseTemp -= baseCoolingRate * baseTemp / 25;
            AudioListener.volume = 0.2f + (hear / 128f);
        }
    }

    public void deductHealth(float dmg) {
        Debug.Log("health deducted " + dmg);
        health -= (dmg - dmgReduction * armour / 100);
    }

    void Flip() {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}