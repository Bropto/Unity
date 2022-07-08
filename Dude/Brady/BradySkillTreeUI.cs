using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BradySkillTreeUI : MonoBehaviour
{
    GameObject BradySkillTreeUICanvas;
    public bool isClick_K = false;


    // 스킬 포인트 관련
    public Text T_skillPoint1;
    public int skillPoint1 = 00;

    public Text T_skillPoint2;
    public int skillPoint2 = 00;

    public Text T_skillPoint3;
    public int skillPoint3 = 00;

    public Text T_skillPoint4;
    public int skillPoint4 = 00;


    //스킬 1 관련
    public Text T_skill1_DmgLv;
    public int skill1_DmgLv = 1;
    Button skill1_Dmg_btn;

    public Text T_skill1_ProjLv;
    public int skill1_ProjLv = 1;
    Button skill1_Proj_btn;

    public Text T_skill1_DurLv;
    public int skill1_DurLv = 1;
    Button skill1_Dur_btn;

    //스킬 2 관련

    public Text T_skill2_DmgLv;
    public int skill2_DmgLv = 1;
    Button skill2_Dmg_btn;

    public Text T_skill2_ProjLv;
    public int skill2_ProjLv = 1;
    Button skill2_Proj_btn;

    public Text T_skill2_DurLv;
    public int skill2_DurLv = 1;
    Button skill2_Dur_btn;

    //스킬 3 관련

    public Text T_skill3_DmgLv;
    public int skill3_DmgLv;
    Button skill3_Dmg_btn;

    public Text T_skill3_ProjLv;
    public int skill3_ProjLv = 1;
    Button skill3_Proj_btn;

    //스킬 4 관련

    public Text T_skill4_DmgLv;
    public int skill4_DmgLv;
    Button skill4_Dmg_btn;

    public Text T_skill4_ProjLv;
    public int skill4_ProjLv = 1;
    Button skill4_Proj_btn;

    public Text T_skill4_DurLv;
    public int skill4_DurLv = 1;
    Button skill4_Dur_btn;


    void Start()
    {
        BradySkillTreeUICanvas = transform.Find("BradySkillTreeUICanvas").gameObject;
        BradySkillTreeUICanvas.SetActive(false);
        isClick_K = false;

        //스킬 포인트 연결
        T_skillPoint1 = BradySkillTreeUICanvas.transform.Find("BackPanel/SkillPoint/SkillPoint_1/Text").GetComponent<Text>();
        T_skillPoint2 = BradySkillTreeUICanvas.transform.Find("BackPanel/SkillPoint/SkillPoint_2/Text").GetComponent<Text>();
        T_skillPoint3 = BradySkillTreeUICanvas.transform.Find("BackPanel/SkillPoint/SkillPoint_3/Text").GetComponent<Text>();
        T_skillPoint4 = BradySkillTreeUICanvas.transform.Find("BackPanel/SkillPoint/SkillPoint_4/Text").GetComponent<Text>();

        //스킬1 레벨 연결
        T_skill1_DmgLv = BradySkillTreeUICanvas.transform.Find("BackPanel/Dmg_btn/Dmg_btn/Text").GetComponent<Text>();
        T_skill1_DmgLv.text = "Dmg Lv" + skill1_DmgLv;
        skill1_Dmg_btn = BradySkillTreeUICanvas.transform.Find("BackPanel/Dmg_btn/Dmg_btn").GetComponent<Button>();
        skill1_Dmg_btn.onClick.AddListener(ClickSkill1_Dmg);

        T_skill1_ProjLv = BradySkillTreeUICanvas.transform.Find("BackPanel/Proj_btn/Proj_btn/Text").GetComponent<Text>();
        T_skill1_ProjLv.text = "Proj Lv" + skill1_ProjLv;
        skill1_Dmg_btn = BradySkillTreeUICanvas.transform.Find("BackPanel/Proj_btn/Proj_btn").GetComponent<Button>();
        skill1_Dmg_btn.onClick.AddListener(ClickSkill1_Proj);

        T_skill1_DurLv = BradySkillTreeUICanvas.transform.Find("BackPanel/Dur_btn/Dur_btn/Text").GetComponent<Text>();
        T_skill1_DurLv.text = "Dur Lv" + skill1_DurLv;
        skill1_Dmg_btn = BradySkillTreeUICanvas.transform.Find("BackPanel/Dur_btn/Dur_btn").GetComponent<Button>();
        skill1_Dmg_btn.onClick.AddListener(ClickSkill1_Dur);

        //스킬2 레벨 연결
        T_skill2_DmgLv = BradySkillTreeUICanvas.transform.Find("BackPanel/Dmg_btn/Dmg_btn_1/Text").GetComponent<Text>();
        T_skill2_DmgLv.text = "Dmg Lv" + skill2_DmgLv;
        skill2_Dmg_btn = BradySkillTreeUICanvas.transform.Find("BackPanel/Dmg_btn/Dmg_btn_1").GetComponent<Button>();
        skill2_Dmg_btn.onClick.AddListener(ClickSkill2_Dmg);

        T_skill2_ProjLv = BradySkillTreeUICanvas.transform.Find("BackPanel/Proj_btn/Proj_btn_1/Text").GetComponent<Text>();
        T_skill2_ProjLv.text = "Proj Lv" + skill2_ProjLv;
        skill2_Dmg_btn = BradySkillTreeUICanvas.transform.Find("BackPanel/Proj_btn/Proj_btn_1").GetComponent<Button>();
        skill2_Dmg_btn.onClick.AddListener(ClickSkill2_Proj);

        T_skill2_DurLv = BradySkillTreeUICanvas.transform.Find("BackPanel/Dur_btn/Dur_btn_1/Text").GetComponent<Text>();
        T_skill2_DurLv.text = "Dur Lv" + skill2_DurLv;
        skill2_Dur_btn = BradySkillTreeUICanvas.transform.Find("BackPanel/Dur_btn/Dur_btn_1").GetComponent<Button>();
        skill2_Dur_btn.onClick.AddListener(ClickSkill2_Dur);

        //스킬3 레벨 연결
        T_skill3_DmgLv = BradySkillTreeUICanvas.transform.Find("BackPanel/Dmg_btn/Dmg_btn_2/Text").GetComponent<Text>();
        T_skill3_DmgLv.text = "Dmg Lv" + skill3_DmgLv;
        skill3_Dmg_btn = BradySkillTreeUICanvas.transform.Find("BackPanel/Dmg_btn/Dmg_btn_2").GetComponent<Button>();
        skill3_Dmg_btn.onClick.AddListener(ClickSkill3_Dmg);

        T_skill3_ProjLv = BradySkillTreeUICanvas.transform.Find("BackPanel/Proj_btn/Proj_btn_2/Text").GetComponent<Text>();
        T_skill3_ProjLv.text = "Proj Lv" + skill3_ProjLv;
        skill3_Proj_btn = BradySkillTreeUICanvas.transform.Find("BackPanel/Proj_btn/Proj_btn_2").GetComponent<Button>();
        skill3_Proj_btn.onClick.AddListener(ClickSkill3_Proj);

        //스킬4 레벨 연결
        T_skill4_DmgLv = BradySkillTreeUICanvas.transform.Find("BackPanel/Dmg_btn/Dmg_btn_3/Text").GetComponent<Text>();
        T_skill4_DmgLv.text = "Dmg Lv" + skill4_DmgLv;
        skill4_Dmg_btn = BradySkillTreeUICanvas.transform.Find("BackPanel/Dmg_btn/Dmg_btn_3").GetComponent<Button>();
        skill4_Dmg_btn.onClick.AddListener(ClickSkill4_Dmg);

        T_skill4_ProjLv = BradySkillTreeUICanvas.transform.Find("BackPanel/Proj_btn/Proj_btn_3/Text").GetComponent<Text>();
        T_skill4_ProjLv.text = "Proj Lv" + skill4_ProjLv;
        skill4_Proj_btn = BradySkillTreeUICanvas.transform.Find("BackPanel/Proj_btn/Proj_btn_3").GetComponent<Button>();
        skill4_Proj_btn.onClick.AddListener(ClickSkill4_Proj);

        T_skill4_DurLv = BradySkillTreeUICanvas.transform.Find("BackPanel/Dur_btn/Dur_btn_3/Text").GetComponent<Text>();
        T_skill4_DurLv.text = "Dur Lv" + skill4_DurLv;
        skill4_Dur_btn = BradySkillTreeUICanvas.transform.Find("BackPanel/Dur_btn/Dur_btn_3").GetComponent<Button>();
        skill4_Dur_btn.onClick.AddListener(ClickSkill4_Dur);

    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.F)) && DataController.Instance.CharSelectIdx == 1)
        {
            //단축키 누르면 키고 끄기 세팅
            if (!isClick_K)
            {
                BradySkillTreeUICanvas.SetActive(true);
                isClick_K = true;
                Cursor.lockState = CursorLockMode.None;

            }
            else
            {
                BradySkillTreeUICanvas.SetActive(false);
                isClick_K = false;
                Cursor.lockState = CursorLockMode.Locked;

            }

        }


    }


    /// <summary>
    /// /////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    public void ClickSkill1_Dmg()   //파이어볼 데미지 증가
    {
        if (skillPoint1 > 0)
        {
            if (skill1_DmgLv < 5)
            {
                skillPoint1 -= 1;
                T_skillPoint1.text = string.Format("{0}", skillPoint1);
                skill1_DmgLv += 1;
                T_skill1_DmgLv.text = "Dmg Lv" + skill1_DmgLv;

                DataController.Instance.FireBallDamage += 20;

            }
        }
    }
    public void ClickSkill1_Proj()
    {
        if (skillPoint1 > 0)
        {
            if (skill1_ProjLv < 5)
            {
                skillPoint1 -= 1;
                T_skillPoint1.text = string.Format("{0}", skillPoint1);
                skill1_ProjLv += 1;
                T_skill1_ProjLv.text = "Proj Lv" + skill1_ProjLv;

                DataController.Instance.FireBall_Proj++;
            }
        }
    }
    public void ClickSkill1_Dur()
    {
        if (skillPoint1 > 0)
        {
            if (skill1_DurLv < 5)
            {
                skillPoint1 -= 1;
                T_skillPoint1.text = string.Format("{0}", skillPoint1);
                skill1_DurLv += 1;
                T_skill1_DurLv.text = "Dur Lv" + skill1_DurLv;

                DataController.Instance.FireBallSplashDamage += 5;
            }
        }
    }
    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    public void ClickSkill2_Dmg()
    {
        if (skillPoint2 > 0)
        {
            if (skill2_DmgLv < 5)
            {
                skillPoint2 -= 1;
                T_skillPoint2.text = string.Format("{0}", skillPoint2);
                skill2_DmgLv += 1;
                T_skill2_DmgLv.text = "Dmg Lv" + skill2_DmgLv;

                DataController.Instance.Meteo += 15;
            }
        }
    }
    public void ClickSkill2_Proj()
    {
        if (skillPoint2 > 0)
        {
            if (skill2_ProjLv < 5)
            {
                skillPoint2 -= 1;
                T_skillPoint2.text = string.Format("{0}", skillPoint2);
                skill2_ProjLv += 1;
                T_skill2_ProjLv.text = "Proj Lv" + skill2_ProjLv;
                DataController.Instance.Meteo_Proj++;
            }
        }
    }
    public void ClickSkill2_Dur()
    {
        if (skillPoint2 > 0)
        {
            if (skill2_DurLv < 5)
            {
                skillPoint2 -= 1;
                T_skillPoint2.text = string.Format("{0}", skillPoint2);
                skill2_DurLv += 1;

                T_skill2_DurLv.text = "Dur Lv" + skill2_DurLv;

                DataController.Instance.MeteoSplash += 10;
            }
        }
    }
    /// <summary>
    /// /////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    public void ClickSkill3_Dmg()
    {
        if (skillPoint3 > 0)
        {
            if (skill3_DmgLv < 5)
            {
                skillPoint3 -= 1;
                T_skillPoint3.text = string.Format("{0}", skillPoint3);
                skill3_DmgLv += 1;
                T_skill3_DmgLv.text = "Dmg Lv" + skill3_DmgLv;

                DataController.Instance.sheildHp += 10;
            }
        }
    }
    public void ClickSkill3_Proj()
    {
        if (skillPoint3 > 0)
        {
            if (skill3_ProjLv < 5)
            {
                skillPoint3 -= 1;
                T_skillPoint3.text = string.Format("{0}", skillPoint3);
                skill3_ProjLv += 1;
                T_skill3_ProjLv.text = "Proj Lv" + skill3_ProjLv;

                DataController.Instance.sheild_Proj++;
            }
        }
    }

    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    public void ClickSkill4_Dmg()
    {
        if (skillPoint4 > 0)
        {
            if (skill4_DmgLv < 5)
            {
                skillPoint4 -= 1;
                T_skillPoint4.text = string.Format("{0}", skillPoint4);
                skill4_DmgLv += 1;
                T_skill4_DmgLv.text = "Dmg Lv" + skill4_DmgLv;

                DataController.Instance.ChainDamage += 10;

            }
        }
    }
    public void ClickSkill4_Proj()
    {
        if (skillPoint4 > 0)
        {
            if (skill4_ProjLv < 5)
            {
                skillPoint4 -= 1;
                T_skillPoint4.text = string.Format("{0}", skillPoint4);
                skill4_ProjLv += 1;
                T_skill4_ProjLv.text = "Proj Lv" + skill4_ProjLv;

                DataController.Instance.Chain_Proj++;
            }
        }
    }
    public void ClickSkill4_Dur()
    {
        if (skillPoint4 > 0)
        {
            if (skill4_DurLv < 5)
            {
                skillPoint4 -= 1;
                T_skillPoint4.text = string.Format("{0}", skillPoint4);
                skill4_DurLv += 1;
                T_skill4_DurLv.text = "Dur Lv" + skill4_DurLv;

                DataController.Instance.Chain_Time++;
            }
        }
    }
}