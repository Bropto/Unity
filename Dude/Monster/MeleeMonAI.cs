using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MeleeMonAI : MonoBehaviour
{
    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        FLEE,
        DAMAGE,
        DIE
    }

    public State state = State.IDLE;
    float traceDist = 100.0f;
    float attackDist = 1.5f;
    public bool isDie = false;

    public float damage = 10f;
    public float hp = 45f;
    float iniHp;

    MonsterItemDrop MonItemDrop;

    Transform monsterTr;
    Transform playerTr;
    NavMeshAgent agent;
    Animator anim;

    GameObject canvasEffect;
    GameObject bloodEffect; //피 효과
    Material mat;           //피격 색변환효과

    //피격 시 UI들
    public Transform canvas;
    public Image hpBar;

    //데미지 받는 UI
    Transform DamageTakenTextPos;
    GameObject DamageTextPref;
    GameObject MobHpBar;//피통 안보이기
    GameObject MobMinimapPoint;//미니맵 점 안보이기

    private readonly int hashMove = Animator.StringToHash("IsMove");
    private readonly int hashAttack = Animator.StringToHash("Attack");
    private readonly int hashDamage = Animator.StringToHash("Damage");
    private readonly int hashDamageIdx = Animator.StringToHash("DamageIdx");
    private readonly int hashDie = Animator.StringToHash("Die");
    private readonly int hashDieIdx = Animator.StringToHash("DieIdx");

    void Start()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        MonItemDrop = GetComponent<MonsterItemDrop>();
        //밑에3개 피격효과들
        canvasEffect = transform.Find("Canvas").gameObject;

        MobHpBar = transform.Find("Canvas/HpBar").gameObject;
        DamageTakenTextPos = transform.Find("Canvas/DamageTakenTextPos").transform;
        DamageTextPref = Resources.Load<GameObject>("DamageTakenTMP");
        MobMinimapPoint = transform.Find("Mini_Icon").gameObject;

        bloodEffect = Resources.Load<GameObject>("Effect/Blood");
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        iniHp = hp;


        // agent.destination = playerTr.position;
        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());
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

            if (collision.collider.CompareTag("WEAPON") && playerTr.GetComponent<MariaSkill>().isSlash)
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                hp -= playerTr.GetComponent<MariaSkill>().SlashDamage;
                TakeDamage(playerTr.GetComponent<MariaSkill>().SlashDamage);
                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }

            if (collision.collider.CompareTag("WEAPON") && playerTr.GetComponent<MariaSkill>().isHSlash)
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                hp -= playerTr.GetComponent<MariaSkill>().HSlashDamage;
                TakeDamage(playerTr.GetComponent<MariaSkill>().HSlashDamage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }


            if (collision.collider.CompareTag("WEAPON") && playerTr.GetComponent<MariaSkill>().isDSlash)
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.DsDamage;
                TakeDamage(DataController.Instance.DsDamage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }

            if (collision.collider.CompareTag("MARIAPS"))
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.PsDamage;
                TakeDamage(DataController.Instance.PsDamage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }
            if (collision.collider.CompareTag("MARIABV"))
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.BvDamage;
                TakeDamage(DataController.Instance.BvDamage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }
        }

        //BradyDamage
        if (DataController.Instance.CharSelectIdx == 1)
        {
            if (collision.collider.CompareTag("M_WEAPON"))
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());

                hp -= collision.gameObject.GetComponent<MagicCtrl>().damage;
                TakeDamage(collision.gameObject.GetComponent<MagicCtrl>().damage);

                collision.gameObject.SetActive(false);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }

            }
            if (collision.collider.CompareTag("EXPLODE"))
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.FireBallSplashDamage;
                TakeDamage(DataController.Instance.FireBallSplashDamage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }
            if (collision.collider.CompareTag("FIREBALL"))
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.FireBallDamage;
                TakeDamage(DataController.Instance.FireBallDamage);

                Destroy(collision.gameObject);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }

            }
            if (collision.collider.CompareTag("METEO"))
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());

                hp -= DataController.Instance.Meteo;
                TakeDamage(DataController.Instance.Meteo);

                Destroy(collision.gameObject);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }

            }
            if (collision.collider.CompareTag("METEO_EXPLODE"))
            {

                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.MeteoSplash;
                TakeDamage(DataController.Instance.MeteoSplash);



                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
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
                hp -= DataController.Instance.skill1_Damage; //기본공격 데미지 추가필요
                TakeDamage(DataController.Instance.skill1_Damage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }

            if (collision.collider.CompareTag("ER_BOMB"))
            {
                ShowBloodEffect(collision);
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.skill2_Damage;
                TakeDamage(DataController.Instance.skill2_Damage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }
        }


        hpBar.fillAmount = hp / iniHp;

    }
    private void OnTriggerEnter(Collider other)
    {
        //MariaDamage
        if (DataController.Instance.CharSelectIdx == 0)
        {

            if (other.gameObject.CompareTag("MARIAWS"))
            {
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.WsDamage;
                TakeDamage(DataController.Instance.WsDamage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }
        }

        //BradyDamage
        if (DataController.Instance.CharSelectIdx == 1)
        {
            if (other.gameObject.CompareTag("CHAIN"))
            {
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.ChainDamage;
                TakeDamage(DataController.Instance.ChainDamage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }
        }

        //ErikaDamage
        if (DataController.Instance.CharSelectIdx == 2)
        {
            if (other.gameObject.CompareTag("ER_TORNADO"))
            {
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.skill4_Damage;
                TakeDamage(DataController.Instance.skill4_Damage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }

            if (other.gameObject.CompareTag("ER_SKILLARROW"))
            {
                StartCoroutine(MeshEffect());
                hp -= DataController.Instance.skill1_Damage;
                TakeDamage(DataController.Instance.skill1_Damage);

                if (hp <= 0)
                {
                    state = State.DIE;
                }
                else
                {
                    anim.SetTrigger(hashDamage);
                    anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
                }
            }
        }



        hpBar.fillAmount = hp / iniHp;
    }

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.3f);

            if (state == State.DIE)
                yield break;

            if (hp < 30)
            {
                agent.speed = 7;
                agent.acceleration = 5;
                anim.speed = 1.5f;
            }

            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            if (distance <= attackDist)
            {
                state = State.ATTACK;

            }
            else if (distance <= traceDist)
            {
                int Moving = Random.Range(0, 3);
                if (Moving == 2)
                {
                    state = State.FLEE;
                }
                else if (Moving < 2)
                {
                    state = State.TRACE;
                }
            }
            else
            {
                state = State.IDLE;
            }
        }
    }


    IEnumerator MonsterAction()
    {

        while (!isDie)
        {
            switch (state)
            {
                case State.IDLE:
                    agent.isStopped = true;
                    anim.SetBool(hashMove, false);
                    break;
                case State.TRACE:
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;
                    anim.SetBool(hashMove, true);
                    anim.SetBool(hashAttack, false);
                    break;
                case State.FLEE:
                    agent.SetDestination(playerTr.position + new Vector3(Random.Range(-30, 30), 0, Random.Range(-30, 30)));
                    agent.isStopped = false;
                    anim.SetBool(hashMove, true);
                    anim.SetBool(hashAttack, false);
                    break;
                case State.ATTACK:

                    attackDist = 1.5f;
                    anim.SetBool(hashAttack, true);
                    hp += 10;
                    break;
                case State.DIE:
                    isDie = true;
                    agent.isStopped = true;
                    anim.SetTrigger(hashDie);
                    anim.SetInteger(hashDieIdx, Random.Range(0, 2));
                    GetComponent<CapsuleCollider>().enabled = false;
                    MobHpBar.SetActive(false);      //피통 없애기코드 0610;
                    gameObject.tag = "Untagged";
                    MobMinimapPoint.SetActive(false);      //미니맵 점 없애기코드
                    DropItem();
                    Destroy(gameObject, 3f);
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void DropItem()
    {
        MonItemDrop.MakeItem();
    }


    //피격효과 함수들
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
        if (hp > 0)
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
        DamageTakenText.transform.parent = canvasEffect.transform;
        DamageTakenText.transform.position = DamageTakenTextPos.position;
    }
}
