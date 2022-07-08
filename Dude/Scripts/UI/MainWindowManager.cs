using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.EventSystems;

public class MainWindowManager : MonoBehaviour
{
    EventSystem system;
    Selectable firstInput;

    GameObject LoginUI;
    InputField IDInputField;
    InputField PassInputField;
    Button Login_btn;
    Button Create_btn;
    
    Button Guest_btn;
    public bool guestLogin = false;


    string LoginUrl;
    string CreateUrl;
    string LoadRecordUrl;
    string CreateRecordUrl;
    string CreateLoadRecordUrl;
    string AugmentKeyIdxUrl;

    public Text RankBoardText;
    public Text RankBoardText1;
    public Text RankBoardText2;
    public Text RankBoardText3;
    void Start()
    {
        //������Ʈ ����
        LoginUI = this.transform.Find("MainWindow/Login_UI").gameObject;
        Login_btn = LoginUI.transform.Find("Login_btn").GetComponent<Button>();
        Login_btn.onClick.AddListener(LoginBtn);
        Create_btn = LoginUI.transform.Find("Create_btn").GetComponent<Button>();
        Create_btn.onClick.AddListener(CreateAccountBtn);
        Guest_btn = LoginUI.transform.Find("Guest_btn").GetComponent<Button>();
        Guest_btn.onClick.AddListener(GuestLogin);

        IDInputField = LoginUI.transform.Find("ID_Window").GetComponent<InputField>();
        PassInputField = LoginUI.transform.Find("PW_Window").GetComponent<InputField>();

        system = EventSystem.current;
        firstInput = IDInputField.GetComponent<Selectable>();
        // ó���� �̸��� Input Field�� �����ϵ��� �Ѵ�.
        firstInput.Select();

        //DB����
        //�α��ο� ���̴� php��
        LoginUrl = "127.0.0.1/dude/Login.php";
        //��������(�ű԰���)�� ���̴� php �� ���� �ٸ� php
        CreateUrl = "127.0.0.1/dude/Join.php";

        LoadRecordUrl = "127.0.0.1/dude/LoadRecord.php";
        CreateRecordUrl = "127.0.0.1/dude/CreateRecord.php";
        CreateLoadRecordUrl = "127.0.0.1/dude/CreateLoadRecord.php";
        AugmentKeyIdxUrl = "127.0.0.1/dude/AugmentKeyIdx.php";

        RankBoardText = transform.Find("GameEndingUICanvas/RankBoardPanel_2/Image/RankBoardText").GetComponent<Text>();
        RankBoardText1 = transform.Find("GameEndingUICanvas/RankBoardPanel_2/Image/RankBoardText_1").GetComponent<Text>();
        RankBoardText2 = transform.Find("GameEndingUICanvas/RankBoardPanel_2/Image/RankBoardText_2").GetComponent<Text>();
        RankBoardText3 = transform.Find("GameEndingUICanvas/RankBoardPanel_2/Image/RankBoardText_3").GetComponent<Text>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            // Tab + LeftShift�� ���� Selectable ��ü�� ����
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (next != null)
            {
                next.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Tab�� �Ʒ��� Selectable ��ü�� ����
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            // ����Ű�� ġ�� �α��� (����) ��ư�� Ŭ��
            Login_btn.onClick.Invoke();
        }
    }

    #region Login
    public void LoginBtn()
    {
        StartCoroutine(LoginCo());
    }

    IEnumerator LoginCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("Input_user", IDInputField.text);
        form.AddField("Input_pass", PassInputField.text);
        
        WWW webRequest = new WWW(LoginUrl, form);
        yield return webRequest;

        if (string.IsNullOrEmpty(webRequest.error))
        {
            //DisplayJSON(webRequest.text);
            var N = JSON.Parse(webRequest.text);
            var Array = N["results"];

            if (Array.Count > 0)
            {
                for (int i = 0; i < Array.Count; i++)
                {
                    string pass = Array[i]["PASS"].Value;
                    if (PassInputField.text == pass)
                    {
                        LoginUI.SetActive(false);
                        DataController.Instance.curID = IDInputField.text;
                        DataController.Instance.keyIdx = Array[i]["KEY"].Value;
                    }

                    else
                        Debug.Log("�н����尡 Ʋ���ϴ�.");
                }
            }
            else
                Debug.Log("�Է��� ���̵� �������� �ʽ��ϴ�.");
        }

    }
    void DisplayJSON(string _jsonData)
    {
        var N = JSON.Parse(_jsonData);
        var Array = N["results"];

        if (Array.Count > 0)
        {
            for (int i = 0; i < Array.Count; i++)
            {
                string pass = Array[i]["PASS"].Value;
                if (PassInputField.text == pass)
                {
                    Debug.Log("�α��� ����");
                    LoginUI.SetActive(false);
                }

                else
                    Debug.Log("�н����尡 Ʋ���ϴ�.");
            }
        }
        else
            Debug.Log("�Է��� ���̵� �������� �ʽ��ϴ�.");
    }

    void GuestLogin()
    {
        LoginUI.SetActive(false);
        guestLogin = true;
        DataController.Instance.keyIdxInt = PlayerPrefs.GetInt("keyidx");

    }


    #endregion

    #region Create
    public void CreateAccountBtn()
    {
        StartCoroutine(CreateCo());
    }


    IEnumerator CreateCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("Input_user", IDInputField.text);
        form.AddField("Input_pass", PassInputField.text);

        WWW webRequest = new WWW(CreateUrl, form);
        yield return webRequest;

        Debug.Log(webRequest.text);

        yield return null;
    }
    #endregion

    #region CreateRecord
    public void CreateRecord()
    {
        StartCoroutine(CreateRecordCo());
    }


    IEnumerator CreateRecordCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("Input_user", DataController.Instance.curID);
        form.AddField("Input_cha", DataController.Instance.CharName);
        form.AddField("Input_stage1", DataController.Instance.duringStage1.ToString());
        form.AddField("Input_stage2", DataController.Instance.duringStage2.ToString());
        form.AddField("Input_stage3", DataController.Instance.duringStage3.ToString());
        form.AddField("Input_clear", DataController.Instance.clearTime.ToString());
        

        WWW webRequest = new WWW(CreateRecordUrl, form);
        yield return webRequest;

        Debug.Log(webRequest.text);

        yield return null;
    }

    #endregion

    #region LoadRecord
    public void LoadRecord()
    {
        StartCoroutine(LoadRecordCo());
    }
    IEnumerator LoadRecordCo()
    {
        WWWForm form = new WWWForm();
        
        WWW webRequest = new WWW(LoadRecordUrl, form);
        yield return webRequest;

        if (string.IsNullOrEmpty(webRequest.error))
        {
            var N = JSON.Parse(webRequest.text);
            var Array = N["results"];

            if (Array.Count > 0)
            {
                for (int i = 0; i < Array.Count; i++)
                {
                    RankBoardText.text += Array[i]["NO"].Value + "\n";
                    RankBoardText1.text += Array[i]["ID"].Value + "\n";
                    RankBoardText2.text += Array[i]["CHA"].Value + "\n";
                    RankBoardText3.text += Array[i]["CLEAR"].Value + "\n";
                }
            }
            else
                Debug.Log("��ŷ ������ ���� ��");
        }

    }



    #endregion

    #region CreateLoadRecord
    public void CreateLoadRecord()
    {
        StartCoroutine(CreateLoadRecordCo());
    }


    IEnumerator CreateLoadRecordCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("Input_user", DataController.Instance.curID);
        form.AddField("Input_cha", DataController.Instance.CharName);
        form.AddField("Input_stage1", DataController.Instance.duringStage1.ToString());
        form.AddField("Input_stage2", DataController.Instance.duringStage2.ToString());
        form.AddField("Input_stage3", DataController.Instance.duringStage3.ToString());
        form.AddField("Input_clear", DataController.Instance.clearTime.ToString());

        WWW webRequest = new WWW(CreateLoadRecordUrl, form);
        yield return webRequest;

        if (string.IsNullOrEmpty(webRequest.error))
        {
            var N = JSON.Parse(webRequest.text);
            var Array = N["results"];

            if (Array.Count > 0)
            {
                for (int i = 0; i < Array.Count; i++)
                {
                    RankBoardText.text += Array[i]["NO"].Value + "\n";
                    RankBoardText1.text += Array[i]["ID"].Value + "\n";
                    RankBoardText2.text += Array[i]["CHA"].Value + "\n";
                    RankBoardText3.text += Array[i]["CLEAR"].Value + "\n";
                }
            }
            else
                Debug.Log("��ŷ ������ ���� ��");
        }
        yield return null;
    }

    #endregion

    #region AugmentKeyIdx

    public void AugmentKeyIdxMethod()
    {
        StartCoroutine(AugmentKeyIdxCo());
    }
    IEnumerator AugmentKeyIdxCo()
    {
        WWWForm form = new WWWForm();
        form.AddField("Input_user", DataController.Instance.curID);

        WWW webRequest = new WWW(AugmentKeyIdxUrl, form);
        yield return webRequest;

        if (string.IsNullOrEmpty(webRequest.error))
        {
            var N = JSON.Parse(webRequest.text);
            var Array = N["results"];

            if (Array.Count > 0)
            {
                for (int i = 0; i < Array.Count; i++)
                {
                    DataController.Instance.keyIdx = Array[i]["KEY"].Value;
                }
            }
            else
            {
                print("parsing problem");
            }
                
        }
        yield return null;
    }
    #endregion


    #region LoadRecordESC
    public void LoadRecordESC()
    {
        StartCoroutine(LoadRecordESCCo());
    }
    IEnumerator LoadRecordESCCo()
    {
        WWWForm form = new WWWForm();

        WWW webRequest = new WWW(LoadRecordUrl, form);
        yield return webRequest;

        if (string.IsNullOrEmpty(webRequest.error))
        {
            var N = JSON.Parse(webRequest.text);
            var Array = N["results"];

            if (Array.Count > 0)
            {
                for (int i = 0; i < Array.Count; i++)
                {
                    GetComponent<ESCManager>().RankBoardText.text += Array[i]["NO"].Value + "\n";
                    GetComponent<ESCManager>().RankBoardText1.text += Array[i]["ID"].Value + "\n";
                    GetComponent<ESCManager>().RankBoardText2.text += Array[i]["CHA"].Value + "\n";
                    GetComponent<ESCManager>().RankBoardText3.text += Array[i]["CLEAR"].Value + "\n";
                }
            }
            else
                Debug.Log("��ŷ ������ ���� ��");
        }

    }



    #endregion
}
