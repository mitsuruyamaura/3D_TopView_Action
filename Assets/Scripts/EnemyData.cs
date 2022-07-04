using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public int id;
    public string name;
    public int maxHp;
    public int attackPower;

    public float moveSpeed;
    public float attackInterval;
    public AttackRangeType attackRangeType;
}