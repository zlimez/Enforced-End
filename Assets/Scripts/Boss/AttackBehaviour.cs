using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour
{
    public abstract bool attack();
    public abstract string getAttackTag();
}
