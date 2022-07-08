using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErikaSkill : MonoBehaviour
{
    Animator anim;

    readonly int hashRoll = Animator.StringToHash("IsRoll"); //구르기
    readonly int hashLAttack = Animator.StringToHash("LAttack");//좌클릭
    readonly int hashRAttack = Animator.StringToHash("RAttack");//우클릭

    readonly int hashSkill1 = Animator.StringToHash("Skill1");
    readonly int hashSkill2 = Animator.StringToHash("Skill2");
    readonly int hashSkill3 = Animator.StringToHash("Skill3");
    readonly int hashSkill4 = Animator.StringToHash("Skill4");

    public GameObject LAttackArrow;
    public GameObject SkillArrow;
    public GameObject Skill_BombArrow;
    public GameObject Skill_Heal;
    public GameObject SkillTornaodo;
    public Transform FirePos;
    public Transform HealPos;

    MariaChaUI chaUI;
    CoolTimeManager coolTimeManager;

    //스킬 마나 소모량
    float ErMp_Skill1 = 10;
    float ErMp_Skill2 = 15;
    float ErMp_Skill3 = 5;
    float ErMp_Skill4 = 20;


    void Start()
    {
        anim = GetComponent<Animator>();
        chaUI = DataController.Instance.UIManager.GetComponent<MariaChaUI>();
        coolTimeManager = DataController.Instance.UIManager.GetComponent<CoolTimeManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            anim.SetTrigger(hashLAttack);
        }
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger(hashRAttack);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))     //구르기
        {
            anim.SetTrigger(hashRoll);
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W))
        {
            anim.SetTrigger(hashRoll);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= ErMp_Skill1
            && coolTimeManager.isCool1 == false)
        {
            anim.SetTrigger(hashSkill1);
            coolTimeManager.isCool1 = true;

        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= ErMp_Skill2
            && coolTimeManager.isCool2 == false)
        {
            anim.SetTrigger(hashSkill2);
            coolTimeManager.isCool2 = true;

        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= ErMp_Skill3
            && coolTimeManager.isCool3 == false)
        {
            anim.SetTrigger(hashSkill3);
            coolTimeManager.isCool3 = true;

        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= ErMp_Skill4
            && coolTimeManager.isCool4 == false)
        {
            anim.SetTrigger(hashSkill4);
            coolTimeManager.isCool4 = true;

        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (chaUI.H_Potion > 0)
            {
                chaUI.H_Potion -= 1;

                //최대체력 10% 회복
                DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp
                    += 1 / 10f * DataController.Instance.UIManager.GetComponent<MariaChaUI>().IniHp;

                chaUI.H_Text.text = chaUI.H_Potion.ToString();

                DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayHealth();
            }


        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (chaUI.M_Potion > 0)
            {
                chaUI.M_Potion -= 1;

                //최대마나 10% 회복
                DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp +=
                    1 / 10f * DataController.Instance.UIManager.GetComponent<MariaChaUI>().IniMp;

                chaUI.M_Text.text = chaUI.M_Potion.ToString();
                DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayMana();

            }

        }
    }


    public void LattackFire()    //좌클릭후 화살 생성
    {

        GameObject LArrow = Instantiate(LAttackArrow, FirePos.position, FirePos.rotation);
        Destroy(LArrow, 3f);
    }

    void Er_Skill1() //스킬1 생성 메소드
    {

        DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp -= ErMp_Skill1;
        DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayMana();

        InstSkill_1(0, 0); //중앙 발사체


        for (int i = 0; i < DataController.Instance.skill1_Projectile; i++)
        {
            if (DataController.Instance.skill1_Projectile == 2)
            {
                InstSkill_1(1f / 2f, i);
            }
            if (DataController.Instance.skill1_Projectile == 4)
            {
                if (i < 2)
                {
                    InstSkill_1(67.5f / 90f, i);
                }
                else if (i >= 2 && i < 4)
                {
                    InstSkill_1(33.75f / 90f, i);
                }
            }
            if (DataController.Instance.skill1_Projectile == 6)
            {
                if (i < 2)
                {
                    InstSkill_1(67.5f / 90f, i);

                }

                else if (i >= 2 && i < 4)
                {
                    InstSkill_1(((67.5f) * 1 / 3f) / 90f, i);
                }

                else if (i >= 4 && i < 6)
                {
                    InstSkill_1(((67.5f) * 2 / 3f) / 90f, i);
                }
            }
            if (DataController.Instance.skill1_Projectile == 8)
            {
                if (i < 2)
                {
                    InstSkill_1(67.5f / 90f, i);

                }

                else if (i >= 2 && i < 4)
                {
                    InstSkill_1(((67.5f) * 1 / 4f) / 90f, i);
                }

                else if (i >= 4 && i < 6)
                {
                    InstSkill_1(((67.5f) * 2 / 4f) / 90f, i);
                }

                else if (i >= 6 && i < 8)
                {
                    InstSkill_1(((67.5f) * 3 / 4f) / 90f, i);
                }
            }

        }


    }

    void InstSkill_1(float angle, int i)  //스킬1 관련 메소드
    {
        //정면에 점찍고 i가 홀수 짝수이냐에따라 왼쪽 오른쪽대각선에 점찍기
        Vector3 forwardPos = Vector3.Lerp(transform.forward, transform.right * Mathf.Pow(-1, i), angle).normalized;

        Vector3 pos = transform.position + Vector3.up * 1.25f + forwardPos / 6;

        Quaternion right = Quaternion.Euler(new Vector3(0, Mathf.Pow(-1, i) * 90 * angle, 0));

        Quaternion last = Quaternion.Euler(new Vector3(0, 0, 0));

        Quaternion rot = transform.rotation * right * last;




        GameObject SkillArrow_Inst = Instantiate(SkillArrow, pos, rot);
        Rigidbody rigSkillArrow = SkillArrow_Inst.GetComponent<Rigidbody>();
        rigSkillArrow.velocity = forwardPos * DataController.Instance.skill1speed * Time.deltaTime; //skillspeed 데이터 컨트롤러변수로 적용

        Destroy(SkillArrow_Inst, DataController.Instance.destoryarrow1);
    }


    void Er_Skill2() //스킬2 생성 메소드
    {
        DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp -= ErMp_Skill2;
        DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayMana();


        InstSkill_2(0, 0); //중앙 발사체


        for (int i = 0; i < DataController.Instance.skill2_Projectile; i++)
        {
            if (DataController.Instance.skill2_Projectile == 2)
            {
                InstSkill_2(1f / 2f, i);
            }
            if (DataController.Instance.skill2_Projectile == 4)
            {
                if (i < 2)
                {
                    InstSkill_2(67.5f / 90f, i);
                }
                else if (i >= 2 && i < 4)
                {
                    InstSkill_2(33.75f / 90f, i);
                }
            }
            if (DataController.Instance.skill2_Projectile == 6)
            {
                if (i < 2)
                {
                    InstSkill_2(67.5f / 90f, i);

                }

                else if (i >= 2 && i < 4)
                {
                    InstSkill_2(((67.5f) * 1 / 3f) / 90f, i);
                }

                else if (i >= 4 && i < 6)
                {
                    InstSkill_2(((67.5f) * 2 / 3f) / 90f, i);
                }
            }
            if (DataController.Instance.skill2_Projectile == 8)
            {
                if (i < 2)
                {
                    InstSkill_2(67.5f / 90f, i);

                }

                else if (i >= 2 && i < 4)
                {
                    InstSkill_2(((67.5f) * 1 / 4f) / 90f, i);
                }

                else if (i >= 4 && i < 6)
                {
                    InstSkill_2(((67.5f) * 2 / 4f) / 90f, i);
                }

                else if (i >= 6 && i < 8)
                {
                    InstSkill_2(((67.5f) * 3 / 4f) / 90f, i);
                }
            }

        }

    }

    void InstSkill_2(float angle, int i)  //스킬2 관련 메소드
    {
        //정면에 점찍고 i가 홀수 짝수이냐에따라 왼쪽 오른쪽대각선에 점찍기
        Vector3 forwardPos = Vector3.Lerp(transform.forward, transform.right * Mathf.Pow(-1, i), angle).normalized;

        Vector3 pos = transform.position + Vector3.up * 1.25f + forwardPos / 6;

        Quaternion right = Quaternion.Euler(new Vector3(0, Mathf.Pow(-1, i) * 90 * angle, 0));

        Quaternion last = Quaternion.Euler(new Vector3(0, 0, 0));

        Quaternion rot = transform.rotation * right * last;



        GameObject SkillArrow_Inst = Instantiate(Skill_BombArrow, pos, rot);
        Rigidbody rigSkillArrow = SkillArrow_Inst.GetComponent<Rigidbody>();
        rigSkillArrow.velocity = forwardPos * DataController.Instance.skill2speed * Time.deltaTime; //skillspeed 데이터 컨트롤러변수로 적용

        Destroy(SkillArrow_Inst, 3f);
    }

    void Er_skill3()
    {
        DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp -= ErMp_Skill3;
        DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayMana();

        GameObject HealEffect = Instantiate(Skill_Heal, HealPos.position, HealPos.rotation);
        HealEffect.transform.parent = transform;
        Destroy(HealEffect, 2f);

        chaUI.CurHp += DataController.Instance.HP_UP;
        DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayHealth();
        chaUI.CurMp += DataController.Instance.MP_UP;
        DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayMana();

    }


    void Er_Skill4()
    {
        DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp -= ErMp_Skill4;
        DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayMana();

        InstSkill_4(0, 0); //중앙 발사체


        for (int i = 0; i < DataController.Instance.skill4_Projectile; i++)
        {
            if (DataController.Instance.skill4_Projectile == 2)
            {
                InstSkill_4(1f / 2f, i);
            }
            if (DataController.Instance.skill4_Projectile == 4)
            {
                if (i < 2)
                {
                    InstSkill_4(67.5f / 90f, i);
                }
                else if (i >= 2 && i < 4)
                {
                    InstSkill_4(33.75f / 90f, i);
                }
            }
            if (DataController.Instance.skill4_Projectile == 6)
            {
                if (i < 2)
                {
                    InstSkill_4(67.5f / 90f, i);

                }

                else if (i >= 2 && i < 4)
                {
                    InstSkill_4(((67.5f) * 1 / 3f) / 90f, i);
                }

                else if (i >= 4 && i < 6)
                {
                    InstSkill_4(((67.5f) * 2 / 3f) / 90f, i);
                }
            }
            if (DataController.Instance.skill4_Projectile == 8)
            {
                if (i < 2)
                {
                    InstSkill_4(67.5f / 90f, i);

                }

                else if (i >= 2 && i < 4)
                {
                    InstSkill_4(((67.5f) * 1 / 4f) / 90f, i);
                }

                else if (i >= 4 && i < 6)
                {
                    InstSkill_4(((67.5f) * 2 / 4f) / 90f, i);
                }

                else if (i >= 6 && i < 8)
                {
                    InstSkill_4(((67.5f) * 3 / 4f) / 90f, i);
                }
            }

        }
    }


    void InstSkill_4(float angle, int i)
    {
        //정면에 점찍고 i가 홀수 짝수이냐에따라 왼쪽 오른쪽대각선에 점찍기
        Vector3 forwardPos = Vector3.Lerp(transform.forward, transform.right * Mathf.Pow(-1, i), angle).normalized;

        Vector3 pos = transform.position + forwardPos * 8;

        Quaternion right = Quaternion.Euler(new Vector3(0, Mathf.Pow(-1, i) * 90 * angle, 0));

        Quaternion last = Quaternion.Euler(new Vector3(-90, 0, 0));

        Quaternion rot = transform.rotation * right * last;



        GameObject Skilltornado_Inst = Instantiate(SkillTornaodo, pos, rot);
        Destroy(Skilltornado_Inst, DataController.Instance.tr_distace);
    }
}