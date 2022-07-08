using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SimpleJSON;

public class DataController : MonoBehaviour
{
    #region Singerton
    //싱글톤
    static DataController s_instance;
    public static DataController Instance
    {
        get
        {
            Init();
            return s_instance;
        }
    }
    #endregion


    #region CharInst
    [Header("캐릭터 동적생성 관련")]
    public int CharSelectIdx = 0;
    public string CharName = "Default";

    public GameObject MariaPref;
    public GameObject BradyPref;
    public GameObject ErikaPref;

    GameObject MinimapCameraPref;
    #endregion

    #region UIManager
    [Header("UIManager 접근 관련(코드로 제어)")]
    public GameObject UIManager; //켰다 껐다 할 용도
    public GameObject MariaSkillTreeUI;
    public GameObject ChaUI;
    public GameObject GameEndingUI;
    GameObject MainWindow;

    MariaChaUI mariaChaUI;
    public MainWindowManager mainWindowManager;

    MariaSkillTreeUI mariaSkillTreeUI;
    BradySkillTreeUI bradySkillTreeUI;
    ErikaSkillTreeUI erikaSkillTreeUI;

    //Key 획득 UI 변수
    GameObject KeyAugmentExplainCanvas;
    Text T_KeyAugmentExplain;
    public bool isKeyUi = false;

    //DB 랭킹 변수
    Text timeChecktext;
    public float startTime { get; set; } = 0;
    public float clearTime { get; set; } = 0;

    public float stage1ClearTime { get; set; } = 0;
    public float duringStage1 { get; set; } = 0;
    public float stage2ClearTime { get; set; } = 0;
    public float duringStage2 { get; set; } = 0;
    public float stage3ClearTime { get; set; } = 0;
    public float duringStage3 { get; set; } = 0;


    //로컬 랭킹 변수

    public int displayRankNo = 10;
    public float[] bestScore;
    public string[] bestName;
    public string[] bestChar;
    public float iniScore = 1000;



    [Header("Ending UI 관련")]
    public string curID = "Guest";
    public string keyIdx = "0"; //DB
    public int keyIdxInt = 0; //Local

    Text PlayerRecordText;

    public Text RankBoardText;
    public Text RankBoardText1;
    public Text RankBoardText2;
    public Text RankBoardText3;
    Button RetryButton;
    Button QuitButton;

    #endregion

    #region Maria
    //About DoubleSlash
    public float DsDamage { get; set; } = 100;
    public float DsSpeed { get; set; } = 1; //검 휘두르는 속도

    //About WindScar
    public float WsDamage { get; set; } = 100;
    public float WsSpeed { get; set; } = 1; //검 휘두르는 속도
    public float WsPojSpeed { get; set; } = 1000; //투사체 날라가는 속도
    public float WsDistance { get; set; } = 4;
    public int WsProjectile { get; set; } = 2; //2의 배수로 커져야함

    //About PetalSwirl
    public float PsDamage { get; set; } = 30;
    public float PsRotateSpeed { get; set; } = 300;
    public float PsAttackSpeed { get; set; } = 1000;
    public float PsDistance { get; set; } = 10;
    public float PsDuration { get; set; } = 3;
    public int PsNumOfProj { get; set; } = 3;

    //About BladeVortex
    public float BvDamage { get; set; } = 30;
    public float BvSpeed { get; set; } = 500;
    public float BvDuration { get; set; } = 10;
    public int BvNomOfProj { get; set; } = 1;

    #endregion

    #region Brady

    //About FireBall 
    public float FireBallDamage { get; set; } = 20;
    public float FireBallSplashDamage { get; set; } = 10;
    public float FireBall_Proj { get; set; } = 1;

    //About Meteo
    public float Meteo { get; set; } = 100;
    public float MeteoSplash { get; set; } = 100;
    public int Meteo_Proj { get; set; } = 1;

    //About Sheild
    public float sheildHp { get; set; } = 1000;
    public float sheild_Proj { get; set; } = 1;

    //About ChainLighting
    public float ChainDamage { get; set; } = 50;
    public float Chain_Proj { get; set; } = 1;
    public float Chain_Time { get; set; } = 2;


    #endregion

    #region Erika
    [Header("스킬1 데이터컨트롤러 메소드")]
    public float skill1_Damage = 100;
    public float skill1_Projectile = 2;//데이터 컨트롤러에 넣을 변수 (짝수화살 갯수)
    public float skill1speed = 2500f;   //날아가는속도 힘을주는크기   //스킬업 속도 변화
    public float destoryarrow1 = 3f;// 삭제 속도 = 오브젝트 유지하기위한 숫자   //스킬업 유지시간

    [Header("스킬2 데이터컨트롤러 메소드")]
    public float skill2_Damage = 100;
    public float skill2_Projectile = 0;//데이터 컨트롤러에 넣을 변수 (짝수화살 갯수)0이면 한개만나감
    public float skill2speed = 2500f;   //날아가는속도 힘을주는크기 //스킬업 속도증가

    [Header("스킬3 데이터컨트롤러 메소드")]
    public int HP_UP = 50;
    public int MP_UP = 20;

    [Header("스킬4 데이터컨트롤러 메소드")]
    public float skill4_Damage = 100;
    public float skill4_Projectile = 2f;//데이터 컨트롤러에 넣을 변수 (짝수화살 갯수)
    public float tr_distace = 4f;   //토네이도 유지시간 변수

    #endregion


    void Start()
    {
        
        Init(); //DataController 존재 체크 및 생성

        MinimapCameraPref = Resources.Load<GameObject>("MiniMapCamera");


        //UI 오브젝트 연결
        UIManager = transform.Find("UIManager").gameObject;
        UIManager.SetActive(true);

        MainWindow = UIManager.transform.Find("MainWindow").gameObject;
        ChaUI = UIManager.transform.Find("CharacterUICanvas").gameObject;
        GameEndingUI = UIManager.transform.Find("GameEndingUICanvas").gameObject;

        MariaSkillTreeUI = UIManager.transform.Find("MariaSkillTreeUICanvas").gameObject;

        MainWindow.SetActive(true);
        ChaUI.SetActive(false);
        GameEndingUI.SetActive(false);
        MariaSkillTreeUI.SetActive(false);

        timeChecktext = ChaUI.transform.Find("1_2_Panel/TimeRawImage/CurTimeText").GetComponent<Text>();

        //스크립트 연결
        mariaSkillTreeUI = UIManager.GetComponent<MariaSkillTreeUI>();
        bradySkillTreeUI = UIManager.GetComponent<BradySkillTreeUI>();
        erikaSkillTreeUI = UIManager.GetComponent<ErikaSkillTreeUI>();

        mariaChaUI = UIManager.GetComponent<MariaChaUI>();
        mainWindowManager = UIManager.GetComponent<MainWindowManager>();

        //GameEndingUI 밑 자식 Panel 밑 자식 Image 밑 자식 Text 첫번째 방법
        PlayerRecordText = GameEndingUI.transform.Find("YourRecordPanel_1/Image/PlayerRecordText").GetComponent<Text>();
        RankBoardText = GameEndingUI.transform.Find("RankBoardPanel_2/Image/RankBoardText").GetComponent<Text>();
        RankBoardText1 = GameEndingUI.transform.Find("RankBoardPanel_2/Image/RankBoardText_1").GetComponent<Text>();
        RankBoardText2 = GameEndingUI.transform.Find("RankBoardPanel_2/Image/RankBoardText_2").GetComponent<Text>();
        RankBoardText3 = GameEndingUI.transform.Find("RankBoardPanel_2/Image/RankBoardText_3").GetComponent<Text>();

        //GameEndingUI 밑 자식 Panel 밑 자식 Button 두번째 방법
        RetryButton = GameEndingUI.transform.GetChild(3).gameObject.transform.GetChild(0).GetComponent<Button>();
        QuitButton = GameEndingUI.transform.GetChild(3).gameObject.transform.GetChild(1).GetComponent<Button>();

        //Button 에 Method 연결
        RetryButton.onClick.AddListener(ToCharSelectScene);
        QuitButton.onClick.AddListener(GameEndingUIQuit);

        //로컬 랭킹 관련
        bestScore = new float[displayRankNo];
        bestName = new string[displayRankNo];
        bestChar = new string[displayRankNo];

        //Key획득 관련
        KeyAugmentExplainCanvas = UIManager.transform.Find("KeyAugmentExplainCanvas").gameObject;
        KeyAugmentExplainCanvas.SetActive(false);
        T_KeyAugmentExplain = KeyAugmentExplainCanvas.transform.Find("BackgroundImage/Text").GetComponent<Text>();
        keyIdxInt = PlayerPrefs.GetInt("keyidx");
    }

    private void Update() //캐릭터 UI 우측 상단 시간 표시
    {
        if (startTime != 0)
        {
            timeChecktext.text = string.Format("{0:N2}", Time.time - startTime);

        }
        else
        {
            timeChecktext.text = "Record";

        }
    }

    #region WheneverSceneLoad
    //씬 넘어갈 때 마다 함수 실행
    void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) //씬 넘어갈 때마다 1번씩 실행
    {
        if (SceneManager.GetActiveScene().name != "StartScene" && SceneManager.GetActiveScene().name != "CharacterSelect")
        {
            InstChar(CharSelectIdx); //캐릭터선택씬에서 캐릭터 선택 누르면 인덱스 변경해서 해당 캐릭터 생성
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (SceneManager.GetActiveScene().name != "StartScene")
        {
            MainWindow.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (SceneManager.GetActiveScene().name == "CharacterSelect")
        {
            Cursor.lockState = CursorLockMode.None;
        }

        //메인씬인거 이름으로 체크해서 그때 캐릭터UI 보이게 세팅.
        //후에 캐릭터 추가되면 함수에 인덱스 넣어서 조절할 예정.
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            ChaUI.SetActive(true);
            mariaChaUI.Char_Image();
        }

        if (SceneManager.GetActiveScene().name == "Stage1Scene")
        {
            startTime = Time.time;
        }

        if (SceneManager.GetActiveScene().name == "Stage2Scene")
        {
            stage1ClearTime = Time.time;
            duringStage1 = stage1ClearTime - startTime;
        }

        if (SceneManager.GetActiveScene().name == "Stage3Scene")
        {
            stage2ClearTime = Time.time;
            duringStage2 = stage2ClearTime - stage1ClearTime;
        }
    }
    #endregion


    public void ToCharSelectScene() //캐선창, 데이터 초기화
    {
        SceneManager.LoadScene("CharacterSelect");
        ChaUI.SetActive(false);
        GameEndingUI.SetActive(false);
        DataInitialization();

    }

    public void GetItemKey() //UI실행, 키 먹은거 적용
    {
        stage3ClearTime = Time.time;
        duringStage3 = stage3ClearTime - stage2ClearTime;
        clearTime = Time.time - startTime;

        Cursor.lockState = CursorLockMode.None;
        isKeyUi = true;
        KeyAugmentExplainCanvas.SetActive(true);
        T_KeyAugmentExplain.text = "키를 획득했습니다. \n" +
            "키를 1개 먹으면 마법사 캐릭터가 해금되고 추가로 1개를 더 먹으면 궁수 캐릭터가 해금됩니다.\n" +
            "아래 버튼을 눌러 랭킹 정보를 확인하세요.";

        //키 먹은거 적용
        if (mainWindowManager.guestLogin == true)
        {
            PlayerPrefs.SetInt("keyidx", keyIdxInt + 1);
            keyIdxInt = PlayerPrefs.GetInt("keyidx");
        }

        if (mainWindowManager.guestLogin == false)
        {
            UIManager.GetComponent<MainWindowManager>().AugmentKeyIdxMethod();

        }

    }

    public void DataInitialization()
    {
        startTime = 0;
        mainWindowManager.RankBoardText.text = "";
        mainWindowManager.RankBoardText1.text = "";
        mainWindowManager.RankBoardText2.text = "";
        mainWindowManager.RankBoardText3.text = "";

        UIManager.GetComponent<MariaChaUI>().CurHp = UIManager.GetComponent<MariaChaUI>().IniHp; //체력회복
        UIManager.GetComponent<MariaChaUI>().CurMp = UIManager.GetComponent<MariaChaUI>().IniMp; //마나회복
        UIManager.GetComponent<MariaChaUI>().DisplayHealth();
        UIManager.GetComponent<MariaChaUI>().DisplayMana();

        mariaChaUI.H_Potion = 0; //포션 개수, UI 초기화
        mariaChaUI.H_Text.text = mariaChaUI.H_Potion.ToString();
        mariaChaUI.M_Potion = 0;
        mariaChaUI.M_Text.text = mariaChaUI.M_Potion.ToString();

        //Maria initialization
        mariaSkillTreeUI.skillPoint1 = 0; //스킬트리 초기화
        mariaSkillTreeUI.skillPoint2 = 0;
        mariaSkillTreeUI.skillPoint3 = 0;
        mariaSkillTreeUI.skillPoint4 = 0;
        mariaSkillTreeUI.skill1_dmgLv = 1;
        mariaSkillTreeUI.skill2_dmgLv = 1;
        mariaSkillTreeUI.skill3_dmgLv = 1;
        mariaSkillTreeUI.skill4_dmgLv = 1;
        mariaSkillTreeUI.skill1_speedLv = 1;
        mariaSkillTreeUI.skill2_speedLv = 1;
        mariaSkillTreeUI.skill3_durationLv = 1;
        mariaSkillTreeUI.skill4_durationLv = 1;
        mariaSkillTreeUI.skill2_no_ProjLv = 1;
        mariaSkillTreeUI.skill3_no_ProjLv = 1;
        mariaSkillTreeUI.skill4_no_ProjLv = 1;
        mariaSkillTreeUI.skill2_ProjSpeedLv = 1;
        mariaSkillTreeUI.skill3_ProjSpeedLv = 1;
        mariaSkillTreeUI.skill2_ProjDistanceLv = 1;
        mariaSkillTreeUI.skill3_ProjDistanceLv = 1;
        DsDamage = 100; //스킬 수치 초기화
        DsSpeed = 1;
        WsDamage = 100;
        WsSpeed = 1;
        WsPojSpeed = 1000;
        WsDistance = 4;
        WsProjectile = 2;
        PsDamage = 30;
        PsAttackSpeed = 1000;
        PsDistance = 10;
        PsDuration = 3;
        PsNumOfProj = 3;
        BvDamage = 30;
        BvSpeed = 500;
        BvDuration = 10;
        BvNomOfProj = 1;

        //Brady initialization
        bradySkillTreeUI.skillPoint1 = 0; //스킬트리 초기화
        bradySkillTreeUI.skillPoint2 = 0;
        bradySkillTreeUI.skillPoint3 = 0;
        bradySkillTreeUI.skillPoint4 = 0;
        bradySkillTreeUI.skill1_DmgLv = 1;
        bradySkillTreeUI.skill1_ProjLv = 1;
        bradySkillTreeUI.skill1_DurLv = 1;
        bradySkillTreeUI.skill2_DmgLv = 1;
        bradySkillTreeUI.skill2_ProjLv = 1;
        bradySkillTreeUI.skill2_DurLv = 1;
        bradySkillTreeUI.skill3_DmgLv = 1;
        bradySkillTreeUI.skill3_ProjLv = 1;
        bradySkillTreeUI.skill4_DmgLv = 1;
        bradySkillTreeUI.skill4_ProjLv = 1;
        bradySkillTreeUI.skill4_DurLv = 1;
        FireBallDamage = 20; //스킬 수치 초기화
        FireBallSplashDamage = 10;
        FireBall_Proj = 1;
        Meteo = 100;
        MeteoSplash = 100;
        Meteo_Proj = 1;
        sheildHp = 1000;
        sheild_Proj = 1;
        ChainDamage = 50;
        Chain_Proj = 1;
        Chain_Time = 2;

        //Erika initialization
        erikaSkillTreeUI.skillPoint1 = 0; //스킬트리 초기화
        erikaSkillTreeUI.skillPoint2 = 0;
        erikaSkillTreeUI.skillPoint3 = 0;
        erikaSkillTreeUI.skillPoint4 = 0;
        erikaSkillTreeUI.skill1_dmgLv = 1;
        erikaSkillTreeUI.skill2_dmgLv = 1;
        erikaSkillTreeUI.skill3_HPLv = 1;
        erikaSkillTreeUI.skill4_dmgLv = 1;
        erikaSkillTreeUI.skill1_NoProj1Lv = 1;
        erikaSkillTreeUI.skill2_NoProj2Lv = 1;
        erikaSkillTreeUI.skill3_MPLv = 1;
        erikaSkillTreeUI.skill4_NoProj4Lv = 1;
        erikaSkillTreeUI.skill1_SpeedLv = 1;
        erikaSkillTreeUI.skill2_SpeedLv = 1;
        erikaSkillTreeUI.skill4_ProjDistance4Lv = 1;
        erikaSkillTreeUI.skill1_ProjDistance1Lv = 1;
        skill1_Damage = 100; //스킬 수치 초기화
        skill1_Projectile = 2;//데이터 컨트롤러에 넣을 변수 (짝수화살 갯수)
        skill1speed = 2500f;   //날아가는속도 힘을주는크기   //스킬업 속도 변화
        destoryarrow1 = 3f;// 삭제 속도 = 오브젝트 유지하기위한 숫자   //스킬업 유지시간
        skill2_Damage = 100;
        skill2_Projectile = 0;//데이터 컨트롤러에 넣을 변수 (짝수화살 갯수)0이면 한개만나감
        skill2speed = 2500f;   //날아가는속도 힘을주는크기 //스킬업 속도증가
        HP_UP = 50;
        MP_UP = 20;
        skill4_Damage = 100;
        skill4_Projectile = 2f;//데이터 컨트롤러에 넣을 변수 (짝수화살 갯수)
        tr_distace = 4f;   //토네이도 유지시간 변수
    }

    static void Init() //DataController 존재 체크 및 생성
    {
        if (s_instance == null)
        {
            GameObject obj = GameObject.Find("DataController");
            if (obj == null)
            {
                obj = new GameObject { name = "DataController" };
                obj.AddComponent<DataController>();
            }

            DontDestroyOnLoad(obj);
            s_instance = obj.GetComponent<DataController>();
        }
    }

    void InstChar(int Idx) //캐릭터 생성
    {
        if (Idx == 0)
        {
            CharName = "Maria";
            Instantiate(MariaPref, Vector3.zero, Quaternion.identity);
        }

        else if (Idx == 1)
        {
            CharName = "Brady";
            Instantiate(BradyPref, Vector3.zero, Quaternion.identity);
        }

        else if (Idx == 2)
        {
            CharName = "Erika";
            Instantiate(ErikaPref, Vector3.zero, Quaternion.identity);
        }

        //미니맵 위한 카메라 생성
        Instantiate(MinimapCameraPref, new Vector3(0, 20, 0), Quaternion.Euler(90, 0, 0));
        

    }

    //로컬 랭킹 관련
    void ScoreSet(float currentScore, string currentName, string currentChar)
    {
        float tmpScore = 0;
        string tmpName = "default";
        string tmpChar = "default";

        for (int i = 0; i < displayRankNo; i++)
        {
            //숫자 작은게 랭킹 순위권이라 초기값을 크게 적용
            if (PlayerPrefs.GetFloat(i + "BestScore") == 0)
            {
                PlayerPrefs.SetFloat(i + "BestScore", iniScore);
            }

            //저장된 랭킹 정보 가져오기
            bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
            bestName[i] = PlayerPrefs.GetString(i + "BestName");
            bestChar[i] = PlayerPrefs.GetString(i + "BestChar");

            //현재 점수가 랭킹에 오를 수 있을 때
            while (bestScore[i] > currentScore)
            {
                //자리 바꾸기
                tmpScore = bestScore[i];
                tmpName = bestName[i];
                tmpChar = bestChar[i];

                bestScore[i] = currentScore;
                bestName[i] = currentName;
                bestChar[i] = currentChar;

                //랭킹에 저장
                PlayerPrefs.SetFloat(i + "BestScore", currentScore);
                PlayerPrefs.SetString(i + "BestName", currentName);
                PlayerPrefs.SetString(i + "BestChar", currentChar);

                //다음 반복을 위한 준비
                currentName = tmpName;
                currentScore = tmpScore;
                currentChar = tmpChar;
            }
        }

        //랭킹에 맞춰 점수, 이름, 캐릭터 저장
        for (int i = 0; i < displayRankNo; i++)
        {
            PlayerPrefs.SetFloat(i + "BestScore", bestScore[i]);
            PlayerPrefs.SetString(i + "BestName", bestName[i]);
            PlayerPrefs.SetString(i + "BestChar", bestChar[i]);
        }
    }

    public void GameEndingRankView() //클리어타임 계산 및 랭킹 표현
    {
        isKeyUi = false; //화면 움직이는 bool 세팅, KeyUI 꺼져있음을 의미
        Cursor.lockState = CursorLockMode.None;



        GameEndingUI.SetActive(true);
        KeyAugmentExplainCanvas.SetActive(false);

        PlayerRecordText.text = "";
        PlayerRecordText.text =
            "Character : " + CharName +
            "\n\nYour ID : " + curID +
            "\n\nStage1 : " + string.Format("{0:N2}", duringStage1) +
            "\nStage2 : " + string.Format("{0:N2}", duringStage2) +
            "\nStage3 : " + string.Format("{0:N2}", duringStage3) +
            "\nClear Time : " + string.Format("{0:N2}", clearTime);

        if (mainWindowManager.guestLogin == false)
        {
            UIManager.GetComponent<MainWindowManager>().CreateLoadRecord();

        }

        else if (mainWindowManager.guestLogin == true)
        {

            ScoreSet(clearTime, curID, CharName);

            mainWindowManager.RankBoardText.text = "";
            mainWindowManager.RankBoardText1.text = "";
            mainWindowManager.RankBoardText2.text = "";
            mainWindowManager.RankBoardText3.text = "";

            for (int i = 0; i < displayRankNo; i++)
            {
                //01 Maria 135.33 WonYoung 형식(로컬시절)
                if (bestScore[i] != iniScore)
                {
                    RankBoardText.text += string.Format("{0:00}", i + 1) + "\n";
                    RankBoardText1.text += bestName[i] + "\n";
                    RankBoardText2.text += bestChar[i] + "\n";
                    RankBoardText3.text += string.Format("{0:N2}", bestScore[i]) + "\n";

                }
            }
        }


    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        DataInitialization();
        GameEndingUI.SetActive(true);

        PlayerRecordText.text = "";

        if (mainWindowManager.guestLogin == false)
        {
            UIManager.GetComponent<MainWindowManager>().LoadRecord();
        }

        else if (mainWindowManager.guestLogin == true)
        {
            mainWindowManager.RankBoardText.text = "";
            mainWindowManager.RankBoardText1.text = "";
            mainWindowManager.RankBoardText2.text = "";
            mainWindowManager.RankBoardText3.text = "";

            for (int i = 0; i < displayRankNo; i++)
            {
                //숫자 작은게 랭킹 순위권이라 초기값을 크게 적용
                if (PlayerPrefs.GetFloat(i + "BestScore") == 0)
                {
                    PlayerPrefs.SetFloat(i + "BestScore", iniScore);
                }

                //저장된 랭킹 정보 가져오기
                bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
                bestName[i] = PlayerPrefs.GetString(i + "BestName");
                bestChar[i] = PlayerPrefs.GetString(i + "BestChar");

                if (bestScore[i] != iniScore)
                {
                    RankBoardText.text += string.Format("{0:00}", i + 1) + "\n";
                    RankBoardText1.text += bestName[i] + "\n";
                    RankBoardText2.text += bestChar[i] + "\n";
                    RankBoardText3.text += string.Format("{0:N2}", bestScore[i]) + "\n";

                }
            }

        }
    }
    public void GameEndingUIQuit() //스타트씬으로 돌아가기
    {
        GameEndingUI.SetActive(false);
        SceneManager.LoadScene("StartScene");
        DataInitialization();
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
