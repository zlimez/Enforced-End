using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThrownMinion : MonoBehaviour
{
    public GameObject minionPrefab;
    public Transform trnsObject;
    public Transform trnsBody;
    public Transform trnsShadow;

    public float gravity = -10;
    private Vector2 groundVelocity;
    private float verticalVelocity;

    private bool isGrounded;

    private UnityEvent hitGroundEvent;

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        CheckGroundHit();
    }

    public void Initialize(Vector2 groundVelocity, float verticalVelocity) {
        this.groundVelocity = groundVelocity;
        this.verticalVelocity = verticalVelocity;
    }

    public void InitializeFallOnly(float verticalPosition) {
        this.groundVelocity = new Vector2(0, 0);
        this.verticalVelocity = 0;
        trnsBody.position += new Vector3(0, verticalPosition, 0);
    }

    public void InitializeHitGroundEvent(UnityEvent hitGroundEvent) {
        this.hitGroundEvent = hitGroundEvent;
    }

    void UpdatePosition() {
        if (isGrounded) {
            return;
        }
        verticalVelocity += gravity * Time.deltaTime;
        trnsBody.position += new Vector3(0, verticalVelocity, 0) * Time.deltaTime;
        trnsObject.position += (Vector3) groundVelocity * Time.deltaTime;
    }

    void CheckGroundHit() {
        if (trnsBody.position.y < trnsObject.position.y && !isGrounded) {
            trnsBody.position = trnsObject.position;
            isGrounded = true;
            OnGroundHit();
        }
    }

    void OnGroundHit() {
        if (hitGroundEvent != null) {
            hitGroundEvent.Invoke();
            Destroy(this.gameObject);
            return;
        }
        Instantiate(minionPrefab, trnsObject.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
