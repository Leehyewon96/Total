using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemies : MonoBehaviour
{
    public enum EnemyType { E1, E2, E3}
    public float maxHp;
    public float curHp;

    public EnemyType enemyType;
    protected float damage = 10.0f;
    protected Vector3 initPos;

    public abstract void Attacked(Enemies.EnemyType type);

}
