using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Enemy
{
    [SerializeField]
    protected Transform attackPoint;
    [SerializeField]
    protected float moveSpeed;

    protected Rigidbody2D rigid;
    protected TrailRenderer attackTrail;

    protected bool isAttack = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        attackTrail = GetComponentInChildren<TrailRenderer>();
    }

    public virtual void FixedUpdate()
    {
        if (GameManager.Instance.IsPlayingGame == false) Destroy(gameObject);
        attack();
    }

    public virtual void attack()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            isAttack = true;
        }
        else if (collision.gameObject.tag.Equals("Player"))
        {
            GameManager.Instance.GaemOver();
        }
    }
}
