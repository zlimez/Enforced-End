using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject droppingMinionPrefab;
    public int summonCount = 5;
    public float minionSpawnInterval = 1;
    // minions are spawned with boss at the centre concentrated in the sector nearest to player
    public float spawnDistance = 4f; 
    public float minionDegreeSep = 10f;
    public bool attacked = false;
    private EnemyHealth healthAndNav;
    void Awake()
    {
        healthAndNav = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacked) {
            StartCoroutine(SpawnMinions());
            healthAndNav.inAttackSeq = true;
        }
    }

    IEnumerator SpawnMinions() {
        // start animation
        int minionCount = 0;
        float angleDev = summonCount % 2 == 0 ? summonCount / 2 * -minionDegreeSep : (summonCount - 1) / 2 * minionDegreeSep;
        while (minionCount < summonCount) {
            yield return new WaitForSeconds(minionSpawnInterval);
            Vector2 spawnDir = Quaternion.AngleAxis(angleDev, Vector3.forward) * (EnemyHealth.player.transform.position - transform.position).normalized * spawnDistance;
            SpawnFallingMinion(spawnDir);
            minionCount += 1;
        }
        healthAndNav.inAttackSeq = false;
        this.enabled = false;
    }

    void SpawnFallingMinion(Vector2 groundVelocity) {
        GameObject fallingMinion = Instantiate(droppingMinionPrefab, transform.position, Quaternion.identity);
        fallingMinion.GetComponent<ThrownMinion>().Initialize(groundVelocity, 5);
    }
}
