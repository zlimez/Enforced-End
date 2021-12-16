using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RangedEnemyBehavior : MonoBehaviour
{
    public static GameObject player;
    public GameObject projectile;
    public Transform firePoint;
    public float attackInterval = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        transform.right = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.right = player.transform.position - transform.position;
        if (attackInterval > 0) {
            attackInterval -= Time.deltaTime;
        } else {
            Debug.Log("ready to fire");
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right);
            // if (hit.collider != null && ) 
            //     Debug.Log("Shot" + hit.transform.name);
            if (hit.collider.gameObject.GetComponent<PlayerController>() != null) {
                Debug.Log("fire to " + hit.transform.name);
                Shoot();
                attackInterval = 2.0f;
            }
        }
    }

    void Shoot() {
        Instantiate(projectile, firePoint.position, firePoint.transform.rotation);
    }
}
