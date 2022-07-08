using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeManager : MonoBehaviour
{

    public Image[] img_Skill;
    public bool isCool1 = false;
    public bool isCool2 = false;
    public bool isCool3 = false;
    public bool isCool4 = false;


    private void Update()
    {
        if (DataController.Instance.CharSelectIdx == 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && isCool1 == false && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= 5)
            {

                StartCoroutine(CoolTime1(2f));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && isCool2 == false && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= 5)
            {
                StartCoroutine(CoolTime2(2f));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && isCool3 == false && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= 5)
            {
                StartCoroutine(CoolTime3(2f));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && isCool4 == false && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= 20)
            {
                StartCoroutine(CoolTime4(2f));
            }
        }
        else if (DataController.Instance.CharSelectIdx == 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= 10
            && isCool1 == false)
            {
                StartCoroutine(CoolTime1(3f));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= 20
            && isCool2 == false)
            {
                StartCoroutine(CoolTime2(3f));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= 15
            && isCool3 == false)
            {
                StartCoroutine(CoolTime3(3f));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= 20
            && isCool4 == false)
            {
                StartCoroutine(CoolTime4(3f));
            }
        }
        else if (DataController.Instance.CharSelectIdx == 2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= 10 && isCool1 == false)
            {
                StartCoroutine(CoolTime1(2f));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= 15 && isCool2 == false)
            {
                StartCoroutine(CoolTime2(2f));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= 5 && isCool3 == false)
            {
                StartCoroutine(CoolTime3(2f));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= 20 && isCool4 == false)
            {
                StartCoroutine(CoolTime4(2f));
            }
        }


    }

    IEnumerator CoolTime1(float cool)
    {
        float a = 0;

        while (a < cool)
        {
            a += Time.deltaTime;
            img_Skill[0].fillAmount = a / cool;


            yield return new WaitForFixedUpdate();
        }

        img_Skill[0].fillAmount = 0;
        isCool1 = false;

    }
    IEnumerator CoolTime2(float cool)
    {
        float a = 0;

        while (a < cool)
        {
            a += Time.deltaTime;
            img_Skill[1].fillAmount = a / cool;

            yield return new WaitForFixedUpdate();
        }

        img_Skill[1].fillAmount = 0;
        isCool2 = false;

    }
    IEnumerator CoolTime3(float cool)
    {
        float a = 0;

        while (a < cool)
        {
            a += Time.deltaTime;
            img_Skill[2].fillAmount = a / cool;

            yield return new WaitForFixedUpdate();
        }

        img_Skill[2].fillAmount = 0;
        isCool3 = false;

    }
    IEnumerator CoolTime4(float cool)
    {
        float a = 0;

        while (a < cool)
        {
            a += Time.deltaTime;
            img_Skill[3].fillAmount = a / cool;

            yield return new WaitForFixedUpdate();
        }

        img_Skill[3].fillAmount = 0;
        isCool4 = false;

    }

    void StopCoolTime()
    {
        StopAllCoroutines();
    }
}