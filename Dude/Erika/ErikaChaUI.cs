using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErikaChaUI : MonoBehaviour
{
    public float IniHp { get; set; } = 1000;
    Image hpBar;
    public float CurHp { get; set; }

    public float IniMp { get; set; } = 200;
    Image mpBar;

    public float CurMp { get; set; }

    public bool isDie = false;

    GameObject ChaUI;

    public int H_Potion = 0;
    public Text H_Text = null;
    public int M_Potion = 0;
    public Text M_Text = null;

    private void Start()
    {
        //ChaUI = transform.GetChild(1).gameObject; //자식오브젝트 중 2번째 오브젝트 연결
        ChaUI = transform.Find("CharacterUI").gameObject; //자식오브젝트 중 이름 찾아서 연결, 만약 없으면 하이어라키에서 찾음

        ChaUI.SetActive(true);
        hpBar = GameObject.FindGameObjectWithTag("HP_BAR")?.GetComponent<Image>();
        mpBar = GameObject.FindGameObjectWithTag("MP_BAR")?.GetComponent<Image>();

        CurHp = IniHp;
        CurMp = IniMp;

        DisplayHealth();
        DisplayMana();
    }

    public void DisplayHealth()
    {
        hpBar.fillAmount = CurHp / IniHp;
    }
    public void DisplayMana()
    {
        if (CurMp >= 0)
        {
            mpBar.fillAmount = CurMp / IniMp;
        }
    }
}
