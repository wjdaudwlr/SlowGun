using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunController : Enemy
{
    [SerializeField]
    private GameObject bulletPrefab;        // 총알 프리펩 오브젝트
    [SerializeField]
    private Transform gunPointTransform;    // 총구 위치

    [SerializeField]
    private float reboundPower;             // 총 반동 파워

    private Rigidbody2D rigid;              // 물리 

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.IsGameStart == false) Destroy(gameObject);
        
        Vector2 direction = gunPointTransform.position - transform.position;

        RaycastHit2D hit = Physics2D.BoxCast(gunPointTransform.position, new Vector2(0.2f, 0.2f), 0, direction, 20, LayerMask.GetMask("Player"));

        if (hit.collider != null)
        {
            GameObject clone = Instantiate(bulletPrefab, gunPointTransform.position, gunPointTransform.rotation);
            EnemyBullet b = clone.GetComponent<EnemyBullet>();
            // 총알 발사
            b.Shot(direction);

            // 총알 방향의 반대반향으로 반동추가 
            rigid.AddForce(-direction * reboundPower, ForceMode2D.Impulse);
        }
    }

}
