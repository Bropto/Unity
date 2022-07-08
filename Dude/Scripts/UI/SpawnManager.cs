using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    float createTime = 2f;
    public bool isGameOver = false;
    public int stageNum;
    public GameObject portal;
    int numOfSpawn = 0;

    public GameObject stage1FamilyPref;
    public GameObject stage2FamilyPref;
    public GameObject stage3FamilyPref;

    [Header("text/text/text2를 넣으시오")]
    public Text Mon_text;
    public GameObject MonText;//몬스터 남은수
    public GameObject InTpText;//포탈 생성 text.
    bool IsInTp = false;
    

    void Start()
    {
        
        portal = GameObject.Find("BossScene_Enter");
        portal.SetActive(false);

        if (spawnPoints.Length > 0)
        {
            StartCoroutine(CreateEnemy());
            StartCoroutine(CheckEnemy());
            
        }
    }


    IEnumerator CreateEnemy()
    {
        while (!isGameOver)
        {
            int enemyCount = GameObject.FindGameObjectsWithTag("ENEMY").Length;
            if (enemyCount <= 0 && stageNum == 1)
            {
                if (numOfSpawn == 1) //3번 스폰된거 다 잡으면 포탈 보이게 세팅
                {
                    portal.SetActive(true);
                    InTpText.SetActive(true);
                    MonText.SetActive(false);
                    Destroy(InTpText, 3f);
                    IsInTp = true;
                }
                numOfSpawn += 1;

                yield return new WaitForSeconds(createTime);

                for (int i = 0; i < spawnPoints.Length; i++)
                    Instantiate(stage1FamilyPref, spawnPoints[i].position, spawnPoints[i].rotation);

            }

            else if (enemyCount <= 0 && stageNum == 2)
            {
                if (numOfSpawn == 1) //3번 스폰된거 다 잡으면 포탈 보이게 세팅
                {
                    portal.SetActive(true);
                    InTpText.SetActive(true);
                    MonText.SetActive(false);
                    Destroy(InTpText, 3f);
                    IsInTp = true;
                }
                numOfSpawn += 1;

                yield return new WaitForSeconds(createTime);
                for (int i = 0; i < spawnPoints.Length; i++)
                    Instantiate(stage2FamilyPref, spawnPoints[i].position, spawnPoints[i].rotation);

            }

            else if (enemyCount <= 0 && stageNum == 3)
            {
                if (numOfSpawn == 1) //4번 스폰된거 다 잡으면 포탈 보이게 세팅
                {
                    portal.SetActive(true);
                    InTpText.SetActive(true);
                    MonText.SetActive(false);
                    Destroy(InTpText, 3f);
                    IsInTp = true;
                }
                numOfSpawn += 1;

                yield return new WaitForSeconds(createTime);
                for (int i = 0; i < spawnPoints.Length; i++)
                    Instantiate(stage3FamilyPref, spawnPoints[i].position, spawnPoints[i].rotation);

            }

            else
                yield return new WaitForSeconds(createTime);
        }
    }


    IEnumerator CheckEnemy()
    {
        while (!IsInTp)
        {
            int enemyCount2 = GameObject.FindGameObjectsWithTag("ENEMY").Length;


            Mon_text.text = "남은 적 : " + enemyCount2;

            yield return new WaitForSeconds(0.01f);
        }

    }
}
