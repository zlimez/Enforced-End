using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnDroppingPlayer : MonoBehaviour
{
    public GameObject droppingPlayerPrefab;
    public GameObject player;
    // Start is called before the first frame update
    void Start() {
        this.GetComponent<SpriteRenderer>().enabled = false;
        player.SetActive(false);
        StartCoroutine(DropPlayer());
    }

    IEnumerator DropPlayer() {
        yield return new WaitForSeconds(0.2f);
        GameObject fallingPlayer = Instantiate(droppingPlayerPrefab, transform.position, Quaternion.identity);
        UnityEvent hitGroundEvent = new UnityEvent();
        hitGroundEvent.AddListener(onHitGround);
        fallingPlayer.GetComponent<ThrownMinion>().InitializeFallOnly(20.0f);
        fallingPlayer.GetComponent<ThrownMinion>().InitializeHitGroundEvent(hitGroundEvent);
    }

    void onHitGround() {
        this.GetComponent<SpriteRenderer>().enabled = true;
        StartCoroutine(DieAfterSeconds(0.5f));
        player.SetActive(true);
    }

    IEnumerator DieAfterSeconds(float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
