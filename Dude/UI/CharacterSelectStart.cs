using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectStart : MonoBehaviour
{
    GameObject MariaRawImage;
    GameObject BradyRawImage;
    GameObject ErikaRawImage;

    GameObject BradyLockedImage;
    GameObject ErikaLockedImage;
    MainWindowManager mainWindowManager;

    Text T_ExplainName;
    Text T_ExplainClass;
    Text T_ExplainSkill_1;
    Text T_ExplainSkill_2;
    Text T_ExplainSkill_3;
    Text T_ExplainSkill_4;


    private void Start()
    {
        mainWindowManager = GameObject.FindObjectOfType<MainWindowManager>().GetComponent<MainWindowManager>();

        MariaRawImage = transform.Find("Canvas/Panel_1/MariaRawImage").gameObject;
        BradyRawImage = transform.Find("Canvas/Panel_1/BradyRawImage").gameObject;
        ErikaRawImage = transform.Find("Canvas/Panel_1/ErikaRawImage").gameObject;

        BradyLockedImage = transform.Find("Canvas/Panel_2/Brady/XRawImage").gameObject;
        ErikaLockedImage = transform.Find("Canvas/Panel_2/Erika/XRawImage").gameObject;

        T_ExplainName = transform.Find("Canvas/Panel/Name/Text").GetComponent<Text>();
        T_ExplainClass = transform.Find("Canvas/Panel/Class/Text").GetComponent<Text>();
        T_ExplainSkill_1 = transform.Find("Canvas/Panel/Skill_1/Text").GetComponent<Text>();
        T_ExplainSkill_2 = transform.Find("Canvas/Panel/Skill_2/Text").GetComponent<Text>();
        T_ExplainSkill_3 = transform.Find("Canvas/Panel/Skill_3/Text").GetComponent<Text>();
        T_ExplainSkill_4 = transform.Find("Canvas/Panel/Skill_4/Text").GetComponent<Text>();

        MariaRawImage.SetActive(true);
        BradyRawImage.SetActive(false);
        ErikaRawImage.SetActive(false);
        MariaChaExplain();

        if (mainWindowManager.guestLogin == true)
        {
            if (DataController.Instance.keyIdxInt == 0)
            {
                BradyLockedImage.SetActive(true);
                ErikaLockedImage.SetActive(true);

            }

            else if (DataController.Instance.keyIdxInt == 1)
            {
                BradyLockedImage.SetActive(false);
                ErikaLockedImage.SetActive(true);
            }

            else
            {
                BradyLockedImage.SetActive(false);
                ErikaLockedImage.SetActive(false);

            }
        }

        else
        {
            if (DataController.Instance.keyIdx == "0")
            {
                BradyLockedImage.SetActive(true);
                ErikaLockedImage.SetActive(true);

            }

            else if (DataController.Instance.keyIdx == "1")
            {
                BradyLockedImage.SetActive(false);
                ErikaLockedImage.SetActive(true);

            }

            else
            {
                BradyLockedImage.SetActive(false);
                ErikaLockedImage.SetActive(false);
            }
        }

        
    }


    void MariaChaExplain()
    {
        T_ExplainName.text = "Maria";
        T_ExplainClass.text = "�� ��";
        T_ExplainSkill_1.text = "�ι� ����";
        T_ExplainSkill_2.text = "�˱� �߻�";
        T_ExplainSkill_3.text = "����ź ����";
        T_ExplainSkill_4.text = "Į�� �ҿ뵹��";
    }

    void BradyChaExplain()
    {
        T_ExplainName.text = "Brady";
        T_ExplainClass.text = "������";
        T_ExplainSkill_1.text = "���̾";
        T_ExplainSkill_2.text = "���׿�";
        T_ExplainSkill_3.text = "�ǵ� ����";
        T_ExplainSkill_4.text = "ü�� ����Ʈ��";
    }

    void ErikaChaExplain()
    {
        T_ExplainName.text = "Erika";
        T_ExplainClass.text = "�� ��";
        T_ExplainSkill_1.text = "�ٹ��� ȭ��";
        T_ExplainSkill_2.text = "���� ȭ��";
        T_ExplainSkill_3.text = "ȸ��";
        T_ExplainSkill_4.text = "����̵�";
    }

    public void GoMainScene()
    {
        SceneManager.LoadScene("MainScene");

    }

    public void StartMaria()
    {
        DataController.Instance.CharSelectIdx = 0;
        MariaChaExplain();
        MariaRawImage.SetActive(true);
        BradyRawImage.SetActive(false);
        ErikaRawImage.SetActive(false);

    }

    public void StartBrady()
    {
        if (mainWindowManager.guestLogin == true)
        {
            if (DataController.Instance.keyIdxInt >= 1)
            {
                DataController.Instance.CharSelectIdx = 1;
                BradyChaExplain();
                MariaRawImage.SetActive(false);
                BradyRawImage.SetActive(true);
                ErikaRawImage.SetActive(false);

            }
            else
            {
                print("Need Key");
            }
        }

        else if(mainWindowManager.guestLogin == false)
        {
            if (DataController.Instance.keyIdx == "0")
            {
                print("Need Key");
            }
            else if(DataController.Instance.keyIdx == "1")
            {
                DataController.Instance.CharSelectIdx = 1;
                BradyChaExplain();
                MariaRawImage.SetActive(false);
                BradyRawImage.SetActive(true);
                ErikaRawImage.SetActive(false);

            }
            else
            {
                DataController.Instance.CharSelectIdx = 1;
                BradyChaExplain();
                MariaRawImage.SetActive(false);
                BradyRawImage.SetActive(true);
                ErikaRawImage.SetActive(false);

            }
        }

    }

    public void StartErika()
    {
        if (mainWindowManager.guestLogin == true)
        {
            if (DataController.Instance.keyIdxInt >= 2)
            {
                DataController.Instance.CharSelectIdx = 2;
                ErikaChaExplain();
                MariaRawImage.SetActive(false);
                BradyRawImage.SetActive(false);
                ErikaRawImage.SetActive(true);

            }
            else
            {
                print("Need Key");
            }
        }

        else if (mainWindowManager.guestLogin == false)
        {
            if (DataController.Instance.keyIdx == "0")
            {
                print("Need Key");
            }
            else if (DataController.Instance.keyIdx == "1")
            {
                print("Need Key");
            }
            else
            {
                DataController.Instance.CharSelectIdx = 2;
                ErikaChaExplain();
                MariaRawImage.SetActive(false);
                BradyRawImage.SetActive(false);
                ErikaRawImage.SetActive(true);

            }
        }
    }
}
