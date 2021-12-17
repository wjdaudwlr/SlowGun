using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Enemy
{
    [SerializeField]
    protected GameObject bulletPrefab;        // 총알 프리펩 오브젝트
    [SerializeField]
    protected Transform gunPointTransform;    // 총구 위치

    [SerializeField]
    protected float reboundPower;             // 총 반동 파워
    [SerializeField]
    protected float shotDelay;                // 발사 딜레이

    protected Rigidbody2D rigid;              // 물리 

    protected bool isAttack = true;
    protected float delayTime;                // 딜레이 타임

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
            // 총알 발사
            b.Shot(direction);

            // 총알 방향의 반대반향으로 반동추가 
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
