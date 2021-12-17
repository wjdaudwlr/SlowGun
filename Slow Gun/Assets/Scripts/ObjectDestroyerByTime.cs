using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyerByTime : MonoBehaviour
{
    [SerializeField]
    private float destroyTime;

    private void Awake()
    {
        // destroyTime 시간이 지나면 오브젝트 파괴
        Destroy(gameObject, destroyTime);
    }
}
