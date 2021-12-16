using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeEnemy : MeleeWeapon
{
    public float rotateSpeed;

    public override void attack()
    {
        Vector2 dir = attackPoint.position - transform.position;

        RaycastHit2D hit = Physics2D.BoxCast(attackPoint.position, new Vector2(0.35f, 0.35f), 0,
            dir, 20, LayerMask.GetMask("Player"));

        if (!isAttack) transform.Rotate(0, 0, Time.deltaTime * rotateSpeed, Space.Self);

        if (hit.collider != null && isAttack)
        {
            rigid.velocity = Vector2.zero;
            isAttack = false;
            rigid.AddForce(dir * moveSpeed, ForceMode2D.Impulse);
        }
        attackTrail.enabled = isAttack ? false : true;
    }

    

}
