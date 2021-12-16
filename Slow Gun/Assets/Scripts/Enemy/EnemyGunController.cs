using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunController : Enemy
{
    [SerializeField]
    private GameObject bulletPrefab;        // �Ѿ� ������ ������Ʈ
    [SerializeField]
    private Transform gunPointTransform;    // �ѱ� ��ġ

    [SerializeField]
    private float reboundPower;             // �� �ݵ� �Ŀ�

    private Rigidbody2D rigid;              // ���� 

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
            // �Ѿ� �߻�
            b.Shot(direction);

            // �Ѿ� ������ �ݴ�������� �ݵ��߰� 
            rigid.AddForce(-direction * reboundPower, ForceMode2D.Impulse);
        }
    }

}
