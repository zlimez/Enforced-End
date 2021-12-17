using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDroppingPlayer : MonoBehaviour
{
    public GameObject droppingPlayerPrefab;
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(DropPlayer());
    }

    // Update is called once per frame
    IEnumerator DropPlayer() {
        yield return new WaitForSeconds(0.2f);
        GameObject fallingPlayer = Instantiate(droppingPlayerPrefab, transform.position, Quaternion.identity);
        fallingPlayer.GetComponent<ThrownMinion>().InitializeFallOnly(20.0f);
    }
}
