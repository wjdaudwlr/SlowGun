using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;            // �Ѿ� ���ǵ�
    [SerializeField]
    private GameObject bulletEffect;    // �浹 ���� �� �����Ǵ� ����Ʈ

    private Rigidbody2D rigid;          // ����

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Shot(Vector2 dir)
    {
        rigid.AddForce(dir * moveSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag.Equals("Wall"))
        {
            Destroy(gameObject);
            Instantiate(bulletEffect, transform.position, Quaternion.identity);
        }
        else if (collision.gameObject.tag.Equals("Enemy") && GameManager.Instance.IsGameStart == true)
        {
            Destroy(collision.gameObject);

            GameManager.Instance.stageEnemyNum--;
            if (GameManager.Instance.stageEnemyNum == 0) GameManager.Instance.GaemOver();

            Instantiate(bulletEffect, transform.position, Quaternion.identity);
        }
    }
}
