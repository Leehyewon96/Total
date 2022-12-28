using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Enemy2 : Enemies, Characters
{
    UnityEvent unityEvent = null;

    Animator animator = null;
    NavMeshAgent meshAgent = null;
    public GameObject destination = null;

    public Sword sword;

    bool isDead = false;

    void Start()
    {
        unityEvent = new UnityEvent();
        unityEvent.AddListener(Die);
        unityEvent.AddListener(DamageAnim);

        animator = GetComponent<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();

        maxHp = 50.0f;
        curHp = maxHp;
        damage = 10.0f;

        initPos = this.gameObject.transform.position;

        enemyType = Enemies.EnemyType.E2;

        sword.AddEnemy(this);
        GameManager.instance.CharacterList.Add(this);
    }

    void Update()
    {
        if (meshAgent == null)
            return;

        if (destination == null)
            return;

        if (!isDead)
            meshAgent.SetDestination(destination.transform.position);

        if (meshAgent.velocity.magnitude > 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    public void Restart()
    {
        isDead = false;
        curHp = maxHp;
        meshAgent.isStopped = false;
    }

    public void RoundEnd()
    {
        transform.position = initPos;
        animator.SetTrigger("GameEnd");
        meshAgent.isStopped = true;
    }

    public override void Attacked(Enemies.EnemyType type)
    {
        if (enemyType != type)
            return;

        curHp -= damage;
        unityEvent.Invoke();
    }

    void Die()
    {
        if (curHp <= 0)
        {
            if (animator == null)
                return;
            curHp = 0;
            isDead = true;
            GameManager.instance.isRoundEnd = true;

            StartCoroutine(GameEnd());
        }
    }

    void DamageAnim()
    {
        if (isDead)
            animator.SetTrigger("Die");
        else
            animator.SetTrigger("Hitted");
    }

    IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(2.0f);
        GameManager.instance.NotifyCharacters();
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Sword"))
        {

        }
    }*/
}
