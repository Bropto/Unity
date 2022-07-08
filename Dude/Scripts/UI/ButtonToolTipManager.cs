using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class ButtonToolTipManager : MonoBehaviour
{
    public int skillNum = 1;
    public string skillName = "Double Slash";

    public int skillExplainNum = 1;
    public string skillExplain = "공격력 증가";
    public string skillNumericExplain = "현재 공격력";
    public string[] skillFigure;

    public string nextSkillNumericExplain = "다음 레벨 공격력";
    public string[] nextSkillFigure;

    public int masterSkillNum;

    TextAsset textAsset;
    XmlDocument xmlDoc;
    XmlNodeList node11; 
    XmlNodeList node12;
    XmlNodeList node21;
    XmlNodeList node22;
    XmlNodeList node23;
    XmlNodeList node24;
    XmlNodeList node25;
    XmlNodeList node31;
    XmlNodeList node32;
    XmlNodeList node33;
    XmlNodeList node34;
    XmlNodeList node35;
    XmlNodeList node41;
    XmlNodeList node42;
    XmlNodeList node43;

    GameObject Skill11Btn;
    GameObject Skill12Btn;

    GameObject Skill21Btn;
    GameObject Skill22Btn;
    GameObject Skill23Btn;
    GameObject Skill24Btn;
    GameObject Skill25Btn;

    GameObject Skill31Btn;
    GameObject Skill32Btn;
    GameObject Skill33Btn;
    GameObject Skill34Btn;
    GameObject Skill35Btn;

    GameObject Skill41Btn;
    GameObject Skill42Btn;
    GameObject Skill43Btn;

    void Start()
    {
        textAsset = (TextAsset)Resources.Load("MariaSkillUpTooltip");
        xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        node11 = xmlDoc.SelectNodes("MariaSkills/Skill1/DmgUp");
        node12 = xmlDoc.SelectNodes("MariaSkills/Skill1/SpeedUp");

        node21 = xmlDoc.SelectNodes("MariaSkills/Skill2/DmgUp");
        node22 = xmlDoc.SelectNodes("MariaSkills/Skill2/SpeedUp");
        node23 = xmlDoc.SelectNodes("MariaSkills/Skill2/NumOfProj");
        node24 = xmlDoc.SelectNodes("MariaSkills/Skill2/ProjSpeed");
        node25 = xmlDoc.SelectNodes("MariaSkills/Skill2/ProjDistance");

        node31 = xmlDoc.SelectNodes("MariaSkills/Skill3/DmgUp");
        node32 = xmlDoc.SelectNodes("MariaSkills/Skill3/Duration");
        node33 = xmlDoc.SelectNodes("MariaSkills/Skill3/NumOfProj");
        node34 = xmlDoc.SelectNodes("MariaSkills/Skill3/ProjSpeed");
        node35 = xmlDoc.SelectNodes("MariaSkills/Skill3/ProjDistance");

        node41 = xmlDoc.SelectNodes("MariaSkills/Skill4/DmgUp");
        node42 = xmlDoc.SelectNodes("MariaSkills/Skill4/Duration");
        node43 = xmlDoc.SelectNodes("MariaSkills/Skill4/NumOfProj");

        Skill11Btn = transform.Find("MariaSkillTreeUICanvas/SkillUpPanel/Skill1_DmgUpButton").gameObject;
        Skill12Btn = transform.Find("MariaSkillTreeUICanvas/SkillUp_1Panel/Skill1_SpeedUpButton").gameObject;

        Skill21Btn = transform.Find("MariaSkillTreeUICanvas/SkillUpPanel/Skill2_DmgUpButton").gameObject;
        Skill31Btn = transform.Find("MariaSkillTreeUICanvas/SkillUpPanel/Skill3_DmgUpButton").gameObject;
        Skill41Btn = transform.Find("MariaSkillTreeUICanvas/SkillUpPanel/Skill4_DmgUpButton").gameObject;

        Skill22Btn = transform.Find("MariaSkillTreeUICanvas/SkillUp_1Panel/Skill2_SpeedUpButton").gameObject;
        Skill32Btn = transform.Find("MariaSkillTreeUICanvas/SkillUp_1Panel/Skill3_DurationUpButton").gameObject;
        Skill42Btn = transform.Find("MariaSkillTreeUICanvas/SkillUp_1Panel/Skill4_DurationUpButton").gameObject;

        Skill23Btn = transform.Find("MariaSkillTreeUICanvas/SkillUp_2Panel/Skill2_NoPojUpButton").gameObject;
        Skill33Btn = transform.Find("MariaSkillTreeUICanvas/SkillUp_2Panel/Skill3_NoPojUpButton").gameObject;
        Skill43Btn = transform.Find("MariaSkillTreeUICanvas/SkillUp_2Panel/Skill4_NoPojUpButton").gameObject;

        Skill24Btn = transform.Find("MariaSkillTreeUICanvas/SkillUp_3Panel/Skill2_Proj_SpeedUpButton").gameObject;
        Skill34Btn = transform.Find("MariaSkillTreeUICanvas/SkillUp_3Panel/Skill3_Proj_SpeedUpButton").gameObject;

        Skill25Btn = transform.Find("MariaSkillTreeUICanvas/SkillUp_4Panel/Skill2_Proj_DistanceUpButton").gameObject;
        Skill35Btn = transform.Find("MariaSkillTreeUICanvas/SkillUp_4Panel/Skill3_Proj_DistanceUpButton").gameObject;

    }

    public void LoadXml(int skillNum, int skillExplainNum)
    {

        ButtonIdentityExplain(skillNum, skillExplainNum);

        if (skillNum == 1 && skillExplainNum == 1)
        {
            Skill11Btn.GetComponent<ButtonToolTipXmlVersion>().skillName = skillName;
            Skill11Btn.GetComponent<ButtonToolTipXmlVersion>().skillExplain = skillExplain;
            Skill11Btn.GetComponent<ButtonToolTipXmlVersion>().skillNumericExplain = skillNumericExplain;
            Skill11Btn.GetComponent<ButtonToolTipXmlVersion>().nextSkillNumericExplain = nextSkillNumericExplain;

            Skill11Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure = new string[int.Parse(node11[0].SelectSingleNode("master").InnerText)];
            Skill11Btn.GetComponent<ButtonToolTipXmlVersion>().masterSkillNum = int.Parse(node11[0].SelectSingleNode("master").InnerText);

            for (int i = 0; i <  int.Parse(node11[0].SelectSingleNode("master").InnerText); i++)
            {
                Skill11Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure[i] = node11[0].SelectSingleNode("lv" + (i + 1)).InnerText;
            }
        }
        
        else if (skillNum == 1 && skillExplainNum == 2)
        {
            Skill12Btn.GetComponent<ButtonToolTipXmlVersion>().skillName = skillName;
            Skill12Btn.GetComponent<ButtonToolTipXmlVersion>().skillExplain = skillExplain;
            Skill12Btn.GetComponent<ButtonToolTipXmlVersion>().skillNumericExplain = skillNumericExplain;
            Skill12Btn.GetComponent<ButtonToolTipXmlVersion>().nextSkillNumericExplain = nextSkillNumericExplain;

            Skill12Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure = new string[int.Parse(node12[0].SelectSingleNode("master").InnerText)];
            Skill12Btn.GetComponent<ButtonToolTipXmlVersion>().masterSkillNum = int.Parse(node12[0].SelectSingleNode("master").InnerText);

            for (int i = 0; i < int.Parse(node12[0].SelectSingleNode("master").InnerText); i++)
            {
                Skill12Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure[i] = node12[0].SelectSingleNode("lv" + (i + 1)).InnerText;
            }
        }

        else if (skillNum == 2 && skillExplainNum == 1)
        {
            Skill21Btn.GetComponent<ButtonToolTipXmlVersion>().skillName = skillName;
            Skill21Btn.GetComponent<ButtonToolTipXmlVersion>().skillExplain = skillExplain;
            Skill21Btn.GetComponent<ButtonToolTipXmlVersion>().skillNumericExplain = skillNumericExplain;
            Skill21Btn.GetComponent<ButtonToolTipXmlVersion>().nextSkillNumericExplain = nextSkillNumericExplain;

            Skill21Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure = new string[int.Parse(node21[0].SelectSingleNode("master").InnerText)];
            Skill21Btn.GetComponent<ButtonToolTipXmlVersion>().masterSkillNum = int.Parse(node21[0].SelectSingleNode("master").InnerText);

            for (int i = 0; i < int.Parse(node21[0].SelectSingleNode("master").InnerText); i++)
            {
                Skill21Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure[i] = node21[0].SelectSingleNode("lv" + (i + 1)).InnerText;
            }
        }

        else if (skillNum == 2 && skillExplainNum == 2)
        {
            Skill22Btn.GetComponent<ButtonToolTipXmlVersion>().skillName = skillName;
            Skill22Btn.GetComponent<ButtonToolTipXmlVersion>().skillExplain = skillExplain;
            Skill22Btn.GetComponent<ButtonToolTipXmlVersion>().skillNumericExplain = skillNumericExplain;
            Skill22Btn.GetComponent<ButtonToolTipXmlVersion>().nextSkillNumericExplain = nextSkillNumericExplain;

            Skill22Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure = new string[int.Parse(node22[0].SelectSingleNode("master").InnerText)];
            Skill22Btn.GetComponent<ButtonToolTipXmlVersion>().masterSkillNum = int.Parse(node22[0].SelectSingleNode("master").InnerText);

            for (int i = 0; i < int.Parse(node22[0].SelectSingleNode("master").InnerText); i++)
            {
                Skill22Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure[i] = node22[0].SelectSingleNode("lv" + (i + 1)).InnerText;
            }
        }

        else if (skillNum == 2 && skillExplainNum == 4)
        {
            Skill23Btn.GetComponent<ButtonToolTipXmlVersion>().skillName = skillName;
            Skill23Btn.GetComponent<ButtonToolTipXmlVersion>().skillExplain = skillExplain;
            Skill23Btn.GetComponent<ButtonToolTipXmlVersion>().skillNumericExplain = skillNumericExplain;
            Skill23Btn.GetComponent<ButtonToolTipXmlVersion>().nextSkillNumericExplain = nextSkillNumericExplain;

            Skill23Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure = new string[int.Parse(node23[0].SelectSingleNode("master").InnerText)];
            Skill23Btn.GetComponent<ButtonToolTipXmlVersion>().masterSkillNum = int.Parse(node23[0].SelectSingleNode("master").InnerText);

            for (int i = 0; i < int.Parse(node23[0].SelectSingleNode("master").InnerText); i++)
            {
                Skill23Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure[i] = node23[0].SelectSingleNode("lv" + (i + 1)).InnerText;
            }
        }

        else if (skillNum == 2 && skillExplainNum == 5)
        {
            Skill24Btn.GetComponent<ButtonToolTipXmlVersion>().skillName = skillName;
            Skill24Btn.GetComponent<ButtonToolTipXmlVersion>().skillExplain = skillExplain;
            Skill24Btn.GetComponent<ButtonToolTipXmlVersion>().skillNumericExplain = skillNumericExplain;
            Skill24Btn.GetComponent<ButtonToolTipXmlVersion>().nextSkillNumericExplain = nextSkillNumericExplain;

            Skill24Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure = new string[int.Parse(node24[0].SelectSingleNode("master").InnerText)];
            Skill24Btn.GetComponent<ButtonToolTipXmlVersion>().masterSkillNum = int.Parse(node24[0].SelectSingleNode("master").InnerText);

            for (int i = 0; i < int.Parse(node24[0].SelectSingleNode("master").InnerText); i++)
            {
                Skill24Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure[i] = node24[0].SelectSingleNode("lv" + (i + 1)).InnerText;
            }
        }

        else if (skillNum == 2 && skillExplainNum == 6)
        {
            Skill25Btn.GetComponent<ButtonToolTipXmlVersion>().skillName = skillName;
            Skill25Btn.GetComponent<ButtonToolTipXmlVersion>().skillExplain = skillExplain;
            Skill25Btn.GetComponent<ButtonToolTipXmlVersion>().skillNumericExplain = skillNumericExplain;
            Skill25Btn.GetComponent<ButtonToolTipXmlVersion>().nextSkillNumericExplain = nextSkillNumericExplain;

            Skill25Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure = new string[int.Parse(node25[0].SelectSingleNode("master").InnerText)];
            Skill25Btn.GetComponent<ButtonToolTipXmlVersion>().masterSkillNum = int.Parse(node25[0].SelectSingleNode("master").InnerText);

            for (int i = 0; i < int.Parse(node25[0].SelectSingleNode("master").InnerText); i++)
            {
                Skill25Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure[i] = node25[0].SelectSingleNode("lv" + (i + 1)).InnerText;
            }
        }

        else if (skillNum == 3 && skillExplainNum == 1)
        {
            Skill31Btn.GetComponent<ButtonToolTipXmlVersion>().skillName = skillName;
            Skill31Btn.GetComponent<ButtonToolTipXmlVersion>().skillExplain = skillExplain;
            Skill31Btn.GetComponent<ButtonToolTipXmlVersion>().skillNumericExplain = skillNumericExplain;
            Skill31Btn.GetComponent<ButtonToolTipXmlVersion>().nextSkillNumericExplain = nextSkillNumericExplain;

            Skill31Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure = new string[int.Parse(node31[0].SelectSingleNode("master").InnerText)];
            Skill31Btn.GetComponent<ButtonToolTipXmlVersion>().masterSkillNum = int.Parse(node31[0].SelectSingleNode("master").InnerText);

            for (int i = 0; i < int.Parse(node31[0].SelectSingleNode("master").InnerText); i++)
            {
                Skill31Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure[i] = node31[0].SelectSingleNode("lv" + (i + 1)).InnerText;
            }
        }

        else if (skillNum == 3 && skillExplainNum == 3)
        {
            Skill32Btn.GetComponent<ButtonToolTipXmlVersion>().skillName = skillName;
            Skill32Btn.GetComponent<ButtonToolTipXmlVersion>().skillExplain = skillExplain;
            Skill32Btn.GetComponent<ButtonToolTipXmlVersion>().skillNumericExplain = skillNumericExplain;
            Skill32Btn.GetComponent<ButtonToolTipXmlVersion>().nextSkillNumericExplain = nextSkillNumericExplain;

            Skill32Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure = new string[int.Parse(node32[0].SelectSingleNode("master").InnerText)];
            Skill32Btn.GetComponent<ButtonToolTipXmlVersion>().masterSkillNum = int.Parse(node32[0].SelectSingleNode("master").InnerText);

            for (int i = 0; i < int.Parse(node32[0].SelectSingleNode("master").InnerText); i++)
            {
                Skill32Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure[i] = node32[0].SelectSingleNode("lv" + (i + 1)).InnerText;
            }
        }

        else if (skillNum == 3 && skillExplainNum == 4)
        {
            Skill33Btn.GetComponent<ButtonToolTipXmlVersion>().skillName = skillName;
            Skill33Btn.GetComponent<ButtonToolTipXmlVersion>().skillExplain = skillExplain;
            Skill33Btn.GetComponent<ButtonToolTipXmlVersion>().skillNumericExplain = skillNumericExplain;
            Skill33Btn.GetComponent<ButtonToolTipXmlVersion>().nextSkillNumericExplain = nextSkillNumericExplain;

            Skill33Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure = new string[int.Parse(node33[0].SelectSingleNode("master").InnerText)];
            Skill33Btn.GetComponent<ButtonToolTipXmlVersion>().masterSkillNum = int.Parse(node33[0].SelectSingleNode("master").InnerText);

            for (int i = 0; i < int.Parse(node33[0].SelectSingleNode("master").InnerText); i++)
            {
                Skill33Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure[i] = node33[0].SelectSingleNode("lv" + (i + 1)).InnerText;
            }
        }

        else if (skillNum == 3 && skillExplainNum == 5)
        {
            Skill34Btn.GetComponent<ButtonToolTipXmlVersion>().skillName = skillName;
            Skill34Btn.GetComponent<ButtonToolTipXmlVersion>().skillExplain = skillExplain;
            Skill34Btn.GetComponent<ButtonToolTipXmlVersion>().skillNumericExplain = skillNumericExplain;
            Skill34Btn.GetComponent<ButtonToolTipXmlVersion>().nextSkillNumericExplain = nextSkillNumericExplain;

            Skill34Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure = new string[int.Parse(node34[0].SelectSingleNode("master").InnerText)];
            Skill34Btn.GetComponent<ButtonToolTipXmlVersion>().masterSkillNum = int.Parse(node34[0].SelectSingleNode("master").InnerText);

            for (int i = 0; i < int.Parse(node34[0].SelectSingleNode("master").InnerText); i++)
            {
                Skill34Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure[i] = node34[0].SelectSingleNode("lv" + (i + 1)).InnerText;
            }
        }

        else if (skillNum == 3 && skillExplainNum == 6)
        {
            Skill35Btn.GetComponent<ButtonToolTipXmlVersion>().skillName = skillName;
            Skill35Btn.GetComponent<ButtonToolTipXmlVersion>().skillExplain = skillExplain;
            Skill35Btn.GetComponent<ButtonToolTipXmlVersion>().skillNumericExplain = skillNumericExplain;
            Skill35Btn.GetComponent<ButtonToolTipXmlVersion>().nextSkillNumericExplain = nextSkillNumericExplain;

            Skill35Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure = new string[int.Parse(node35[0].SelectSingleNode("master").InnerText)];
            Skill35Btn.GetComponent<ButtonToolTipXmlVersion>().masterSkillNum = int.Parse(node35[0].SelectSingleNode("master").InnerText);

            for (int i = 0; i < int.Parse(node35[0].SelectSingleNode("master").InnerText); i++)
            {
                Skill35Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure[i] = node35[0].SelectSingleNode("lv" + (i + 1)).InnerText;
            }
        }

        else if (skillNum == 4 && skillExplainNum == 1)
        {
            Skill41Btn.GetComponent<ButtonToolTipXmlVersion>().skillName = skillName;
            Skill41Btn.GetComponent<ButtonToolTipXmlVersion>().skillExplain = skillExplain;
            Skill41Btn.GetComponent<ButtonToolTipXmlVersion>().skillNumericExplain = skillNumericExplain;
            Skill41Btn.GetComponent<ButtonToolTipXmlVersion>().nextSkillNumericExplain = nextSkillNumericExplain;

            Skill41Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure = new string[int.Parse(node41[0].SelectSingleNode("master").InnerText)];
            Skill41Btn.GetComponent<ButtonToolTipXmlVersion>().masterSkillNum = int.Parse(node41[0].SelectSingleNode("master").InnerText);

            for (int i = 0; i < int.Parse(node41[0].SelectSingleNode("master").InnerText); i++)
            {
                Skill41Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure[i] = node41[0].SelectSingleNode("lv" + (i + 1)).InnerText;
            }
        }

        else if (skillNum == 4 && skillExplainNum == 3)
        {
            Skill42Btn.GetComponent<ButtonToolTipXmlVersion>().skillName = skillName;
            Skill42Btn.GetComponent<ButtonToolTipXmlVersion>().skillExplain = skillExplain;
            Skill42Btn.GetComponent<ButtonToolTipXmlVersion>().skillNumericExplain = skillNumericExplain;
            Skill42Btn.GetComponent<ButtonToolTipXmlVersion>().nextSkillNumericExplain = nextSkillNumericExplain;

            Skill42Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure = new string[int.Parse(node42[0].SelectSingleNode("master").InnerText)];
            Skill42Btn.GetComponent<ButtonToolTipXmlVersion>().masterSkillNum = int.Parse(node42[0].SelectSingleNode("master").InnerText);

            for (int i = 0; i < int.Parse(node42[0].SelectSingleNode("master").InnerText); i++)
            {
                Skill42Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure[i] = node42[0].SelectSingleNode("lv" + (i + 1)).InnerText;
            }
        }

        else if (skillNum == 4 && skillExplainNum == 4)
        {
            Skill43Btn.GetComponent<ButtonToolTipXmlVersion>().skillName = skillName;
            Skill43Btn.GetComponent<ButtonToolTipXmlVersion>().skillExplain = skillExplain;
            Skill43Btn.GetComponent<ButtonToolTipXmlVersion>().skillNumericExplain = skillNumericExplain;
            Skill43Btn.GetComponent<ButtonToolTipXmlVersion>().nextSkillNumericExplain = nextSkillNumericExplain;

            Skill43Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure = new string[int.Parse(node43[0].SelectSingleNode("master").InnerText)];
            Skill43Btn.GetComponent<ButtonToolTipXmlVersion>().masterSkillNum = int.Parse(node43[0].SelectSingleNode("master").InnerText);

            for (int i = 0; i < int.Parse(node43[0].SelectSingleNode("master").InnerText); i++)
            {
                Skill43Btn.GetComponent<ButtonToolTipXmlVersion>().skillFigure[i] = node43[0].SelectSingleNode("lv" + (i + 1)).InnerText;
            }
        }
    }


    public void ButtonIdentityExplain(int skillNum, int skillExplainNum)
    {

        //스킬 번호, 이름 세팅
        if (skillNum == 1)
        {
            skillName = "Double Slash";
        }
        else if (skillNum == 2)
        {
            skillName = "Wind Scar";
        }
        else if (skillNum == 3)
        {
            skillName = "Petal Swirl";
        }
        else if (skillNum == 4)
        {
            skillName = "Blade Vortex";
        }

        //스킬 기능 설명 세팅
        if (skillExplainNum == 1)
        {
            skillExplain = "공격력 증가";
            skillNumericExplain = "현재 공격력";
            nextSkillNumericExplain = "다음 공격력";
        }
        else if (skillExplainNum == 2)
        {
            skillExplain = "칼 휘두르는 속도 증가";
            skillNumericExplain = "현재 속도";
            nextSkillNumericExplain = "다음 속도";

        }
        else if (skillExplainNum == 3)
        {
            skillExplain = "투사체 지속시간 증가";
            skillNumericExplain = "현재 투사체 지속시간";
            nextSkillNumericExplain = "다음 투사체 지속시간";

        }
        else if (skillExplainNum == 4)
        {
            skillExplain = "투사체 개수 증가";
            skillNumericExplain = "현재 투사체 개수";
            nextSkillNumericExplain = "다음 투사체 개수";

        }
        else if (skillExplainNum == 5)
        {
            skillExplain = "투사체 진행 속도 증가";
            skillNumericExplain = "현재 투사체 진행 속도";
            nextSkillNumericExplain = "다음 투사체 진행 속도";

        }
        else if (skillExplainNum == 6)
        {
            skillExplain = "투사체 진행 거리 증가";
            skillNumericExplain = "현재 투사체 진행 거리";
            nextSkillNumericExplain = "다음 투사체 진행 거리";

        }
    }


}
