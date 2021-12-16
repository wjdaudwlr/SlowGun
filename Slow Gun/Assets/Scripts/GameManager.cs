using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("MainMenu")]
    [SerializeField]
    private GameObject mainMenuPanel;
    [SerializeField]
    private Text stageText;

    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject gameClearPanel;

    [SerializeField]
    private GameObject playerGun;
    [SerializeField]
    private StageController stageController;
    public bool IsGameStart { set; get; } = false;
    public bool IsGameOver { set; get; } = false;

    private static GameManager instance = null;

    private int currentStage = 1;
    public int stageEnemyNum;

    public bool testStage = false;

    void Awake()
    {
        if (null == instance)
        {
            //�� Ŭ���� �ν��Ͻ��� ź������ �� �������� instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
            instance = this;

            //�� ��ȯ�� �Ǵ��� �ı����� �ʰ� �Ѵ�.
            //gameObject�����ε� �� ��ũ��Ʈ�� ������Ʈ�μ� �پ��ִ� Hierarchy���� ���ӿ�����Ʈ��� ��������, 
            //���� �򰥸� ������ ���� this�� �ٿ��ֱ⵵ �Ѵ�.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //���� �� �̵��� �Ǿ��µ� �� ������ Hierarchy�� GameMgr�� ������ ���� �ִ�.
            //�׷� ��쿣 ���� ������ ����ϴ� �ν��Ͻ��� ��� ������ִ� ��찡 ���� �� ����.
            //�׷��� �̹� ���������� instance�� �ν��Ͻ��� �����Ѵٸ� �ڽ�(���ο� ���� GameMgr)�� �������ش�.
            Destroy(this.gameObject);
        }

        if (testStage) PlayerPrefs.SetInt("BestStage", 0);

        currentStage = PlayerPrefs.GetInt("BestStage");
    }

    //���� �Ŵ��� �ν��Ͻ��� ������ �� �ִ� ������Ƽ. static�̹Ƿ� �ٸ� Ŭ�������� ���� ȣ���� �� �ִ�.
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Start()
    {
        stageText.text = (currentStage + 1).ToString();

        StartCoroutine(TapToStart());
    }

    IEnumerator TapToStart()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GaemStart();

                yield break;
            }
            yield return null;
        }
    }

    private void GaemStart()
    {
        stageEnemyNum = stageController.stageEnemynum[currentStage];
        IsGameStart = true;
        mainMenuPanel.SetActive(false);
        playerGun.transform.position = new Vector3(-6, 0, 0);
        playerGun.SetActive(true);
        stageController.Stage(currentStage);
    }

    public void GaemOver()
    {
        gameOverPanel.SetActive(true);
        playerGun.SetActive(false);
        IsGameOver = true;
        IsGameStart = false;
    }

    public void GameClear()
    {
        gameClearPanel.SetActive(true);
        IsGameStart = false;
        currentStage++;
        PlayerPrefs.SetInt("BestStage", currentStage);
    }

    public void HomeBtn()
    {
        IsGameOver = false;
        playerGun.SetActive(false);
        mainMenuPanel.SetActive(true);
        currentStage = PlayerPrefs.GetInt("BestStage");
        stageText.text = (currentStage + 1).ToString();
        gameOverPanel.SetActive(false);
        gameClearPanel.SetActive(false);
        StartCoroutine(TapToStart());
    }
}
