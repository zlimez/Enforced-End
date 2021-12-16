using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThrowMinion : MonoBehaviour
{
    public GameObject droppingMinionPrefab;
    // void OnMouseDown(){
        
    // }

    void SpawnFallingMinion() {
        GameObject fallingMinion = Instantiate(droppingMinionPrefab, transform.position, Quaternion.identity);
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // Vector2 groundVelocity = new Vector2(2, 2);
        Vector2 groundVelocity = new Vector2(
            mousePosition.x - transform.position.x, 
            mousePosition.y - transform.position.y
        );
        // groundVelocity.Normalize();
        groundVelocity = groundVelocity * new Vector2(0.5f, 0.5f);
        fallingMinion.GetComponent<ThrownMinion>().Initialize(groundVelocity, 6);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            SpawnFallingMinion();
        }
    }
}
