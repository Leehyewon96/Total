using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public List<Enemies> EnemyList = new List<Enemies>();

    public void AddEnemy(Enemies enemy)
    {
        if (EnemyList.Contains(enemy))
        {
            Debug.Log("�̹� ����Ʈ�� �����ϴ� Enemy�Դϴ�.");
        }
        else
        {
            EnemyList.Add(enemy);
        }
    }

    public void RemoveEnemy(Enemies enemy)
    {
        if(EnemyList.Contains(enemy))
        {
            EnemyList.Remove(enemy);
        }
        else
        {
            Debug.Log("�������� �ʴ� Enemy�Դϴ�.");
        }
    }

    public void NotifyEnemies(Enemies.EnemyType type)
    {
        foreach(Enemies enemy in EnemyList)
        {
            enemy.Attacked(type);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemies.EnemyType Type = other.gameObject.GetComponent<Enemies>().enemyType;
            NotifyEnemies(Type);
        }
    }

}