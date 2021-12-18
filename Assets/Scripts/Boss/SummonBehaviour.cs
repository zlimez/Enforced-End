using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SummonBehaviour : AttackBehaviour
{
    // Start is called before the first frame update
    public GameObject droppingMinionPrefab;
    public int summonCount = 1;
    public float minionSpawnInterval = 2.0f;
    // minions are spawned with boss at the centre concentrated in the sector nearest to player
    public float spawnDistance = 4f; 
    public float minionDegreeSep = 10f;
    public bool attacked = false;
    private EnemyHealth healthAndNav;
    private BossBehaviour boss;
    public static GridGraph gg;
    void Awake()
    {
        healthAndNav = GetComponent<EnemyHealth>();
        boss = GetComponent<BossBehaviour>();
    }

    void Start() {
        gg = AstarPath.active.data.gridGraph;
        Debug.Log("Graph is " + gg);
    }

    override public bool attack() {
        healthAndNav.inAttackSeq = true;
        StartCoroutine(SpawnMinions());
        // SpawnMinion();
        return true;
    }

    public override string getAttackTag()
    {
        return "Summon";
    }

    // void SpawnMinion() {
    //     // start animation
    //     boss.animator.SetTrigger("SpawnMinion");
    //     float angleDev = summonCount % 2 == 0 ? summonCount / 2 * -minionDegreeSep : (summonCount - 1) / 2 * minionDegreeSep;
    //     Vector2 spawnDir = Quaternion.AngleAxis(angleDev, Vector3.forward) * (EnemyHealth.player.transform.position - transform.position).normalized * spawnDistance;
    //     SpawnFallingMinion(spawnDir);
    //     healthAndNav.inAttackSeq = false;
    //     boss.attackCompleted = true;
    // }

    IEnumerator SpawnMinions() {
        // start animation
        boss.animator.SetTrigger("SpawnMinion");
        int minionCount = 0;
        float angleDev = summonCount % 2 == 0 ? summonCount / 2 * -minionDegreeSep : (summonCount - 1) / 2 * -minionDegreeSep;
        while (minionCount < summonCount) {
            yield return new WaitForSeconds(minionSpawnInterval);
            Vector2 spawnDir = Quaternion.AngleAxis(angleDev, Vector3.forward) * (healthAndNav.player.transform.position - transform.position).normalized * spawnDistance;
            if (gg.GetNode(Mathf.RoundToInt(spawnDir.x), Mathf.RoundToInt(spawnDir.y)).Walkable) {
                SpawnFallingMinion(spawnDir);
                minionCount += 1;
            }
            angleDev += minionDegreeSep;
        }
        healthAndNav.inAttackSeq = false;
        boss.attackCompleted = true;
        yield return null;
    }

    void SpawnFallingMinion(Vector2 groundVelocity) {
        GameObject fallingMinion = Instantiate(droppingMinionPrefab, transform.position, Quaternion.identity);
        fallingMinion.GetComponent<ThrownMinion>().Initialize(groundVelocity, 5);
    }
}
