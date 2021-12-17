using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Enemy
{
    [SerializeField]
    protected GameObject bulletPrefab;        // �Ѿ� ������ ������Ʈ
    [SerializeField]
    protected Transform gunPointTransform;    // �ѱ� ��ġ

    [SerializeField]
    protected float reboundPower;             // �� �ݵ� �Ŀ�
    [SerializeField]
    protected float shotDelay;                // �߻� ������

    protected Rigidbody2D rigid;              // ���� 

    protected bool isAttack = true;
    protected float delayTime;                // ������ Ÿ��

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public virtual void FixedUpdate()
    {
        if (GameManager.Instance.IsPlayingGame == false) Destroy(gameObject);

        Vector2 direction = gunPointTransform.position - transform.position;

        RaycastHit2D hit = Physics2D.BoxCast(gunPointTransform.position, new Vector2(0.10f, 0.10f), 0, direction, 20, LayerMask.GetMask("Player"));

        if (hit.collider != null && isAttack)
        {
            GameObject clone = Instantiate(bulletPrefab, gunPointTransform.position, gunPointTransform.rotation);
            EnemyBullet b = clone.GetComponent<EnemyBullet>();
            // �Ѿ� �߻�
            b.Shot(direction);

            // �Ѿ� ������ �ݴ�������� �ݵ��߰� 
            rigid.AddForce(-direction * reboundPower, ForceMode2D.Impulse);
            isAttack = false;
        }

        if (!isAttack)
        {
            delayTime += Time.deltaTime;
            if(delayTime >= shotDelay)
            {
                delayTime = 0;
                isAttack = true;
            }
        }

    }
}
