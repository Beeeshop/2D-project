using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeePatrolState : BaseState
{
    private Vector3 target;
    private Vector3 moveDir;

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        currentEnemy.anim.SetBool("walk", true);

        // ����Ѳ��״̬��ʱ��������һ��Ŀ���
        target = currentEnemy.GetNewPoint();
    }

    public override void LogicUpdate()
    {
        // ���� player �л��� chase 
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }

        if ((currentEnemy.transform.position - target).sqrMagnitude < 0.1f)
        {
            // ˵������Ŀ�����
            currentEnemy.wait = true;
            target = currentEnemy.GetNewPoint();
        }

        // �ƶ����� = Ŀ��λ�� - ��ǰλ��
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
        if (!currentEnemy.isHurt && !currentEnemy.isDead && !currentEnemy.wait)
        {
            currentEnemy.rb.velocity = moveDir * currentEnemy.currentSpeed;
        }
        else
        {
            // ������Ҫͣ����
            currentEnemy.rb.velocity = Vector2.zero;
        }
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("walk", false);
    }
}
