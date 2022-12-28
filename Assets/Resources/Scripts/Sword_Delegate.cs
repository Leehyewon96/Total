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
                Debug.Log("�̹� �����ϴ� �Լ� �Դϴ�.");
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

        Debug.Log("�������� �ʴ� �Լ��̱� ������ ������ �� �����ϴ�.");
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
