using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpear : MeleeWeapon
{
    public Transform player;

    public override void Awake()
    {
        base.Awake();

        player = GameObject.Find("Gun").GetComponent<Transform>();
    }

    public override void attack()
    {
        Vector2 direction = attackPoint.position - transform.position;

        RaycastHit2D hit = Physics2D.BoxCast(attackPoint.position, new Vector2(0.2f, 0.2f), 0, direction, 20, LayerMask.GetMask("Player"));

        if (hit.collider != null && isAttack)
        {
            rigid.velocity = Vector2.zero;
            isAttack = false;

            

            StartCoroutine(AttackAndAvoid(direction));
        }

        if (!isAttack)
        {
            Vector2 dir = player.position - transform.position;

            float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            // 공격 방향으로 회전
        }

        attackTrail.enabled = isAttack ? false : true;
    }

    IEnumerator AttackAndAvoid(Vector2 dir)
    {
        rigid.AddForce(dir * moveSpeed, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.35f);

        rigid.velocity = Vector2.zero;
        isAttack = true;
        rigid.AddForce(-dir * (moveSpeed / 2), ForceMode2D.Impulse);
    }
}
