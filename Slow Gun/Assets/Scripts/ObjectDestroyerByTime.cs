using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyerByTime : MonoBehaviour
{
    [SerializeField]
    private float destroyTime;

    private void Awake()
    {
        // destroyTime �ð��� ������ ������Ʈ �ı�
        Destroy(gameObject, destroyTime);
    }
}
