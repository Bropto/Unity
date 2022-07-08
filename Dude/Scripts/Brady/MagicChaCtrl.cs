using System.Collections;
using UnityEngine;

public class MagicChaCtrl : MonoBehaviour
{
    Transform playerTr;
    Animator anim;


    // 애니메이션 관련
    readonly int hashIsJump = Animator.StringToHash("Jump");
    readonly int hashAttack = Animator.StringToHash("Attack");
    readonly int hashDamage = Animator.StringToHash("Damage");
    readonly int hashDamageIdx = Animator.StringToHash("DamageIdx");
    readonly int hashDie = Animator.StringToHash("Die");
    readonly int hashTeleport = Animator.StringToHash("Teleport");
    readonly int hashShield = Animator.StringToHash("Shield");
    readonly int hashFireBall = Animator.StringToHash("FireBall");
    readonly int hashMetoe = Animator.StringToHash("Meteo");
    readonly int hashChain = Animator.StringToHash("Chain");

    //캐릭터 움직임 관련
    public float moveSpeed = 10f;
    public float turnSpeed = 500f;

    //발사 위치 관련
    public Transform firePos;
    public GameObject magicBall;
    public GameObject FireBall_A;

    float FireBallMp = 10;
    float MeteoMp = 20;
    float ShieldMp = 15;
    float ChainMp = 25;

    public Transform MeteoPos;
    public GameObject MeteoFire;

    public GameObject eTeleport;
    public Transform TelPos;

    public GameObject Chain;

    //Hp, Mp 관련
    MariaChaUI chaUi;
    CoolTimeManager coolTimeManager;

    //스킬트리 관련
    BradySkillTreeUI B_Skill;

    void Start()
    {

        playerTr = GetComponent<Transform>();
        anim = GetComponent<Animator>();

        chaUi = DataController.Instance.UIManager.GetComponent<MariaChaUI>();
        B_Skill = GameObject.FindObjectOfType<BradySkillTreeUI>().GetComponent<BradySkillTreeUI>();
        coolTimeManager = DataController.Instance.UIManager.GetComponent<CoolTimeManager>();

    }


    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("ITEM_SKILL1") || coll.gameObject.CompareTag("ITEM_SKILL2")
            || coll.gameObject.CompareTag("ITEM_SKILL3") || coll.gameObject.CompareTag("ITEM_SKILL4")
            || coll.gameObject.CompareTag("ITEM_HP") || coll.gameObject.CompareTag("ITEM_MP")
            || coll.gameObject.CompareTag("ITEM_KEY"))
        {
            Destroy(coll.gameObject);
        }

        if (coll.gameObject.CompareTag("ITEM_HP"))
        {
            chaUi.H_Potion += 1;
            chaUi.H_Text.text = chaUi.H_Potion.ToString();

        }

        if (coll.gameObject.CompareTag("ITEM_MP"))
        {
            chaUi.M_Potion += 1;
            chaUi.M_Text.text = chaUi.M_Potion.ToString();

        }

        if (coll.gameObject.CompareTag("ITEM_SKILL1"))
        {
            B_Skill.skillPoint1 += 1;
            B_Skill.T_skillPoint1.text = string.Format("{0:00}", B_Skill.skillPoint1);
        }

        if (coll.gameObject.CompareTag("ITEM_SKILL2"))
        {
            B_Skill.skillPoint2 += 1;
            B_Skill.T_skillPoint2.text = string.Format("{0:00}", B_Skill.skillPoint2);
        }

        if (coll.gameObject.CompareTag("ITEM_SKILL3"))
        {
            B_Skill.skillPoint3 += 1;
            B_Skill.T_skillPoint3.text = string.Format("{0:00}", B_Skill.skillPoint3);
        }

        if (coll.gameObject.CompareTag("ITEM_SKILL4"))
        {
            B_Skill.skillPoint4 += 1;
            B_Skill.T_skillPoint4.text = string.Format("{0:00}", B_Skill.skillPoint4);
        }

        if (coll.gameObject.CompareTag("ITEM_KEY"))
        {
            DataController.Instance.GetItemKey();

        }
        if (coll.collider.CompareTag("E_ATTACK"))
        {
            chaUi.CurHp -= 10;

            if (chaUi.CurHp <= 0)
            {
                anim.SetTrigger(hashDie);
                coll.gameObject.SetActive(false);
                DataController.Instance.GameOver();
            }
            else
            {
                anim.SetTrigger(hashDamage);
                anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
            }
        }
        else if (coll.collider.CompareTag("A_ATTACK"))
        {
            chaUi.CurHp -= coll.gameObject.GetComponent<MagicCtrl>().damage;

            if (chaUi.CurHp <= 0)
            {
                anim.SetTrigger(hashDie);
                coll.gameObject.SetActive(false);
                DataController.Instance.GameOver();

            }
            else
            {
                anim.SetTrigger(hashDamage);
                anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
            }
            Destroy(coll.gameObject);
        }
        else if (coll.collider.CompareTag("M_ATTACK"))
        {
            chaUi.CurHp -= coll.gameObject.GetComponent<MagicCtrl>().damage;

            if (chaUi.CurHp <= 0)
            {
                anim.SetTrigger(hashDie);
                GetComponent<CapsuleCollider>().enabled = false;
                DataController.Instance.GameOver();

            }
            else
            {
                anim.SetTrigger(hashDamage);
                anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
            }
            Destroy(coll.gameObject);
        }
        if (coll.collider.CompareTag("POISONARROW"))
        {
            StartCoroutine(PoisonDamage());
        }


        chaUi.DisplayHealth();

    }

    IEnumerator PoisonDamage()
    {

        for (int i = 0; i < 5; i++)
        {
            chaUi.CurHp -= 50;
            yield return new WaitForSeconds(1f); ;
        }
    }


    private void OnTriggerEnter(Collider coll)
    {


        if (coll.gameObject.CompareTag("BOSSATTACKA"))
        {
            chaUi.CurHp -= 100;
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayHealth(); //맞을 때마다 HP UI 업데이트
            if (chaUi.CurHp <= 0)
            {
                anim.SetTrigger(hashDie);
                coll.gameObject.SetActive(false);
                DataController.Instance.GameOver();

            }
            else
            {
                anim.SetTrigger(hashDamage);
                anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
            }
        }

        if (coll.gameObject.CompareTag("BOSSATTACKB"))
        {
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp -= 200;
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayHealth(); //맞을 때마다 HP UI 업데이트
            if (chaUi.CurHp <= 0)
            {
                anim.SetTrigger(hashDie);
                coll.gameObject.SetActive(false);
                DataController.Instance.GameOver();

            }
            else
            {
                anim.SetTrigger(hashDamage);
                anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
            }
        }

        if (coll.gameObject.CompareTag("BOSSATTACKC"))
        {
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurHp -= 100;
            DataController.Instance.UIManager.GetComponent<MariaChaUI>().DisplayHealth(); //맞을 때마다 HP UI 업데이트
            if (chaUi.CurHp <= 0)
            {
                anim.SetTrigger(hashDie);
                coll.gameObject.SetActive(false);
                DataController.Instance.GameOver();

            }
            else
            {
                anim.SetTrigger(hashDamage);
                anim.SetInteger(hashDamageIdx, Random.Range(0, 2));
            }
        }


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("DEBUFFZONE"))
        {
            if (moveSpeed > 6)
            {
                moveSpeed -= 5;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DEBUFFZONE"))
        {
            moveSpeed = 10;
        }
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");

        Vector3 moveDir = Vector3.forward * v + Vector3.right * h;
        playerTr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
        playerTr.Rotate(Vector3.up * r * turnSpeed * Time.deltaTime);

        PlayerAnim(h, v);

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger(hashAttack);
        }

        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetTrigger(hashTeleport);
            StartCoroutine(Teleport());
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger(hashIsJump);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= FireBallMp
            && coolTimeManager.isCool1 == false)
        {
            anim.SetTrigger(hashFireBall);
            chaUi.CurMp -= FireBallMp;
            coolTimeManager.isCool1 = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= MeteoMp
            && coolTimeManager.isCool2 == false)
        {
            anim.SetTrigger(hashMetoe);
            chaUi.CurMp -= MeteoMp;
            coolTimeManager.isCool2 = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= ShieldMp
            && coolTimeManager.isCool3 == false)
        {
            anim.SetTrigger(hashShield);
            chaUi.CurMp -= ShieldMp;
            coolTimeManager.isCool3 = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && DataController.Instance.UIManager.GetComponent<MariaChaUI>().CurMp >= ChainMp
            && coolTimeManager.isCool4 == false)
        {
            chaUi.CurMp -= ChainMp;
            anim.SetTrigger(hashChain);
            coolTimeManager.isCool4 = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (chaUi.H_Potion > 0)
            {
                chaUi.H_Potion -= 1;
                chaUi.CurHp += 1 / 10f * chaUi.IniHp;
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (chaUi.M_Potion > 0)
            {
                chaUi.M_Potion -= 1;
                chaUi.CurMp += 1 / 10f * chaUi.IniMp;
            }
        }
        chaUi.DisplayHealth();
        chaUi.DisplayMana();

        chaUi.H_Text.text = chaUi.H_Potion.ToString();
        chaUi.M_Text.text = chaUi.M_Potion.ToString();


    }

    void PlayerAnim(float h, float v)
    {
        if (Mathf.Approximately(h, 0) && Mathf.Approximately(v, 0))
        {
            anim.SetBool("IsRun", false);
        }
        else
        {
            anim.SetBool("IsRun", true);
        }
        anim.SetFloat("xDir", h);
        anim.SetFloat("yDir", v);
    }
    void Fire()
    {

        GameObject _magicBall = Instantiate(magicBall, firePos.position, firePos.rotation);
        Rigidbody rigidWindScar = _magicBall.GetComponent<Rigidbody>();
        rigidWindScar.velocity = firePos.forward * 30;

        Destroy(_magicBall, 2);
    }
    void FireBall()
    {

        GameObject _FireBall = Instantiate(FireBall_A, firePos.position, firePos.rotation);
        Rigidbody Fireball = _FireBall.GetComponent<Rigidbody>();
        Fireball.velocity = firePos.forward * 30;

        Destroy(_FireBall, 2);
    }
    public void Meteo()
    {
        for (int i = 0; i < DataController.Instance.Meteo_Proj; i++)
        {
            int a = Random.Range(-1, 3);
            if (i == 0)
            {
                a = 0;
            }
            GameObject _Meteo = Instantiate(MeteoFire, MeteoPos.position + new Vector3(a * 5, a * 5, a * 5), MeteoPos.rotation);
            Destroy(_Meteo, 3f);
        }
    }
    void Teleport_E()
    {
        GameObject _Teleport = Instantiate(eTeleport, TelPos.position, TelPos.rotation);
        Destroy(_Teleport, 2f);
    }
    void ChainLightning()
    {
        for (int i = 0; i < DataController.Instance.Chain_Proj; i++)
        {
            float a = 0;
            if (i % 2 == 1)
                a = i * -1;
            else
                a = i;
            GameObject _Chain = Instantiate(Chain, firePos.position + new Vector3(2 * a, 0, 2 * a), firePos.rotation);
            Rigidbody ChainRig = _Chain.GetComponent<Rigidbody>();
            ChainRig.velocity = firePos.forward * 30;
            Destroy(_Chain, DataController.Instance.Chain_Time);
        }
    }


    IEnumerator Teleport()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * 5);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * 5);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * 5);
        }
        else
        {
            transform.Translate(Vector3.forward * 5);
        }

        chaUi.CurMp -= 15;
        yield return null;
    }

}