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
    public bool IsPlayingGame { set; get; } = false;
    public bool IsGameOver { set; get; } = false;

    private static GameManager instance = null;

    private int currentStage = 1;
    public int stageEnemyNum;

    public bool testStage = false;

    void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            //gameObject만으로도 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트라는 뜻이지만, 
            //나는 헷갈림 방지를 위해 this를 붙여주기도 한다.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameMgr이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameMgr)을 삭제해준다.
            Destroy(this.gameObject);
        }

        if (testStage) PlayerPrefs.SetInt("BestStage", 0);

        currentStage = PlayerPrefs.GetInt("BestStage");
    }

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GaemStart();

                yield break;
            }
            yield return null;
        }
    }

  

    private void GaemStart()
    {
        StopCoroutine(TapToStart());
        stageEnemyNum = stageController.stageEnemynum[currentStage];
        IsPlayingGame = true;
        mainMenuPanel.SetActive(false);
        playerGun.transform.position = new Vector3(-6, Random.Range(-2f, 3f), 0);
        playerGun.transform.rotation = Quaternion.Euler(0, 0, 30);
        playerGun.SetActive(true);
        stageController.Stage(currentStage);
    }

    public void GaemOver()
    {
        if (!IsPlayingGame) return;

        Instantiate(playerGun.GetComponent<GunController>().dieEffect, playerGun.transform.position, Quaternion.identity);
        Time.timeScale = 1;
        gameOverPanel.SetActive(true);
        playerGun.SetActive(false);
        IsGameOver = true;
        IsPlayingGame = false;
    }

    public void GameClear()
    {
        if (!IsPlayingGame) return;

        Time.timeScale = 1;
        gameClearPanel.SetActive(true);
        IsPlayingGame = false;
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
