using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MariaSkillTreeUI : MonoBehaviour
{
    #region Variables

    GameObject SkillTree;

    public bool isClick_K = false;

    public Text T_skillPoint1;
    public int skillPoint1 = 0;

    public Text T_skillPoint2;
    public int skillPoint2 = 0;

    public Text T_skillPoint3;
    public int skillPoint3 = 0;

    public Text T_skillPoint4;
    public int skillPoint4 = 0;

    public Text T_skill1_dmgLv;
    public int skill1_dmgLv = 1;

    public Text T_skill2_dmgLv;
    public int skill2_dmgLv = 1;

    public Text T_skill3_dmgLv;
    public int skill3_dmgLv = 1;

    public Text T_skill4_dmgLv;
    public int skill4_dmgLv = 1;

    public Text T_skill1_speedLv;
    public int skill1_speedLv = 1;

    public Text T_skill2_speedLv;
    public int skill2_speedLv = 1;

    public Text T_skill3_durationLv;
    public int skill3_durationLv = 1;

    public Text T_skill4_durationLv;
    public int skill4_durationLv = 1;

    public Text T_skill2_no_ProjLv;
    public int skill2_no_ProjLv = 1;

    public Text T_skill3_no_ProjLv;
    public int skill3_no_ProjLv = 1;

    public Text T_skill4_no_ProjLv;
    public int skill4_no_ProjLv = 1;

    public Text T_skill2_ProjSpeedLv;
    public int skill2_ProjSpeedLv = 1;

    public Text T_skill3_ProjSpeedLv;
    public int skill3_ProjSpeedLv = 1;

    public Text T_skill2_ProjDistanceLv;
    public int skill2_ProjDistanceLv = 1;

    public Text T_skill3_ProjDistanceLv;
    public int skill3_ProjDistanceLv = 1;

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
    #endregion

    void Start()
    {
        SkillTree = transform.GetChild(0).gameObject;
        SkillTree.SetActive(false);

        isClick_K = false;

        T_skill1_dmgLv.text = "Dmg Lv" + skill1_dmgLv;
        T_skill2_dmgLv.text = "Dmg Lv" + skill2_dmgLv;
        T_skill3_dmgLv.text = "Dmg Lv" + skill3_dmgLv;
        T_skill4_dmgLv.text = "Dmg Lv" + skill4_dmgLv;

        T_skill1_speedLv.text = "Speed Lv" + skill1_speedLv;
        T_skill2_speedLv.text = "Speed Lv" + skill2_speedLv;
        T_skill3_durationLv.text = "Duration Lv" + skill3_durationLv;
        T_skill4_durationLv.text = "Duration Lv" + skill4_durationLv;

        T_skill2_no_ProjLv.text = "No.Proj Lv" + skill2_no_ProjLv;
        T_skill3_no_ProjLv.text = "No.Proj Lv" + skill3_no_ProjLv;
        T_skill4_no_ProjLv.text = "No.Proj Lv" + skill4_no_ProjLv;

        T_skill2_ProjSpeedLv.text = "Proj_Speed Lv" + skill2_ProjSpeedLv;
        T_skill3_ProjSpeedLv.text = "Proj_Speed Lv" + skill3_ProjSpeedLv;

        T_skill2_ProjDistanceLv.text = "Proj_Distance Lv" + skill2_ProjDistanceLv;
        T_skill3_ProjDistanceLv.text = "Proj_Distance Lv" + skill3_ProjDistanceLv;

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

        Skill11Btn.GetComponent<Button>().onClick.AddListener(ClickSkill1Dmg);

    }


    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.F)) && DataController.Instance.CharSelectIdx == 0)
        {
            //단축키 누르면 키고 끄기 세팅
            if (!isClick_K)
            {
                SkillTree.SetActive(true);
                isClick_K = true;
                Cursor.lockState = CursorLockMode.None;

            }
            else
            {
                SkillTree.SetActive(false);
                isClick_K = false;
                Cursor.lockState = CursorLockMode.Locked;

            }

        }


    }



    
    //Button OnClick Method
    void ClickSkill1Dmg()
    {

        if (skillPoint1 > 0) //스킬포인트 체크
        {
            if (skill1_dmgLv < 5) //만렙 5 체크
            {
                skillPoint1 -= 1; //스킬포인트 1 차감
                T_skillPoint1.text = skillPoint1 + ""; //UI표시


                skill1_dmgLv += 1; //데미지 텍스트 1 더하기
                T_skill1_dmgLv.text = "Dmg Lv" + skill1_dmgLv; //UI표시

                DataController.Instance.DsDamage += 20; //실제데이터 변경
                Skill11Btn.GetComponent<ButtonToolTipXmlVersion>().ClickEvent();
            }
        }
    }
    public void ClickSkill1Speed()
    {
        if (skillPoint1 > 0)
        {
            if (skill1_speedLv < 3)
            {
                skillPoint1 -= 1;
                T_skillPoint1.text = skillPoint1 + "";


                skill1_speedLv += 1;
                T_skill1_speedLv.text = "Speed Lv" + skill1_speedLv;
                DataController.Instance.DsSpeed += 1;
                Skill12Btn.GetComponent<ButtonToolTipXmlVersion>().ClickEvent();

            }
        }
    }


    public void ClickSkill2Dmg()
    {
        if (skillPoint2 > 0) 
        {
            if (skill2_dmgLv < 5) 
            {
                skillPoint2 -= 1;
                T_skillPoint2.text = skillPoint2 + "";


                skill2_dmgLv += 1;
                T_skill2_dmgLv.text = "Dmg Lv" + skill2_dmgLv;

                DataController.Instance.WsDamage += 20;
                Skill21Btn.GetComponent<ButtonToolTipXmlVersion>().ClickEvent();

            }
        }
    }

    public void ClickSkill2Speed()
    {
        if (skillPoint2 > 0)
        {
            if (skill2_speedLv < 3)
            {
                skillPoint2 -= 1;
                T_skillPoint2.text = skillPoint2 + "";


                skill2_speedLv += 1;
                T_skill2_speedLv.text = "Speed Lv" + skill2_speedLv;

                DataController.Instance.WsSpeed += 1;
                Skill22Btn.GetComponent<ButtonToolTipXmlVersion>().ClickEvent();
            }
        }
    }

    public void ClickSkill2No_Proj()
    {
        if (skillPoint2 > 0)
        {
            if (skill2_no_ProjLv < 4)
            {
                skillPoint2 -= 1;
                T_skillPoint2.text = skillPoint2 + "";


                skill2_no_ProjLv += 1;
                T_skill2_no_ProjLv.text = "No.Proj Lv" + skill2_no_ProjLv;

                DataController.Instance.WsProjectile += 2;
                Skill23Btn.GetComponent<ButtonToolTipXmlVersion>().ClickEvent();
            }
        }
    }
    public void ClickSkill2Proj_Speed()
    {
        if (skillPoint2 > 0)
        {
            if (skill2_ProjSpeedLv < 5)
            {
                skillPoint2 -= 1;
                T_skillPoint2.text = skillPoint2 + "";


                skill2_ProjSpeedLv += 1;
                T_skill2_ProjSpeedLv.text = "Proj_Speed Lv" + skill2_ProjSpeedLv;

                DataController.Instance.WsPojSpeed += 500;
                Skill24Btn.GetComponent<ButtonToolTipXmlVersion>().ClickEvent();
            }
        }
    }
    public void ClickSkill2Proj_Distance()
    {
        if (skillPoint2 > 0)
        {
            if (skill2_ProjDistanceLv < 5)
            {
                skillPoint2 -= 1;
                T_skillPoint2.text = skillPoint2 + "";


                skill2_ProjDistanceLv += 1;
                T_skill2_ProjDistanceLv.text = "Proj_Distance Lv" + skill2_ProjDistanceLv;

                DataController.Instance.WsDistance += 10;
                Skill25Btn.GetComponent<ButtonToolTipXmlVersion>().ClickEvent();
            }
        }
    }


    public void ClickSkill3Dmg()
    {
        if (skillPoint3 > 0)
        {
            if (skill3_dmgLv < 5)
            {
                skillPoint3 -= 1;
                T_skillPoint3.text = skillPoint3 + "";


                skill3_dmgLv += 1;
                T_skill3_dmgLv.text = "Dmg Lv" + skill3_dmgLv;

                DataController.Instance.PsDamage += 20;
                Skill31Btn.GetComponent<ButtonToolTipXmlVersion>().ClickEvent();
            }
        }
    }
    public void ClickSkill3Duration()
    {
        if (skillPoint3 > 0)
        {
            if (skill3_durationLv < 5)
            {
                skillPoint3 -= 1;
                T_skillPoint3.text = skillPoint3 + "";

                skill3_durationLv += 1;
                T_skill3_durationLv.text = "Duration Lv" + skill3_durationLv;

                DataController.Instance.PsDuration += 5;
                Skill32Btn.GetComponent<ButtonToolTipXmlVersion>().ClickEvent();
            }
        }
    }

    public void ClickSkill3No_Proj()
    {
        if (skillPoint3 > 0)
        {
            if (skill3_no_ProjLv < 5)
            {
                skillPoint3 -= 1;
                T_skillPoint3.text = skillPoint3 + "";


                skill3_no_ProjLv += 1;
                T_skill3_no_ProjLv.text = "No.Proj Lv" + skill3_no_ProjLv;

                DataController.Instance.PsNumOfProj += 5;
                Skill33Btn.GetComponent<ButtonToolTipXmlVersion>().ClickEvent();
            }
        }
    }

    public void ClickSkill3Proj_Speed()
    {
        if (skillPoint3 > 0)
        {
            if (skill3_ProjSpeedLv < 5)
            {
                skillPoint3 -= 1;
                T_skillPoint3.text = skillPoint3 + "";


                skill3_ProjSpeedLv += 1;
                T_skill3_ProjSpeedLv.text = "Proj_Speed Lv" + skill3_ProjSpeedLv;

                DataController.Instance.PsAttackSpeed += 100;
                Skill34Btn.GetComponent<ButtonToolTipXmlVersion>().ClickEvent();
            }
        }
    }

    public void ClickSkill3Proj_Distance()
    {
        if (skillPoint3 > 0)
        {
            if (skill3_ProjDistanceLv < 5)
            {
                skillPoint3 -= 1;
                T_skillPoint3.text = skillPoint3 + "";


                skill3_ProjDistanceLv += 1;
                T_skill3_ProjDistanceLv.text = "Proj_Distance Lv" + skill3_ProjDistanceLv;

                DataController.Instance.PsDistance += 10;
                Skill35Btn.GetComponent<ButtonToolTipXmlVersion>().ClickEvent();
            }
        }
    }


    public void ClickSkill4Dmg()
    {
        if (skillPoint4 > 0)
        {
            if (skill4_dmgLv < 5)
            {
                skillPoint4 -= 1;
                T_skillPoint4.text = skillPoint4 + "";


                skill4_dmgLv += 1;
                T_skill4_dmgLv.text = "Dmg Lv" + skill4_dmgLv;

                DataController.Instance.BvDamage += 20;
                Skill41Btn.GetComponent<ButtonToolTipXmlVersion>().ClickEvent();
            }
        }
    }

    public void ClickSkill4Duration()
    {
        if (skillPoint4 > 0)
        {
            if (skill4_durationLv < 5)
            {
                skillPoint4 -= 1;
                T_skillPoint4.text = skillPoint4 + "";

                skill4_durationLv += 1;
                T_skill4_durationLv.text = "Duration Lv" + skill4_durationLv;

                DataController.Instance.BvDuration += 5;
                Skill42Btn.GetComponent<ButtonToolTipXmlVersion>().ClickEvent();
            }
        }
    }

    public void ClickSkill4No_Proj()
    {
        if (skillPoint4 > 0)
        {
            if (skill4_no_ProjLv < 4) 
            {
                skillPoint4 -= 1;
                T_skillPoint4.text = skillPoint4 + "";  


                skill4_no_ProjLv += 1; 
                T_skill4_no_ProjLv.text = "No.Proj Lv" + skill4_no_ProjLv;

                DataController.Instance.BvNomOfProj += 1;
                Skill43Btn.GetComponent<ButtonToolTipXmlVersion>().ClickEvent();
            }
        }
    }




}
