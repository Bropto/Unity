using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MariaChaUI : MonoBehaviour
{
    public float IniHp { get; set; } = 1000;
    public Image hpBar;

    public float CurHp { get; set; }

    public float IniMp { get; set; } = 200;
    public Image mpBar;

    public float CurMp { get; set; }


    public int H_Potion = 0;
    public Text H_Text = null;
    public int M_Potion = 0;
    public Text M_Text = null;

    GameObject Maria_Class;
    GameObject Maria_Skill1;
    GameObject Maria_Skill2;
    GameObject Maria_Skill3;
    GameObject Maria_Skill4;

    GameObject Magic_Class;
    GameObject Magic_Skill1;
    GameObject Magic_Skill2;
    GameObject Magic_Skill3;
    GameObject Magic_Skill4;

    GameObject Archer_Class;
    GameObject Archer_Skill1;
    GameObject Archer_Skill2;
    GameObject Archer_Skill3;
    GameObject Archer_Skill4;

    private void Start()
    {
        hpBar = transform.Find("CharacterUICanvas/1_1_Panel/HpBar").GetComponent<Image>();
        mpBar = transform.Find("CharacterUICanvas/1_1_Panel/mpBar").GetComponent<Image>();
        H_Text = transform.Find("CharacterUICanvas/3_2_Panel/Healing/H_Count").GetComponent<Text>();
        M_Text = transform.Find("CharacterUICanvas/3_2_Panel/Mana/M_Count").GetComponent<Text>();

        CurHp = IniHp;
        CurMp = IniMp;



        Maria_Class = transform.Find("CharacterUICanvas/1_1_Panel/Image/Sword").gameObject;
        Maria_Skill1 = transform.Find("CharacterUICanvas/3_2_Panel/Skill(1)/Maria_1").gameObject;
        Maria_Skill2 = transform.Find("CharacterUICanvas/3_2_Panel/Skill(2)/Maria_2").gameObject;
        Maria_Skill3 = transform.Find("CharacterUICanvas/3_2_Panel/Skill(3)/Maria_3").gameObject;
        Maria_Skill4 = transform.Find("CharacterUICanvas/3_2_Panel/Skill(4)/Maria_4").gameObject;

        Magic_Class = transform.Find("CharacterUICanvas/1_1_Panel/Image/Magic").gameObject;
        Magic_Skill1 = transform.Find("CharacterUICanvas/3_2_Panel/Skill(1)/Magic1").gameObject;
        Magic_Skill2 = transform.Find("CharacterUICanvas/3_2_Panel/Skill(2)/Magic2").gameObject;
        Magic_Skill3 = transform.Find("CharacterUICanvas/3_2_Panel/Skill(3)/Magic3").gameObject;
        Magic_Skill4 = transform.Find("CharacterUICanvas/3_2_Panel/Skill(4)/Magic4").gameObject;

        Archer_Class = transform.Find("CharacterUICanvas/1_1_Panel/Image/Bow").gameObject;
        Archer_Skill1 = transform.Find("CharacterUICanvas/3_2_Panel/Skill(1)/Archor_1").gameObject;
        Archer_Skill2 = transform.Find("CharacterUICanvas/3_2_Panel/Skill(2)/Archor_2").gameObject;
        Archer_Skill3 = transform.Find("CharacterUICanvas/3_2_Panel/Skill(3)/Archor_3").gameObject;
        Archer_Skill4 = transform.Find("CharacterUICanvas/3_2_Panel/Skill(4)/Archor_4").gameObject;

    }

    public void Char_Image()
    {
        if (DataController.Instance.CharSelectIdx == 0)
        {
            Maria_Class.SetActive(true);
            Magic_Class.SetActive(false);
            Archer_Class.SetActive(false);

            Maria_Skill1.SetActive(true);
            Maria_Skill2.SetActive(true);
            Maria_Skill3.SetActive(true);
            Maria_Skill4.SetActive(true);

            Magic_Skill1.SetActive(false);
            Magic_Skill2.SetActive(false);
            Magic_Skill3.SetActive(false);
            Magic_Skill4.SetActive(false);

            Archer_Skill1.SetActive(false);
            Archer_Skill2.SetActive(false);
            Archer_Skill3.SetActive(false);
            Archer_Skill4.SetActive(false);
        }
        else if (DataController.Instance.CharSelectIdx == 1)
        {
            Maria_Class.SetActive(false);
            Magic_Class.SetActive(true);
            Archer_Class.SetActive(false);

            Maria_Skill1.SetActive(false);
            Maria_Skill2.SetActive(false);
            Maria_Skill3.SetActive(false);
            Maria_Skill4.SetActive(false);

            Magic_Skill1.SetActive(true);
            Magic_Skill2.SetActive(true);
            Magic_Skill3.SetActive(true);
            Magic_Skill4.SetActive(true);

            Archer_Skill1.SetActive(false);
            Archer_Skill2.SetActive(false);
            Archer_Skill3.SetActive(false);
            Archer_Skill4.SetActive(false);
        }
        else if (DataController.Instance.CharSelectIdx == 2)
        {
            Maria_Class.SetActive(false);
            Magic_Class.SetActive(false);
            Archer_Class.SetActive(true);

            Maria_Skill1.SetActive(false);
            Maria_Skill2.SetActive(false);
            Maria_Skill3.SetActive(false);
            Maria_Skill4.SetActive(false);

            Magic_Skill1.SetActive(false);
            Magic_Skill2.SetActive(false);
            Magic_Skill3.SetActive(false);
            Magic_Skill4.SetActive(false);

            Archer_Skill1.SetActive(true);
            Archer_Skill2.SetActive(true);
            Archer_Skill3.SetActive(true);
            Archer_Skill4.SetActive(true);
        }
    }

    public void DisplayHealth()
    {
        hpBar.fillAmount = CurHp / IniHp;
    }
    public void DisplayMana()
    {
        mpBar.fillAmount = CurMp / IniMp;
    }
}