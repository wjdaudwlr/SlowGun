using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    public int[] stageEnemynum;

    public void Stage(int stage)
    {
        switch (stage)
        {
            case 0:
                Instantiate(enemies[0], new Vector2(6, 0), Quaternion.identity);
                break;
            case 1:
                Instantiate(enemies[1], new Vector2(6, 0), Quaternion.identity);
                break;
            case 2:
                Instantiate(enemies[2], new Vector2(6, 0), Quaternion.identity);
                break;
            case 3:
                Instantiate(enemies[0], new Vector2(6, 0), Quaternion.identity);
                Instantiate(enemies[0], new Vector2(6, 2), Quaternion.identity);
                break;
            case 4:
                Instantiate(enemies[0], new Vector2(6, 0), Quaternion.identity);
                Instantiate(enemies[1], new Vector2(6, 2), Quaternion.identity);
                Instantiate(enemies[2], new Vector2(6, -2), Quaternion.identity);
                break;
            case 5:
                Instantiate(enemies[3], new Vector2(6, 0), Quaternion.identity);
                break;
            case 6:
                Instantiate(enemies[3], new Vector2(6, 0), Quaternion.identity);
                Instantiate(enemies[3], new Vector2(6, 2), Quaternion.identity);

                break;
        } 
    }
}
