using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    [Header("移动范围")]
    public float patrolRadius;

    protected override void Awake()
    {
        base.Awake();
        patrolState = new BeePatrolState();
        chaseState = new BeeChaseState();
    }

    public override bool FoundPlayer()
    {
        var obj = Physics2D.OverlapCircle(transform.position, checkDistance, attackLayer);
        if (obj)
        {
            attacker = obj.transform;
        }
        return obj;
    }

    public override void OawGizmosSelected()
    {
        // 画出警戒区大小
        Gizmos.DrawWireSphere(transform.position, checkDistance);

        // 画出巡逻范围大小
        Gizmos.color = Color.green;
        if (spwanPoint == Vector3.zero)
        {
            Gizmos.DrawWireSphere(transform.position, patrolRadius);
        }
        else
        {
            Gizmos.DrawWireSphere(spwanPoint, patrolRadius);
        }
    }

    public override Vector3 GetNewPoint()
    {
        // 算出随机角度
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);
        // 算出随机向量
        Vector2 randomVector2 = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
        // 算出随机半径
        float radius = Random.Range(0.0f, patrolRadius);

        return spwanPoint + (Vector3) (randomVector2 * radius);
    }

    public override void Move()
    {
        
    }
}
