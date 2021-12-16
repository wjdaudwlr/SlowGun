using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeEnemy : MeleeWeapon
{
    public float rotateSpeed;

    private void Start()
    {
            StartCoroutine(rot());

    }
    public override void attack()
    {
        Vector2 dir = attackPoint.position - transform.position;

        RaycastHit2D hit = Physics2D.BoxCast(attackPoint.position, new Vector2(0.2f, 0.2f), 0, dir, 20, LayerMask.GetMask("Player"));


        if (hit.collider != null && isAttack)
        {
            rigid.velocity = Vector2.zero;
            isAttack = false;
            rigid.AddForce(dir * moveSpeed, ForceMode2D.Impulse);

        }



        attackTrail.enabled = isAttack ? false : true;
    }


    IEnumerator rot()
    {
        yield return null;

        float time = Time.time + 2;

        while(time >= Time.time)
        {
            yield return null;
           transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotateSpeed));
        }
    }

}
