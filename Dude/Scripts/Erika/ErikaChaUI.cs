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
        //ChaUI = transform.GetChild(1).gameObject; //�ڽĿ�����Ʈ �� 2��° ������Ʈ ����
        ChaUI = transform.Find("CharacterUI").gameObject; //�ڽĿ�����Ʈ �� �̸� ã�Ƽ� ����, ���� ������ ���̾��Ű���� ã��

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
