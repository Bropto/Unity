using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErikaSkillTreeUI : MonoBehaviour
{
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

    [Header("1st Line")]
    public Text T_skill1_dmgLv;
    public int skill1_dmgLv = 1;

    public Text T_skill2_dmgLv;
    public int skill2_dmgLv = 1;

    public Text T_skill3_HPLv;
    public int skill3_HPLv = 1;

    public Text T_skill4_dmgLv;
    public int skill4_dmgLv = 1;

    [Header("2nd Line")]

    public Text T_skill1_NoProj1Lv;
    public int skill1_NoProj1Lv = 1;

    public Text T_skill2_NoProj2Lv;
    public int skill2_NoProj2Lv = 1;

    public Text T_skill3_MPLv;
    public int skill3_MPLv = 1;

    public Text T_skill4_NoProj4Lv;
    public int skill4_NoProj4Lv = 1;

    [Header("3rd Line")]

    public Text T_skill1_Speed1Lv;
    public int skill1_SpeedLv = 1;

    public Text T_skill2_Speed2Lv;
    public int skill2_SpeedLv = 1;

    public Text T_skill4_ProjDistance4Lv;
    public int skill4_ProjDistance4Lv = 1;

    [Header("4th Line")]
    public Text T_skill1_ProjDistance1Lv;
    public int skill1_ProjDistance1Lv = 1;

    [Header("버튼 관리")]
    Button B_skill1_Dmg;
    Button B_skill2_Dmg;
    Button B_skill3_HP;
    Button B_skill4_Dmg;

    Button B_skill1_Proj;
    Button B_skill2_Proj;
    Button B_skill3_MP;
    Button B_skill4_Proj;

    Button B_skill1_Speed;
    Button B_skill2_Speed;
    Button B_Skill4_Distance;

    Button B_Skill1_Distance;


    private void Start()
    {
        SkillTree = transform.Find("ErikaSkillTreeUICanvas").gameObject;
        SkillTree.SetActive(false);

        isClick_K = false;

        T_skill1_dmgLv.text = "Dmg Lv" + skill1_dmgLv;
        T_skill2_dmgLv.text = "Dmg Lv" + skill2_dmgLv;
        T_skill3_HPLv.text = "HPHeal Lv" + skill3_HPLv;
        T_skill4_dmgLv.text = "Dmg Lv" + skill4_dmgLv;

        T_skill1_NoProj1Lv.text = "No.Proj Lv" + skill1_NoProj1Lv;
        T_skill2_NoProj2Lv.text = "No.Proj Lv" + skill2_NoProj2Lv;
        T_skill3_MPLv.text = "MPHeal Lv" + skill3_MPLv;
        T_skill4_NoProj4Lv.text = "No.Proj Lv" + skill4_NoProj4Lv;

        T_skill1_Speed1Lv.text = "Speed Lv" + skill1_SpeedLv;
        T_skill2_Speed2Lv.text = "Speed Lv" + skill2_SpeedLv;
        T_skill4_ProjDistance4Lv.text = "Proj_Distance Lv" + skill4_ProjDistance4Lv;

        T_skill1_ProjDistance1Lv.text = "Proj_Distance Lv" + skill1_ProjDistance1Lv;

        //버튼 연결 코드
        B_skill1_Dmg = this.transform.Find("ErikaSkillTreeUICanvas/SkillUpPanel/DmgUp_1").GetComponent<Button>();
        B_skill2_Dmg = this.transform.Find("ErikaSkillTreeUICanvas/SkillUpPanel/DmgUp_2").GetComponent<Button>();
        B_skill3_HP = this.transform.Find("ErikaSkillTreeUICanvas/SkillUpPanel/HPUp").GetComponent<Button>();
        B_skill4_Dmg = this.transform.Find("ErikaSkillTreeUICanvas/SkillUpPanel/DmgUp_4").GetComponent<Button>();


        B_skill1_Proj = this.transform.Find("ErikaSkillTreeUICanvas/SkillUpPanel_1/No.Proj1").GetComponent<Button>();
        B_skill2_Proj = this.transform.Find("ErikaSkillTreeUICanvas/SkillUpPanel_1/No.Proj2").GetComponent<Button>();
        B_skill3_MP = this.transform.Find("ErikaSkillTreeUICanvas/SkillUpPanel_1/MPHeal").GetComponent<Button>();
        B_skill4_Proj = this.transform.Find("ErikaSkillTreeUICanvas/SkillUpPanel_1/No.Proj4").GetComponent<Button>();

        B_skill1_Speed = this.transform.Find("ErikaSkillTreeUICanvas/SkillUpPanel_2/Proj Speed1").GetComponent<Button>();
        B_skill2_Speed = this.transform.Find("ErikaSkillTreeUICanvas/SkillUpPanel_2/Proj Speed2").GetComponent<Button>();
        B_Skill4_Distance = this.transform.Find("ErikaSkillTreeUICanvas/SkillUpPanel_2/Proj Distance").GetComponent<Button>();

        B_Skill1_Distance = this.transform.Find("ErikaSkillTreeUICanvas/SkillUpPanel_3/Proj Distance").GetComponent<Button>();

        //클릭했을때 함수불러오는것들
        B_skill1_Dmg.onClick.AddListener(ClickSkill1Dmg);
        B_skill2_Dmg.onClick.AddListener(ClickSkill2Dmg);
        B_skill3_HP.onClick.AddListener(ClickSkill3HPHeal);
        B_skill4_Dmg.onClick.AddListener(ClickSkill4Dmg);

        B_skill1_Proj.onClick.AddListener(ClickSkill1No_Proj);
        B_skill2_Proj.onClick.AddListener(ClickSkill2No_Proj);
        B_skill3_MP.onClick.AddListener(ClickSkill3MPHeal);
        B_skill4_Proj.onClick.AddListener(ClickSkill4No_Proj);

        B_skill1_Speed.onClick.AddListener(ClickSkill1Speed);
        B_skill2_Speed.onClick.AddListener(ClickSkill2Speed);
        B_Skill4_Distance.onClick.AddListener(ClickSkill4Proj_Distance);

        B_Skill1_Distance.onClick.AddListener(ClickSkll1Proj_Distance);

    }


    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.F)) && DataController.Instance.CharSelectIdx == 2)
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

    //스킬UP 1줄
    public void ClickSkill1Dmg()
    {
        if (skillPoint1 > 0)
        {
            if (skill1_dmgLv < 5)
            {
                skillPoint1 -= 1;
                T_skillPoint1.text = skillPoint1 + "";

                skill1_dmgLv += 1;
                T_skill1_dmgLv.text = "Dmg Lv" + skill1_dmgLv;

                DataController.Instance.skill1_Damage += 50;
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

                DataController.Instance.skill2_Damage += 50;
            }
        }
    }

    public void ClickSkill3HPHeal()
    {
        if (skillPoint3 > 0)
        {
            if (skill3_HPLv < 5)
            {
                skillPoint3 -= 1;
                T_skillPoint3.text = skillPoint3 + "";

                skill3_HPLv += 1;
                T_skill3_HPLv.text = "HPHeal Lv" + skill3_HPLv;

                DataController.Instance.HP_UP += 50;
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

                DataController.Instance.skill4_Damage += 50;
            }
        }
    }
    //스킬UP 2줄

    public void ClickSkill1No_Proj()
    {
        if (skillPoint1 > 0)
        {
            if (skill1_NoProj1Lv < 4)
            {
                skillPoint1 -= 1;
                T_skillPoint1.text = skillPoint1 + "";


                skill1_NoProj1Lv += 1;
                T_skill1_NoProj1Lv.text = "No.Proj Lv" + skill1_NoProj1Lv;

                DataController.Instance.skill1_Projectile += 2;
            }
        }
    }
    public void ClickSkill2No_Proj()
    {
        if (skillPoint2 > 0)
        {
            if (skill2_NoProj2Lv < 4)
            {
                skillPoint2 -= 1;
                T_skillPoint2.text = skillPoint2 + "";


                skill2_NoProj2Lv += 1;
                T_skill2_NoProj2Lv.text = "No.Proj Lv" + skill2_NoProj2Lv;

                DataController.Instance.skill2_Projectile += 2;
            }
        }
    }

    public void ClickSkill3MPHeal()
    {
        if (skillPoint3 > 0)
        {
            if (skill3_MPLv < 5)
            {
                skillPoint3 -= 1;
                T_skillPoint3.text = skillPoint3 + "";

                skill3_MPLv += 1;
                T_skill3_MPLv.text = "MPHeal Lv" + skill3_MPLv;

                DataController.Instance.MP_UP += 20;

            }

        }
    }

    public void ClickSkill4No_Proj()
    {
        if (skillPoint4 > 0)
        {
            if (skill4_NoProj4Lv < 4)
            {
                skillPoint4 -= 1;
                T_skillPoint4.text = skillPoint4 + "";


                skill4_NoProj4Lv += 1;
                T_skill4_NoProj4Lv.text = "No.Proj Lv" + skill4_NoProj4Lv;

                DataController.Instance.skill4_Projectile += 2;
            }
        }
    }
    //스킬UP 3줄

    public void ClickSkill1Speed()
    {
         if(skillPoint1 > 0)
        {
            if(skill1_SpeedLv < 5)
            {
                skillPoint1 -= 1;
                T_skillPoint1.text = skillPoint1 + "";

                skill1_SpeedLv += 1;
                T_skill1_Speed1Lv.text ="Speed Lv"+ skill1_SpeedLv;

                DataController.Instance.skill1speed += 300;
            }
        }
    }

    public void ClickSkill2Speed()
    {
        if (skillPoint2 > 0)
        {
            if (skill2_SpeedLv < 5)
            {
                skillPoint2 -= 1;
                T_skillPoint2.text = skillPoint2 + "";

                skill2_SpeedLv +=1;
                T_skill2_Speed2Lv.text = "Speed Lv" + skill2_SpeedLv;

                
                DataController.Instance.skill2speed += 500;
            }
        }
    }
    
    public void ClickSkill4Proj_Distance()
    {
        if(skillPoint4>0)
        {
            if(skill4_ProjDistance4Lv< 5)
            {
                skillPoint4 -= 1;
                T_skillPoint4.text = skillPoint4 + "";

                skill4_ProjDistance4Lv += 1;
                T_skill4_ProjDistance4Lv.text = "Proj_Distance Lv" + skill4_ProjDistance4Lv;

                DataController.Instance.tr_distace += 1;

            }
        }
    }

    //스킬UP 4줄

    public void ClickSkll1Proj_Distance()
    {
        if (skillPoint1 > 0)
        {
            if (skill1_ProjDistance1Lv < 5)
            {
                skillPoint1 -= 1;
                T_skillPoint1.text = skillPoint1 + "";

                skill1_ProjDistance1Lv += 1;
                T_skill1_ProjDistance1Lv.text = "Proj_Distance Lv" + skill1_ProjDistance1Lv;

                DataController.Instance.destoryarrow1 += 1;
            }
        }
    }
}

