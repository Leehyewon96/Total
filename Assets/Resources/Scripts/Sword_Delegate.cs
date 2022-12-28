using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Delegate : MonoBehaviour
{
    public delegate void NotifyEnemies(Enemies.EnemyType type);

    public event NotifyEnemies notifyEnemies;

    public void AddEnemy(NotifyEnemies notification)
    {
        foreach(NotifyEnemies n in notifyEnemies.GetInvocationList())
        {
            if(n == notification)
            {
                Debug.Log("이미 존재하는 함수 입니다.");
                return;
            }
        }

        notifyEnemies += notification;
    }

    public void RemoveEnemy(NotifyEnemies notification)
    {
        foreach(NotifyEnemies n in notifyEnemies.GetInvocationList())
        {
            if(n == notification)
            {
                notifyEnemies -= notification;
                return;
            }
        }

        Debug.Log("존재하지 않는 함수이기 때문에 제거할 수 없습니다.");
    }

    public void NotifyEnemy(Enemies.EnemyType type)
    {
        notifyEnemies.Invoke(type);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemies.EnemyType Type = other.gameObject.GetComponent<Enemies>().enemyType;
            NotifyEnemy(Type);
        }
    }

}
