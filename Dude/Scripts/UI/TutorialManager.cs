using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class TutorialManager : MonoBehaviour
{
    GameObject TutorialCanvas;
    public GameObject PortalToStage1;
    public GameObject SkillBook1;
    public GameObject SkillBook2;
    public GameObject SkillBook3;
    public GameObject SkillBook4;
    public GameObject HpPortion;
    public GameObject MpPortion;

    Text T_tutorial;
    bool isTutorialing = false;
    Queue<string> tutorialQueue = new Queue<string>();
    string[] tutorialDialogue;
    void Start()
    {
        tutorialDialogue = new string[12];
        DataController.Instance.DataInitialization();
        LoadXml();

        SkillBook1.SetActive(false);
        SkillBook2.SetActive(false);
        SkillBook3.SetActive(false);
        SkillBook4.SetActive(false);
        HpPortion.SetActive(false);
        MpPortion.SetActive(false);

        TutorialCanvas = transform.Find("TutorialCanvas").gameObject;
        TutorialCanvas.SetActive(false);
        PortalToStage1.SetActive(true);
        T_tutorial = TutorialCanvas.transform.Find("Text").GetComponent<Text>();


        if (DataController.Instance.mainWindowManager.guestLogin == true)
        {
            if (DataController.Instance.keyIdxInt == 0)
            {
                TutorialStart();
            }
        }

        if (DataController.Instance.mainWindowManager.guestLogin == false)
        {
            if (DataController.Instance.keyIdx == "0")
            {
                TutorialStart();
            }
        }
    }

    void Update()
    {
        if (isTutorialing)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                //Skip
                tutorialQueue.Clear();
                EndDialogue();
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                //대화창 다음 넘기기
                StartDialogue();
            }
        }
    }

    void TutorialStart()
    {
        isTutorialing = true;
        InitialTutorialDialogueSetting();
        PortalToStage1.SetActive(false);
        TutorialCanvas.SetActive(true);
        StartDialogue();

    }

    void InitialTutorialDialogueSetting()
    {
        LoadXml();

        tutorialQueue.Clear();

        foreach (string sentence in tutorialDialogue)
        {
            tutorialQueue.Enqueue(sentence);
        }
    }

    void StartDialogue()
    {
        if (tutorialQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        if (tutorialQueue.Count == 7)
        {
            SkillBook1.SetActive(true);
        }

        if (tutorialQueue.Count == 5)
        {
            SkillBook2.SetActive(true);
        }

        if (tutorialQueue.Count == 4)
        {
            SkillBook3.SetActive(true);
        }

        if (tutorialQueue.Count == 3)
        {
            SkillBook4.SetActive(true);
        }

        if (tutorialQueue.Count == 2)
        {
            HpPortion.SetActive(true);
            MpPortion.SetActive(true);
        }

        T_tutorial.text = tutorialQueue.Dequeue();
    }

    void EndDialogue()
    {
        isTutorialing = false;
        PortalToStage1.SetActive(true);
        TutorialCanvas.SetActive(false);
        DataController.Instance.DataInitialization();

    }

    void CreateXml()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "Tutorial", string.Empty);
        xmlDoc.AppendChild(root);
        XmlNode child = xmlDoc.CreateNode(XmlNodeType.Element, "Dialogue", string.Empty);
        root.AppendChild(child);

        XmlElement dialogue0 = xmlDoc.CreateElement("dialogue0");
        dialogue0.InnerText = "튜토리얼 시작하겠습니다. G키를 누르면 다음 대사가 나옵니다.\n" +
    "만약 튜토리얼이 필요 없으면 P키를 눌러 건너뛰십시오.";
        child.AppendChild(dialogue0);

        XmlElement dialogue1 = xmlDoc.CreateElement("dialogue1");
        dialogue1.InnerText = "wasd로 캐릭터를 움직입니다. 마우스를 좌우로 움직이면 화면이 회전합니다.";
        child.AppendChild(dialogue1);

        XmlElement dialogue2 = xmlDoc.CreateElement("dialogue2");
        dialogue2.InnerText = "좌클릭은 기본공격1입니다. 우클릭은 기본공격2입니다.";
        child.AppendChild(dialogue2);

        XmlElement dialogue3 = xmlDoc.CreateElement("dialogue3");
        dialogue3.InnerText = "w와 왼쪽 shift키를 누르면 전방 구르기를 합니다.";
        child.AppendChild(dialogue3);

        XmlElement dialogue4 = xmlDoc.CreateElement("dialogue4");
        dialogue4.InnerText = "k 또는 f키를 누르면 스킬트리 창이 켜고 한번 더 누르면 꺼집니다.";
        child.AppendChild(dialogue4);

        XmlElement dialogue5 = xmlDoc.CreateElement("dialogue5");
        dialogue5.InnerText = "이제부터 스킬북을 먹고 원하는 기능을 레벨업 해보겠습니다.\n" +
            "생성된 스킬북에 다가가면 자동으로 캐릭터가 스킬북을 획득합니다.";
        child.AppendChild(dialogue5);

        XmlElement dialogue6 = xmlDoc.CreateElement("dialogue6");
        dialogue6.InnerText = "k 또는 f키를 눌러 스킬트리 창을 키고 1번 스킬을 강화해보세요.";
        child.AppendChild(dialogue6);

        XmlElement dialogue7 = xmlDoc.CreateElement("dialogue7");
        dialogue7.InnerText = "이번엔 2번 스킬입니다.";
        child.AppendChild(dialogue7);

        XmlElement dialogue8 = xmlDoc.CreateElement("dialogue8");
        dialogue8.InnerText = "이번엔 3번 스킬입니다.";
        child.AppendChild(dialogue8);

        XmlElement dialogue9 = xmlDoc.CreateElement("dialogue9");
        dialogue9.InnerText = "이번엔 4번 스킬입니다.";
        child.AppendChild(dialogue9);

        XmlElement dialogue10 = xmlDoc.CreateElement("dialogue10");
        dialogue10.InnerText = "물약을 사용해보겠습니다. 생성된 물약에 다가가면 자동으로 획득합니다.\n5번을 누르면 Hp, 6번을 누르면 Mp가 회복됩니다.";
        child.AppendChild(dialogue10);

        XmlElement dialogue11 = xmlDoc.CreateElement("dialogue11");
        dialogue11.InnerText = "이제 튜토리얼이 끝났습니다. 생성된 포탈을 타고 몬스터를 처치하십시오.";
        child.AppendChild(dialogue11);



        xmlDoc.Save("./Assets/Resources/TutorialDialogue.xml");
        print("creat");
    }

    void LoadXml()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("TutorialDialogue");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodes = xmlDoc.SelectNodes("Tutorial/Dialogue");

        for ( int i = 0; i < 12; i++)
        {
            tutorialDialogue[i] = nodes[0].SelectSingleNode("dialogue" + i).InnerText;

        }
    }


}
