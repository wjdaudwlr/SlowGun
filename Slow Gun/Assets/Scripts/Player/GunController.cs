using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;        // �Ѿ� ������ ������Ʈ
    [SerializeField]
    private Transform gunPointTransform;    // �ѱ� ��ġ

    public GameObject dieEffect;           // ���� ����Ʈ

    [SerializeField]
    private float reboundPower;             // �� �ݵ� �Ŀ�
    [SerializeField]
    private float slowTime;                 // �������� �ð�

    private Rigidbody2D rigid;              // ���� 

    

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsPlayingGame == false) return;

        Vector2 direction = gunPointTransform.position - transform.position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetTimeScale(1);
            // �Ѿ� ����
            GameObject clone = Instantiate(bulletPrefab, gunPointTransform.position, gunPointTransform.rotation);
            Bullet b = clone.GetComponent<Bullet>();
            // �Ѿ� �߻�
            b.Shot(direction); 

            // �Ѿ� ������ �ݴ�������� �ݵ��߰� 
            rigid.AddForce(-direction * reboundPower, ForceMode2D.Impulse);
        }
        else if(Input.GetKeyDown(KeyCode.LeftShift)){       
            SetTimeScale(0.1f);
        }
    }


    public void SetTimeScale(float time)
    {
        Time.timeScale = time;
        Time.fixedDeltaTime = 0.02f * time;
    }
}
