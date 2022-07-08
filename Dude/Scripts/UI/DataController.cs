using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SimpleJSON;

public class DataController : MonoBehaviour
{
    #region Singerton
    //�̱���
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
    [Header("ĳ���� �������� ����")]
    public int CharSelectIdx = 0;
    public string CharName = "Default";

    public GameObject MariaPref;
    public GameObject BradyPref;
    public GameObject ErikaPref;

    GameObject MinimapCameraPref;
    #endregion

    #region UIManager
    [Header("UIManager ���� ����(�ڵ�� ����)")]
    public GameObject UIManager; //�״� ���� �� �뵵
    public GameObject MariaSkillTreeUI;
    public GameObject ChaUI;
    public GameObject GameEndingUI;
    GameObject MainWindow;

    MariaChaUI mariaChaUI;
    public MainWindowManager mainWindowManager;

    MariaSkillTreeUI mariaSkillTreeUI;
    BradySkillTreeUI bradySkillTreeUI;
    ErikaSkillTreeUI erikaSkillTreeUI;

    //Key ȹ�� UI ����
    GameObject KeyAugmentExplainCanvas;
    Text T_KeyAugmentExplain;
    public bool isKeyUi = false;

    //DB ��ŷ ����
    Text timeChecktext;
    public float startTime { get; set; } = 0;
    public float clearTime { get; set; } = 0;

    public float stage1ClearTime { get; set; } = 0;
    public float duringStage1 { get; set; } = 0;
    public float stage2ClearTime { get; set; } = 0;
    public float duringStage2 { get; set; } = 0;
    public float stage3ClearTime { get; set; } = 0;
    public float duringStage3 { get; set; } = 0;


    //���� ��ŷ ����

    public int displayRankNo = 10;
    public float[] bestScore;
    public string[] bestName;
    public string[] bestChar;
    public float iniScore = 1000;



    [Header("Ending UI ����")]
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
    public float DsSpeed { get; set; } = 1; //�� �ֵθ��� �ӵ�

    //About WindScar
    public float WsDamage { get; set; } = 100;
    public float WsSpeed { get; set; } = 1; //�� �ֵθ��� �ӵ�
    public float WsPojSpeed { get; set; } = 1000; //����ü ���󰡴� �ӵ�
    public float WsDistance { get; set; } = 4;
    public int WsProjectile { get; set; } = 2; //2�� ����� Ŀ������

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
    [Header("��ų1 ��������Ʈ�ѷ� �޼ҵ�")]
    public float skill1_Damage = 100;
    public float skill1_Projectile = 2;//������ ��Ʈ�ѷ��� ���� ���� (¦��ȭ�� ����)
    public float skill1speed = 2500f;   //���ư��¼ӵ� �����ִ�ũ��   //��ų�� �ӵ� ��ȭ
    public float destoryarrow1 = 3f;// ���� �ӵ� = ������Ʈ �����ϱ����� ����   //��ų�� �����ð�

    [Header("��ų2 ��������Ʈ�ѷ� �޼ҵ�")]
    public float skill2_Damage = 100;
    public float skill2_Projectile = 0;//������ ��Ʈ�ѷ��� ���� ���� (¦��ȭ�� ����)0�̸� �Ѱ�������
    public float skill2speed = 2500f;   //���ư��¼ӵ� �����ִ�ũ�� //��ų�� �ӵ�����

    [Header("��ų3 ��������Ʈ�ѷ� �޼ҵ�")]
    public int HP_UP = 50;
    public int MP_UP = 20;

    [Header("��ų4 ��������Ʈ�ѷ� �޼ҵ�")]
    public float skill4_Damage = 100;
    public float skill4_Projectile = 2f;//������ ��Ʈ�ѷ��� ���� ���� (¦��ȭ�� ����)
    public float tr_distace = 4f;   //����̵� �����ð� ����

    #endregion


    void Start()
    {
        
        Init(); //DataController ���� üũ �� ����

        MinimapCameraPref = Resources.Load<GameObject>("MiniMapCamera");


        //UI ������Ʈ ����
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

        //��ũ��Ʈ ����
        mariaSkillTreeUI = UIManager.GetComponent<MariaSkillTreeUI>();
        bradySkillTreeUI = UIManager.GetComponent<BradySkillTreeUI>();
        erikaSkillTreeUI = UIManager.GetComponent<ErikaSkillTreeUI>();

        mariaChaUI = UIManager.GetComponent<MariaChaUI>();
        mainWindowManager = UIManager.GetComponent<MainWindowManager>();

        //GameEndingUI �� �ڽ� Panel �� �ڽ� Image �� �ڽ� Text ù��° ���
        PlayerRecordText = GameEndingUI.transform.Find("YourRecordPanel_1/Image/PlayerRecordText").GetComponent<Text>();
        RankBoardText = GameEndingUI.transform.Find("RankBoardPanel_2/Image/RankBoardText").GetComponent<Text>();
        RankBoardText1 = GameEndingUI.transform.Find("RankBoardPanel_2/Image/RankBoardText_1").GetComponent<Text>();
        RankBoardText2 = GameEndingUI.transform.Find("RankBoardPanel_2/Image/RankBoardText_2").GetComponent<Text>();
        RankBoardText3 = GameEndingUI.transform.Find("RankBoardPanel_2/Image/RankBoardText_3").GetComponent<Text>();

        //GameEndingUI �� �ڽ� Panel �� �ڽ� Button �ι�° ���
        RetryButton = GameEndingUI.transform.GetChild(3).gameObject.transform.GetChild(0).GetComponent<Button>();
        QuitButton = GameEndingUI.transform.GetChild(3).gameObject.transform.GetChild(1).GetComponent<Button>();

        //Button �� Method ����
        RetryButton.onClick.AddListener(ToCharSelectScene);
        QuitButton.onClick.AddListener(GameEndingUIQuit);

        //���� ��ŷ ����
        bestScore = new float[displayRankNo];
        bestName = new string[displayRankNo];
        bestChar = new string[displayRankNo];

        //Keyȹ�� ����
        KeyAugmentExplainCanvas = UIManager.transform.Find("KeyAugmentExplainCanvas").gameObject;
        KeyAugmentExplainCanvas.SetActive(false);
        T_KeyAugmentExplain = KeyAugmentExplainCanvas.transform.Find("BackgroundImage/Text").GetComponent<Text>();
        keyIdxInt = PlayerPrefs.GetInt("keyidx");
    }

    private void Update() //ĳ���� UI ���� ��� �ð� ǥ��
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
    //�� �Ѿ �� ���� �Լ� ����
    void OnEnable()
    {
        // ��������Ʈ ü�� �߰�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) //�� �Ѿ ������ 1���� ����
    {
        if (SceneManager.GetActiveScene().name != "StartScene" && SceneManager.GetActiveScene().name != "CharacterSelect")
        {
            InstChar(CharSelectIdx); //ĳ���ͼ��þ����� ĳ���� ���� ������ �ε��� �����ؼ� �ش� ĳ���� ����
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

        //���ξ��ΰ� �̸����� üũ�ؼ� �׶� ĳ����UI ���̰� ����.
        //�Ŀ� ĳ���� �߰��Ǹ� �Լ��� �ε��� �־ ������ ����.
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


    public void ToCharSelectScene() //ĳ��â, ������ �ʱ�ȭ
    {
        SceneManager.LoadScene("CharacterSelect");
        ChaUI.SetActive(false);
        GameEndingUI.SetActive(false);
        DataInitialization();

    }

    public void GetItemKey() //UI����, Ű ������ ����
    {
        stage3ClearTime = Time.time;
        duringStage3 = stage3ClearTime - stage2ClearTime;
        clearTime = Time.time - startTime;

        Cursor.lockState = CursorLockMode.None;
        isKeyUi = true;
        KeyAugmentExplainCanvas.SetActive(true);
        T_KeyAugmentExplain.text = "Ű�� ȹ���߽��ϴ�. \n" +
            "Ű�� 1�� ������ ������ ĳ���Ͱ� �رݵǰ� �߰��� 1���� �� ������ �ü� ĳ���Ͱ� �رݵ˴ϴ�.\n" +
            "�Ʒ� ��ư�� ���� ��ŷ ������ Ȯ���ϼ���.";

        //Ű ������ ����
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

        UIManager.GetComponent<MariaChaUI>().CurHp = UIManager.GetComponent<MariaChaUI>().IniHp; //ü��ȸ��
        UIManager.GetComponent<MariaChaUI>().CurMp = UIManager.GetComponent<MariaChaUI>().IniMp; //����ȸ��
        UIManager.GetComponent<MariaChaUI>().DisplayHealth();
        UIManager.GetComponent<MariaChaUI>().DisplayMana();

        mariaChaUI.H_Potion = 0; //���� ����, UI �ʱ�ȭ
        mariaChaUI.H_Text.text = mariaChaUI.H_Potion.ToString();
        mariaChaUI.M_Potion = 0;
        mariaChaUI.M_Text.text = mariaChaUI.M_Potion.ToString();

        //Maria initialization
        mariaSkillTreeUI.skillPoint1 = 0; //��ųƮ�� �ʱ�ȭ
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
        DsDamage = 100; //��ų ��ġ �ʱ�ȭ
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
        bradySkillTreeUI.skillPoint1 = 0; //��ųƮ�� �ʱ�ȭ
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
        FireBallDamage = 20; //��ų ��ġ �ʱ�ȭ
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
        erikaSkillTreeUI.skillPoint1 = 0; //��ųƮ�� �ʱ�ȭ
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
        skill1_Damage = 100; //��ų ��ġ �ʱ�ȭ
        skill1_Projectile = 2;//������ ��Ʈ�ѷ��� ���� ���� (¦��ȭ�� ����)
        skill1speed = 2500f;   //���ư��¼ӵ� �����ִ�ũ��   //��ų�� �ӵ� ��ȭ
        destoryarrow1 = 3f;// ���� �ӵ� = ������Ʈ �����ϱ����� ����   //��ų�� �����ð�
        skill2_Damage = 100;
        skill2_Projectile = 0;//������ ��Ʈ�ѷ��� ���� ���� (¦��ȭ�� ����)0�̸� �Ѱ�������
        skill2speed = 2500f;   //���ư��¼ӵ� �����ִ�ũ�� //��ų�� �ӵ�����
        HP_UP = 50;
        MP_UP = 20;
        skill4_Damage = 100;
        skill4_Projectile = 2f;//������ ��Ʈ�ѷ��� ���� ���� (¦��ȭ�� ����)
        tr_distace = 4f;   //����̵� �����ð� ����
    }

    static void Init() //DataController ���� üũ �� ����
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

    void InstChar(int Idx) //ĳ���� ����
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

        //�̴ϸ� ���� ī�޶� ����
        Instantiate(MinimapCameraPref, new Vector3(0, 20, 0), Quaternion.Euler(90, 0, 0));
        

    }

    //���� ��ŷ ����
    void ScoreSet(float currentScore, string currentName, string currentChar)
    {
        float tmpScore = 0;
        string tmpName = "default";
        string tmpChar = "default";

        for (int i = 0; i < displayRankNo; i++)
        {
            //���� ������ ��ŷ �������̶� �ʱⰪ�� ũ�� ����
            if (PlayerPrefs.GetFloat(i + "BestScore") == 0)
            {
                PlayerPrefs.SetFloat(i + "BestScore", iniScore);
            }

            //����� ��ŷ ���� ��������
            bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
            bestName[i] = PlayerPrefs.GetString(i + "BestName");
            bestChar[i] = PlayerPrefs.GetString(i + "BestChar");

            //���� ������ ��ŷ�� ���� �� ���� ��
            while (bestScore[i] > currentScore)
            {
                //�ڸ� �ٲٱ�
                tmpScore = bestScore[i];
                tmpName = bestName[i];
                tmpChar = bestChar[i];

                bestScore[i] = currentScore;
                bestName[i] = currentName;
                bestChar[i] = currentChar;

                //��ŷ�� ����
                PlayerPrefs.SetFloat(i + "BestScore", currentScore);
                PlayerPrefs.SetString(i + "BestName", currentName);
                PlayerPrefs.SetString(i + "BestChar", currentChar);

                //���� �ݺ��� ���� �غ�
                currentName = tmpName;
                currentScore = tmpScore;
                currentChar = tmpChar;
            }
        }

        //��ŷ�� ���� ����, �̸�, ĳ���� ����
        for (int i = 0; i < displayRankNo; i++)
        {
            PlayerPrefs.SetFloat(i + "BestScore", bestScore[i]);
            PlayerPrefs.SetString(i + "BestName", bestName[i]);
            PlayerPrefs.SetString(i + "BestChar", bestChar[i]);
        }
    }

    public void GameEndingRankView() //Ŭ����Ÿ�� ��� �� ��ŷ ǥ��
    {
        isKeyUi = false; //ȭ�� �����̴� bool ����, KeyUI ���������� �ǹ�
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
                //01 Maria 135.33 WonYoung ����(���ý���)
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
                //���� ������ ��ŷ �������̶� �ʱⰪ�� ũ�� ����
                if (PlayerPrefs.GetFloat(i + "BestScore") == 0)
                {
                    PlayerPrefs.SetFloat(i + "BestScore", iniScore);
                }

                //����� ��ŷ ���� ��������
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
    public void GameEndingUIQuit() //��ŸƮ������ ���ư���
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
