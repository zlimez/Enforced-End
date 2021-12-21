using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CollisionAvoidance : SingleNodeBlocker
{
    void Start() {
        this.manager = GameObject.Find("BlockManager").GetComponent<BlockManager>();
    }
}
