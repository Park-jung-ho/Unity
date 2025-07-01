using System;
using UnityEngine;

public abstract class MonsterCore : MonoBehaviour
{
    public enum MonsterState 
    {
        IDLE,
        PATROL,
        TRACE,
        ATTACK,
        HIT,
    }
    public MonsterState state;
    public Animator animator;
    public float hp;
    public float speed;
    
    public Transform target;
    public float traceDistance;
    public float attackDistance;
    protected float moveDir;
    protected float targetDistance;
    

    protected virtual void Init(float hp, float speed)
    {
        this.hp = hp;
        this.speed = speed;
        target = GameObject.FindGameObjectWithTag("Player").transform;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Return"))
        {
            moveDir *= -1;
            transform.localScale = new Vector3(-moveDir, 1, 1);
        }
    }
}
