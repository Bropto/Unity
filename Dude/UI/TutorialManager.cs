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
                //��ȭâ ���� �ѱ��
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
        dialogue0.InnerText = "Ʃ�丮�� �����ϰڽ��ϴ�. GŰ�� ������ ���� ��簡 ���ɴϴ�.\n" +
    "���� Ʃ�丮���� �ʿ� ������ PŰ�� ���� �ǳʶٽʽÿ�.";
        child.AppendChild(dialogue0);

        XmlElement dialogue1 = xmlDoc.CreateElement("dialogue1");
        dialogue1.InnerText = "wasd�� ĳ���͸� �����Դϴ�. ���콺�� �¿�� �����̸� ȭ���� ȸ���մϴ�.";
        child.AppendChild(dialogue1);

        XmlElement dialogue2 = xmlDoc.CreateElement("dialogue2");
        dialogue2.InnerText = "��Ŭ���� �⺻����1�Դϴ�. ��Ŭ���� �⺻����2�Դϴ�.";
        child.AppendChild(dialogue2);

        XmlElement dialogue3 = xmlDoc.CreateElement("dialogue3");
        dialogue3.InnerText = "w�� ���� shiftŰ�� ������ ���� �����⸦ �մϴ�.";
        child.AppendChild(dialogue3);

        XmlElement dialogue4 = xmlDoc.CreateElement("dialogue4");
        dialogue4.InnerText = "k �Ǵ� fŰ�� ������ ��ųƮ�� â�� �Ѱ� �ѹ� �� ������ �����ϴ�.";
        child.AppendChild(dialogue4);

        XmlElement dialogue5 = xmlDoc.CreateElement("dialogue5");
        dialogue5.InnerText = "�������� ��ų���� �԰� ���ϴ� ����� ������ �غ��ڽ��ϴ�.\n" +
            "������ ��ų�Ͽ� �ٰ����� �ڵ����� ĳ���Ͱ� ��ų���� ȹ���մϴ�.";
        child.AppendChild(dialogue5);

        XmlElement dialogue6 = xmlDoc.CreateElement("dialogue6");
        dialogue6.InnerText = "k �Ǵ� fŰ�� ���� ��ųƮ�� â�� Ű�� 1�� ��ų�� ��ȭ�غ�����.";
        child.AppendChild(dialogue6);

        XmlElement dialogue7 = xmlDoc.CreateElement("dialogue7");
        dialogue7.InnerText = "�̹��� 2�� ��ų�Դϴ�.";
        child.AppendChild(dialogue7);

        XmlElement dialogue8 = xmlDoc.CreateElement("dialogue8");
        dialogue8.InnerText = "�̹��� 3�� ��ų�Դϴ�.";
        child.AppendChild(dialogue8);

        XmlElement dialogue9 = xmlDoc.CreateElement("dialogue9");
        dialogue9.InnerText = "�̹��� 4�� ��ų�Դϴ�.";
        child.AppendChild(dialogue9);

        XmlElement dialogue10 = xmlDoc.CreateElement("dialogue10");
        dialogue10.InnerText = "������ ����غ��ڽ��ϴ�. ������ ���࿡ �ٰ����� �ڵ����� ȹ���մϴ�.\n5���� ������ Hp, 6���� ������ Mp�� ȸ���˴ϴ�.";
        child.AppendChild(dialogue10);

        XmlElement dialogue11 = xmlDoc.CreateElement("dialogue11");
        dialogue11.InnerText = "���� Ʃ�丮���� �������ϴ�. ������ ��Ż�� Ÿ�� ���͸� óġ�Ͻʽÿ�.";
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
