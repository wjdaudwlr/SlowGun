using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;        // 총알 프리펩 오브젝트
    [SerializeField]
    private Transform gunPointTransform;    // 총구 위치

    public GameObject dieEffect;           // 다이 이펙트

    [SerializeField]
    private float reboundPower;             // 총 반동 파워
    [SerializeField]
    private float slowTime;                 // 느려지는 시간

    private Rigidbody2D rigid;              // 물리 

    

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
            // 총알 생성
            GameObject clone = Instantiate(bulletPrefab, gunPointTransform.position, gunPointTransform.rotation);
            Bullet b = clone.GetComponent<Bullet>();
            // 총알 발사
            b.Shot(direction); 

            // 총알 방향의 반대반향으로 반동추가 
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
