using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BeeChaseState : BaseState
{
    private Attack attack;
    private Vector3 target;
    private Vector3 moveDir;
    private bool isAttack;
    private float attackRateCounter = 0;

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.anim.SetBool("run", true);
        attack = enemy.GetComponent<Attack>();

        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.lostTimeCounter <= 0)
        {
            currentEnemy.SwitchState(NPCState.Patrol);
        }

        // attacker.position 是指玩家脚底的位置，这不是我们想要的，所以在 y 轴上要加 1.5
        target = new Vector3(currentEnemy.attacker.position.x, currentEnemy.attacker.position.y + 1.5f, 0);

        // 判断攻击距离
        if ((target - currentEnemy.transform.position).magnitude <= attack.attackRange)
        {
            // 可以播放攻击动画了
            isAttack = true;

            // 先让蜜蜂停下来
            if (!currentEnemy.isHurt)
            {
                currentEnemy.rb.velocity = Vector2.zero;
            }

            // 计时器
            attackRateCounter -= Time.deltaTime;
            if (attackRateCounter <= 0)
            {
                currentEnemy.anim.SetTrigger("attack");
                attackRateCounter = attack.attackRate;
            }
        }
        else
        {
            // 超出攻击范围
            isAttack = false;
        }

        // 移动方向 = 目标位置 - 当前位置
        moveDir = (target - currentEnemy.transform.position).normalized;
        if (moveDir.x > 0)
        {
            currentEnemy.transform.localScale = new Vector3(
                -1 * Mathf.Abs(currentEnemy.transform.localScale.x),
                currentEnemy.transform.localScale.y,
                currentEnemy.transform.localScale.z
            );
        }
        else if (moveDir.x < 0)
        {
            currentEnemy.transform.localScale = new Vector3(
                Mathf.Abs(currentEnemy.transform.localScale.x),
                currentEnemy.transform.localScale.y,
                currentEnemy.transform.localScale.z
            );
        }
    }

    public override void PhysicsUpdate()
    {
        if (!currentEnemy.isHurt && 
            !currentEnemy.isDead && 
            !currentEnemy.wait &&
            !isAttack)
        {
            currentEnemy.rb.velocity = moveDir * currentEnemy.currentSpeed;
        }
        // 追击过程中是没有 wait 等待的
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("run", false);
    }
}
