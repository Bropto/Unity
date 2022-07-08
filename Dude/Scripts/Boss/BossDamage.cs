using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDamage : MonoBehaviour
{
    public float maxHp;
    float curHp = 0;
    GameObject player;
    public Slider hpSlider;
    public Transform canvas;

    //데미지 받는 UI
    Transform DamageTakenTextPos;
    GameObject DamageTextPref;
    public GameObject MobHpBar;//피통 안보이기


    GameObject bloodEffect;

    Material mat;


    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("PLAYER");
        bloodEffect = Resources.Load<GameObject>("Effect/Blood");
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        curHp = maxHp;
        hpSlider.value = curHp / maxHp;


        MobHpBar = transform.Find("Canvas/HP_Bar").gameObject;
        DamageTakenTextPos = transform.Find("Canvas/DamageTakenTextPos").transform;
        DamageTextPref = Resources.Load<GameObject>("DamageTakenTMP");
    }

    private void Update()
    {
        canvas.forward = Camera.main.transform.forward;
    }

    private void OnCollisionEnter(Collision collision)
    {

        //MariaDamage
        if (DataController.Instance.CharSelectIdx == 0)
        {
            if (collision.collider.CompareTag("WEAPON") && player.GetComponent<MariaSkill>().isSlash)
            {
                StartCoroutine(MeshEffect());
                ShowBloodEffect(collision);
                curHp -= player.GetComponent<MariaSkill>().SlashDamage;
                TakeDamage(player.GetComponent<MariaSkill>().SlashDamage);


                hpSlider.value = curHp / maxHp;

                if (curHp <= 0)
                {
                    GetComponent<BossAI>().state = BossAI.State.DIE;
                }
            }

            if (collision.collider.CompareTag("WEAPON") && player.GetComponent<MariaSkill>().isHSlash)
            {
                StartCoroutine(MeshEffect());
                ShowBloodEffect(collision);
                curHp -= player.GetComponent<MariaSkill>().HSlashDamage;
                TakeDamage(player.GetComponent<MariaSkill>().HSlashDamage);

                hpSlider.value = curHp / maxHp;

                if (curHp <= 0)
                {
                    GetComponent<BossAI>().state = BossAI.State.DIE;
                }
            }


            if (collision.collider.CompareTag("WEAPON") && player.GetComponent<MariaSkill>().isDSlash)
            {
                StartCoroutine(MeshEffect());
                ShowBloodEffect(collision);
                curHp -= DataController.Instance.DsDamage;
                TakeDamage(DataController.Instance.DsDamage);


                hpSlider.value = curHp / maxHp;

                if (curHp <= 0)
                {
                    GetComponent<BossAI>().state = BossAI.State.DIE;
                }
            }

            if (collision.collider.CompareTag("MARIAPS"))
            {
                StartCoroutine(MeshEffect());
                ShowBloodEffect(collision);
                curHp -= DataController.Instance.PsDamage;
                TakeDamage(DataController.Instance.PsDamage);


                hpSlider.value = curHp / maxHp;

                if (curHp <= 0)
                {
                    GetComponent<BossAI>().state = BossAI.State.DIE;
                }
            }
            if (collision.collider.CompareTag("MARIABV"))
            {
                StartCoroutine(MeshEffect());
                ShowBloodEffect(collision);
                curHp -= DataController.Instance.BvDamage;
                TakeDamage(DataController.Instance.BvDamage);

                hpSlider.value = curHp / maxHp;

                if (curHp <= 0)
                {
                    GetComponent<BossAI>().state = BossAI.State.DIE;
                }
            }

        }

        //BradyDamage
        if (DataController.Instance.CharSelectIdx == 1)
        {
            if (collision.collider.CompareTag("M_WEAPON"))
            {
                StartCoroutine(MeshEffect());
                ShowBloodEffect(collision);
                curHp -= collision.gameObject.GetComponent<MagicCtrl>().damage;
                TakeDamage(collision.gameObject.GetComponent<MagicCtrl>().damage);

                collision.gameObject.SetActive(false);
                hpSlider.value = curHp / maxHp;

                if (curHp <= 0)
                {
                    GetComponent<BossAI>().state = BossAI.State.DIE;
                }


            }
            if (collision.collider.CompareTag("EXPLODE"))
            {
                StartCoroutine(MeshEffect());
                ShowBloodEffect(collision);
                curHp -= DataController.Instance.FireBallSplashDamage;
                TakeDamage(DataController.Instance.FireBallSplashDamage);

                hpSlider.value = curHp / maxHp;

                if (curHp <= 0)
                {
                    GetComponent<BossAI>().state = BossAI.State.DIE;
                }
            }
            if (collision.collider.CompareTag("FIREBALL"))
            {
                StartCoroutine(MeshEffect());
                ShowBloodEffect(collision);
                curHp -= DataController.Instance.FireBallDamage;
                TakeDamage(DataController.Instance.FireBallDamage);

                hpSlider.value = curHp / maxHp;

                Destroy(collision.gameObject);

                if (curHp <= 0)
                {
                    GetComponent<BossAI>().state = BossAI.State.DIE;
                }

            }
            if (collision.collider.CompareTag("METEO"))
            {
                StartCoroutine(MeshEffect());
                ShowBloodEffect(collision);
                curHp -= DataController.Instance.Meteo;
                TakeDamage(DataController.Instance.Meteo);

                hpSlider.value = curHp / maxHp;

                Destroy(collision.gameObject);

                if (curHp <= 0)
                {
                    GetComponent<BossAI>().state = BossAI.State.DIE;
                }

            }
            if (collision.collider.CompareTag("METEO_EXPLODE"))
            {
                StartCoroutine(MeshEffect());
                ShowBloodEffect(collision);
                curHp -= DataController.Instance.MeteoSplash;
                TakeDamage(DataController.Instance.MeteoSplash);

                hpSlider.value = curHp / maxHp;

                if (curHp <= 0)
                {
                    GetComponent<BossAI>().state = BossAI.State.DIE;
                }


            }
        }



        //ErikaDamage
        if (DataController.Instance.CharSelectIdx == 2)
        {
            if (collision.collider.CompareTag("ER_LATTACK"))
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                curHp -= DataController.Instance.skill1_Damage; //기본공격 데미지 추가필요
                TakeDamage(DataController.Instance.skill1_Damage);

                hpSlider.value = curHp / maxHp;

                if (curHp <= 0)
                {
                    GetComponent<BossAI>().state = BossAI.State.DIE;
                }
            }

            if (collision.collider.CompareTag("ER_BOMB"))
            {
                StartCoroutine(MeshEffect());
                ShowBloodEffect(collision);
                curHp -= DataController.Instance.skill2_Damage;
                TakeDamage(DataController.Instance.skill2_Damage);

                hpSlider.value = curHp / maxHp;

                if (curHp <= 0)
                {
                    GetComponent<BossAI>().state = BossAI.State.DIE;
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //MariaDamage
        if (DataController.Instance.CharSelectIdx == 0)
        {
            if (other.gameObject.CompareTag("MARIAWS"))
            {
                StartCoroutine(MeshEffect());
                curHp -= DataController.Instance.WsDamage;
                TakeDamage(DataController.Instance.WsDamage);

                hpSlider.value = curHp / maxHp;

                if (curHp <= 0)
                {
                    GetComponent<BossAI>().state = BossAI.State.DIE;
                }
            }
        }

        //BradyDamage
        if (DataController.Instance.CharSelectIdx == 1)
        {
            if (other.gameObject.CompareTag("CHAIN"))
            {
                StartCoroutine(MeshEffect());
                curHp -= DataController.Instance.ChainDamage;
                TakeDamage(DataController.Instance.ChainDamage);

                hpSlider.value = curHp / maxHp;

                if (curHp <= 0)
                {
                    GetComponent<BossAI>().state = BossAI.State.DIE;
                }
            }
        }


        //ErikaDamage
        if (DataController.Instance.CharSelectIdx == 2)
        {
            if (other.gameObject.CompareTag("ER_TORNADO"))
            {
                StartCoroutine(MeshEffect());
                curHp -= DataController.Instance.skill4_Damage;
                TakeDamage(DataController.Instance.skill4_Damage);

                hpSlider.value = curHp / maxHp;

                if (curHp <= 0)
                {
                    GetComponent<BossAI>().state = BossAI.State.DIE;
                }
            }

            if (other.gameObject.CompareTag("ER_SKILLARROW"))
            {
                StartCoroutine(MeshEffect());
                curHp -= DataController.Instance.skill1_Damage;
                TakeDamage(DataController.Instance.skill1_Damage);

                hpSlider.value = curHp / maxHp;

                if (curHp <= 0)
                {
                    GetComponent<BossAI>().state = BossAI.State.DIE;
                }
            }
        }
    }

    void ShowBloodEffect(Collision coll)
    {

        ContactPoint cp = coll.GetContact(0);
        Quaternion rot = Quaternion.LookRotation(-cp.normal);
        GameObject blood = Instantiate(bloodEffect, cp.point, rot);
        Destroy(blood, 2f);
    }

    IEnumerator MeshEffect()
    {
        mat.color = Color.red; 
        yield return new WaitForSeconds(0.3f);
        if(curHp>0)
        {
            mat.color = Color.white;
        }
        else
        {
            mat.color = Color.clear;
        }    
    }


    public void TakeDamage(float damage1)
    {
        GameObject DamageTakenText = Instantiate(DamageTextPref); // 생성할 텍스트 오브젝트
        DamageTakenText.GetComponent<MonsterDamageTaken>().damage = damage1; // 데미지 전달
        DamageTakenText.transform.parent = canvas.transform;
        DamageTakenText.transform.position = DamageTakenTextPos.position;
    }

}