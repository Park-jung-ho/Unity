using System.Collections;
using UnityEngine;

public class Snail : MonsterCore
{
    public GameObject Attack_VFX;
    public float timer;
    
    private float idleTime, patrolTime;
    private bool isAttacking;
    protected override void Init(float hp, float speed)
    {
        base.Init(hp, speed);
        
    }

    void Start()
    {
        Init(hp, speed);
    }

    public override void EnterState(MonsterState newState)
    {
        switch (state)
        {
            case MonsterState.IDLE:
                
                break;
            case MonsterState.PATROL:
                
                break;
            case MonsterState.TRACE:
                
                break;
            case MonsterState.ATTACK:
                
                break;
            case MonsterState.HIT:
                StartCoroutine(HitRoutine());
                break;
            case MonsterState.DEATH:
                StopAllCoroutines();
                hpBar.transform.parent.parent.gameObject.SetActive(false);
                break;
        }
        
    }

    public void CheckTrace()
    {
        var monsterDir = Vector3.right * moveDir;
        var playerDir = (transform.position - target.position).normalized;
        float dotVal = Vector3.Dot(monsterDir, playerDir);
        var canSeeTarget = dotVal < 0;
        if (targetDistance < traceDistance && canSeeTarget)
        {
            timer = 0;
            animator.SetBool("isRun",true);
            ChangeState(MonsterState.TRACE);
        }
    }
    

    public override void Idle()
    {
        timer += Time.deltaTime;
        if (timer >= idleTime)
        {
            timer = 0;
            moveDir = Random.Range(0, 2) == 0 ? -1 : 1;
            transform.localScale = new Vector3(-moveDir, 1, 1);
            hpBarScale();
            animator.SetBool("isRun",true);
            patrolTime = Random.Range(3f, 5f);
            ChangeState(MonsterState.PATROL);
        }

        CheckTrace();
    }

    public override void Patrol()
    {
        transform.position += Vector3.right * (moveDir * speed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= patrolTime)
        {
            timer = 0;
            animator.SetBool("isRun",false);
            idleTime = Random.Range(0.8f, 1.2f);
            ChangeState(MonsterState.IDLE);
        }

        CheckTrace();
    }

    public override void Trace()
    {
        var targetDir = (target.position - transform.position).normalized;
        transform.position += Vector3.right * targetDir.x * speed * Time.deltaTime;
        var scaleX = targetDir.x > 0 ? -1 : 1;
        transform.localScale = new Vector3(scaleX, 1, 1);
        hpBarScale();
        if (targetDistance <= attackDistance)
        {
            ChangeState(MonsterState.ATTACK);
        }
        if (targetDistance > traceDistance)
        {
            animator.SetBool("isRun",false);
            ChangeState(MonsterState.IDLE);
        }
    }

    public override void Attack()
    {
        if (!isAttacking) StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
        isAttacking = false;
        Attack_VFX.SetActive(false);
        ChangeState(MonsterState.TRACE);
    }

    public override void Hit()
    {
        
    }

    public void AnimationEvent_Attack()
    {
        Attack_VFX.SetActive(true);
    }
    

    IEnumerator HitRoutine()
    {
        animator.SetTrigger("Hit");
        yield return new WaitForSeconds(1.7f);
        timer = 0;
        animator.SetBool("isRun",false);
        idleTime = Random.Range(0.8f, 1.2f);
        
        ChangeState(MonsterState.IDLE);
    }

    void hpBarScale()
    {
        if (transform.localScale.x < 0) hpBar.transform.localScale = new Vector3(-1, 1, 1);
        else hpBar.transform.localScale = new Vector3(1, 1, 1);
    }
}
