using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErikaHitDamage : MonoBehaviour
{
    //About Animator
    Animator anim;

    readonly int hashDie = Animator.StringToHash("Die");

    //About ItemAcquisition

    MariaChaUI mariaChaUI;
    ErikaSkillTreeUI erikaSkillTreeUi;
    private void Start()
    {
        anim = GetComponent<Animator>();
        //FindObjectOfType<Script>() 스크립트 가진 오브젝트 찾기, GetComponent<Script> 해당 오브젝트가 가지고 있는 스크립트 참조
        erikaSkillTreeUi = DataController.Instance.UIManager.GetComponent<ErikaSkillTreeUI>();
        //mariaChaUI = GameObject.FindObjectOfType<MariaSkillTreeUI>().GetComponent<MariaChaUI>();
        mariaChaUI = DataController.Instance.UIManager.GetComponent<MariaChaUI>();
    }

    private void OnTriggerEnter(Collider coll)
    {


        if (coll.gameObject.CompareTag("BOSSATTACKA"))
        {
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp -= 100;
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayHealth(); //맞을 때마다 HP UI 업데이트
            if (DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp <= 0)
            {
                anim.SetTrigger(hashDie);
                DataController.Instance.GameOver();
            }
        }

        if (coll.gameObject.CompareTag("BOSSATTACKB"))
        {
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp -= 200;
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayHealth(); //맞을 때마다 HP UI 업데이트
            if (DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp <= 0)
            {
                anim.SetTrigger(hashDie);
                DataController.Instance.GameOver();
            }
        }

        if (coll.gameObject.CompareTag("BOSSATTACKC"))
        {
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp -= 100;
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayHealth(); //맞을 때마다 HP UI 업데이트
            if (DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp <= 0)
            {
                anim.SetTrigger(hashDie);
                DataController.Instance.GameOver();
            }
        }
    }

    private void OnCollisionEnter(Collision coll)
    {

        //데미지 관련
        if (coll.collider.CompareTag("E_ATTACK")) //콜라이더 충돌 시 태그 활용해서 적 인식 후 데미지 전달
        {
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp -= 10;

            DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayHealth(); //맞을 때마다 HP UI 업데이트


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

            DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayHealth(); //맞을 때마다 HP UI 업데이트


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

            DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayHealth(); //맞을 때마다 HP UI 업데이트


            if (DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp <= 0)
            {
                anim.SetTrigger(hashDie);
                DataController.Instance.GameOver();
            }
        }
        if (coll.collider.CompareTag("POISONARROW"))
        {
            StartCoroutine(PoisonDamage());
        }






        //아이템 획득 및 증가 옵션 관리
        if (coll.gameObject.CompareTag("ITEM_SKILL1") || coll.gameObject.CompareTag("ITEM_SKILL2")
    || coll.gameObject.CompareTag("ITEM_SKILL3") || coll.gameObject.CompareTag("ITEM_SKILL4")
    || coll.gameObject.CompareTag("ITEM_HP") || coll.gameObject.CompareTag("ITEM_MP")
    || coll.gameObject.CompareTag("ITEM_KEY"))
        {
            Destroy(coll.gameObject);
        }

        if (coll.gameObject.CompareTag("ITEM_HP"))
        {
            mariaChaUI.H_Potion += 1;
            mariaChaUI.H_Text.text = mariaChaUI.H_Potion.ToString();

        }

        if (coll.gameObject.CompareTag("ITEM_MP"))
        {
            mariaChaUI.M_Potion += 1;
            mariaChaUI.M_Text.text = mariaChaUI.M_Potion.ToString();

        }

        #region EachCharacter SkillTreeUI

        if (coll.gameObject.CompareTag("ITEM_SKILL1"))
        {
            erikaSkillTreeUi.skillPoint1 += 1;
            erikaSkillTreeUi.T_skillPoint1.text = erikaSkillTreeUi.skillPoint1 + "";

        }

        if (coll.gameObject.CompareTag("ITEM_SKILL2"))
        {
            erikaSkillTreeUi.skillPoint2 += 1;
            erikaSkillTreeUi.T_skillPoint2.text = erikaSkillTreeUi.skillPoint2 + "";
        }

        if (coll.gameObject.CompareTag("ITEM_SKILL3"))
        {
            erikaSkillTreeUi.skillPoint3 += 1;
            erikaSkillTreeUi.T_skillPoint3.text = erikaSkillTreeUi.skillPoint3 + "";
        }

        if (coll.gameObject.CompareTag("ITEM_SKILL4"))
        {
            erikaSkillTreeUi.skillPoint4 += 1;
            erikaSkillTreeUi.T_skillPoint4.text = erikaSkillTreeUi.skillPoint4 + "";
        }

        #endregion

        if (coll.gameObject.CompareTag("ITEM_KEY"))
        {
            DataController.Instance.GetItemKey();
        }



    }
    IEnumerator PoisonDamage()
    {

        for (int i = 0; i < 5; i++)
        {
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp -= 5;
            yield return new WaitForSeconds(1f); ;
        }
    }

}