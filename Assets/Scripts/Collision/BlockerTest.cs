using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BlockerTest : MonoBehaviour
{
    // Start is called before the first frame update
    public SingleNodeBlocker blocker;
    void Start()
    {
        SingleNodeBlocker blocker = GetComponent<CollisionAvoidance>(); 
        Debug.Log(blocker.gameObject.name);
    }

    void Update() {
        blocker.Unblock();
        blocker.BlockAtCurrentPosition();  
    }
}
