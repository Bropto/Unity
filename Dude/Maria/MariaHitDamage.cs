using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MariaHitDamage : MonoBehaviour
{

    //About Animator
    Animator anim;

    readonly int hashDie = Animator.StringToHash("Die");

    //About ItemAcquisition
    MariaSkillTreeUI mariaSkillTreeUi;
    MariaChaUI mariaChaUI;
    private void Start()
    {
        anim = GetComponent<Animator>();
        //FindObjectOfType<Script>() ��ũ��Ʈ ���� ������Ʈ ã��, GetComponent<Script> �ش� ������Ʈ�� ������ �ִ� ��ũ��Ʈ ����
        mariaSkillTreeUi = GameObject.FindObjectOfType<MariaSkillTreeUI>().GetComponent<MariaSkillTreeUI>();
        //mariaChaUI = GameObject.FindObjectOfType<MariaSkillTreeUI>().GetComponent<MariaChaUI>();
        mariaChaUI = DataController.Instance.UIManager.GetComponent<MariaChaUI>();
    }

    private void OnTriggerEnter(Collider coll)
    {


        if (coll.gameObject.CompareTag("BOSSATTACKA"))
        {
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp -= 100;
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayHealth(); //���� ������ HP UI ������Ʈ

            if (DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp <= 0)
            {
                anim.SetTrigger(hashDie);
                DataController.Instance.GameOver();
            }
        }

        if (coll.gameObject.CompareTag("BOSSATTACKB"))
        {
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp -= 200;
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayHealth(); //���� ������ HP UI ������Ʈ

            if (DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp <= 0)
            {
                anim.SetTrigger(hashDie);
                DataController.Instance.GameOver();
            }
        }

        if (coll.gameObject.CompareTag("BOSSATTACKC"))
        {
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp -= 100;
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayHealth(); //���� ������ HP UI ������Ʈ

            if (DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp <= 0)
            {
                anim.SetTrigger(hashDie);
                DataController.Instance.GameOver();
            }
        }
    }

    private void OnCollisionEnter(Collision coll)
    {

        //������ ����
        if (coll.collider.CompareTag("E_ATTACK")) //�ݶ��̴� �浹 �� �±� Ȱ���ؼ� �� �ν� �� ������ ����
        {
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp -= 10;

            DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayHealth(); //���� ������ HP UI ������Ʈ


            if (DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp <= 0)
            {
                anim.SetTrigger(hashDie);
                DataController.Instance.GameOver();
            }
        }
        else if (coll.collider.CompareTag("A_ATTACK"))
        {

            DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp -= coll.gameObject.GetComponent<MagicCtrl>().damage;
            Destroy(coll.gameObject);

            DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayHealth(); //���� ������ HP UI ������Ʈ


            if (DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp <= 0)
            {
                anim.SetTrigger(hashDie);
                DataController.Instance.GameOver();
            }
        }
        else if (coll.collider.CompareTag("M_ATTACK"))
        {

            DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp -= coll.gameObject.GetComponent<MagicCtrl>().damage;
            Destroy(coll.gameObject);

            DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayHealth(); //���� ������ HP UI ������Ʈ


            if (DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp <= 0)
            {
                anim.SetTrigger(hashDie);
                DataController.Instance.GameOver();
            }
        }
        if (coll.collider.CompareTag("POISONARROW"))
        {
            StartCoroutine(PoisonDamage());
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayHealth(); //���� ������ HP UI ������Ʈ

        }


        //������ ȹ�� �� ���� �ɼ� ����



        else if (coll.gameObject.CompareTag("ITEM_HP"))
        {
            mariaChaUI.H_Potion += 1;
            mariaChaUI.H_Text.text = mariaChaUI.H_Potion.ToString();

        }

        else if (coll.gameObject.CompareTag("ITEM_MP"))
        {
            mariaChaUI.M_Potion += 1;
            mariaChaUI.M_Text.text = mariaChaUI.M_Potion.ToString();

        }

        else if (coll.gameObject.CompareTag("ITEM_SKILL1"))
        {
            mariaSkillTreeUi.skillPoint1 += 1;
            mariaSkillTreeUi.T_skillPoint1.text = mariaSkillTreeUi.skillPoint1 + "";

        }

        else if (coll.gameObject.CompareTag("ITEM_SKILL2"))
        {
            mariaSkillTreeUi.skillPoint2 += 1;
            mariaSkillTreeUi.T_skillPoint2.text = mariaSkillTreeUi.skillPoint2 + "";
        }

        else if (coll.gameObject.CompareTag("ITEM_SKILL3"))
        {
            mariaSkillTreeUi.skillPoint3 += 1;
            mariaSkillTreeUi.T_skillPoint3.text = mariaSkillTreeUi.skillPoint3 + "";
        }

        else if (coll.gameObject.CompareTag("ITEM_SKILL4"))
        {
            mariaSkillTreeUi.skillPoint4 += 1;
            mariaSkillTreeUi.T_skillPoint4.text = mariaSkillTreeUi.skillPoint4 + "";
        }

        else if (coll.gameObject.CompareTag("ITEM_KEY"))
        {
            DataController.Instance.GetItemKey();
        }

        if (coll.gameObject.CompareTag("ITEM_SKILL1") || coll.gameObject.CompareTag("ITEM_SKILL2")
    || coll.gameObject.CompareTag("ITEM_SKILL3") || coll.gameObject.CompareTag("ITEM_SKILL4")
    || coll.gameObject.CompareTag("ITEM_HP") || coll.gameObject.CompareTag("ITEM_MP")
    || coll.gameObject.CompareTag("ITEM_KEY"))
        {
            Destroy(coll.gameObject);
        }

    }
    IEnumerator PoisonDamage()
    {

        for (int i = 0; i < 5; i++)
        {
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp -= 50;

            yield return new WaitForSeconds(1f); ;
        }
    }

}