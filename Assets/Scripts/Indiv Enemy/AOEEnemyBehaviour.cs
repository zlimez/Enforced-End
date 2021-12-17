using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEEnemyBehaviour : MonoBehaviour
{
    public static GameObject playerObject;
    public static PlayerController player;
    public float radius = 8;
    public float damage;
    public float coolDown = 3.0f;
    public float fullAudioLength = 5.0f;
    public bool attackSeqCompleted = true;
    public AudioSource source;
    public GameObject testPrefab;
    // Start is called before the first frame update
    void Start()
    {
        player = player == null ? GameObject.Find("Player").GetComponent<PlayerController>() : player;
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDown > 0 && attackSeqCompleted) {
            coolDown -= Time.deltaTime;
        } else if (coolDown <= 0) {
            Debug.Log("ready for aoe");
            attackSeqCompleted = false;
            StartCoroutine(attackSeq());
            coolDown = 3.0f;
        }
    }

    public void inflictDmg() {
        // start animation
        Debug.Log("aoe unleashed");
        if (Vector2.Distance(transform.position, player.transform.position) <= radius) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
            // player can hide behind wall
            if (hit.collider.gameObject.GetComponent<PlayerController>() != null)
                player.deductHealth(damage);
        }
        Instantiate(testPrefab, transform.position, Quaternion.identity);
    }

    // gradual increase in volume
    IEnumerator AudioQueue() {
        while (true) {
            Debug.Log("Vol " + source.volume);
            yield return new WaitForSeconds(0.1f);
            source.volume += 0.02f;
        }
    }

    IEnumerator attackSeq() {
        yield return new WaitForSeconds(fullAudioLength * (1 - player.hear / 100));
        source.Play();
        IEnumerator audioQueue = AudioQueue();
        StartCoroutine(audioQueue);
        yield return new WaitForSeconds(player.hear / 100 * fullAudioLength);
        StopCoroutine(audioQueue);
        inflictDmg();
        source.Stop();
        source.volume = 0;
        attackSeqCompleted = true;
    }
}
