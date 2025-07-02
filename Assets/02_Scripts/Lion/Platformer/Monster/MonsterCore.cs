using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public abstract class MonsterCore : MonoBehaviour, IDamageable
{
    public enum MonsterState 
    {
        IDLE,
        PATROL,
        TRACE,
        ATTACK,
        HIT,
        DEATH,
    }
    
    public ItemManager itemManager;
    
    public MonsterState state;
    public Animator animator;
    public float hp;
    public float currHp;
    public float speed;
    public float damage;
    
    [Header("UI")]
    public Image hpBar;
    
    public Transform target;
    public float traceDistance;
    public float attackDistance;
    protected float moveDir;
    protected float targetDistance;
    

    protected virtual void Init(float hp, float speed)
    {
        this.hp = hp;
        currHp = hp;
        this.speed = speed;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        itemManager = FindFirstObjectByType<ItemManager>();
    }

    void Update()
    {
        targetDistance = Vector3.Distance(transform.position, target.position);
        switch (state)
        {
            case MonsterState.IDLE:
                Idle();
                break;
            case MonsterState.PATROL:
                Patrol();
                break;
            case MonsterState.TRACE:
                Trace();
                break;
            case MonsterState.ATTACK:
                Attack();
                break;
        }
    }

    public virtual void EnterState(MonsterState newState)
    {
        
    }

    public abstract void Idle();
    public abstract void Patrol();
    public abstract void Trace();
    public abstract void Attack();
    public abstract void Hit();

    public virtual void ExitState(MonsterState newState)
    {
        
    }

    public void ChangeState(MonsterState newState)
    {
        if (state == newState) return;
        Debug.Log($"[{name}] {state} => {newState}");
        ExitState(state);
        state = newState;
        EnterState(state);
    }
    public void TakeDamage(float damage)
    {
        currHp -= damage;
        hpBar.fillAmount = currHp / hp;
        
        if (currHp <= 0)
        {
            Death();
        }
        else
        {
            ChangeState(MonsterState.HIT);
        }
    }

    public void Death()
    {
        ChangeState(MonsterState.DEATH);
        animator.SetTrigger("Death");
        int ranVal = Random.Range(1, 5);
        for (int i = 0; i < ranVal; i++)
        {
            itemManager.DropItem(transform.position);
        }
        // col.enabled = false;
        // rb.gravityScale = 0;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Return"))
        {
            moveDir *= -1;
            transform.localScale = new Vector3(-moveDir, 1, 1);
        }

        if (other.CompareTag("Player"))
        {
            Debug.Log($"[{name}] Attack {damage}damage => {other.name}");
            other.GetComponent<IDamageable>()?.TakeDamage(damage);
        }
    }

    
}
