using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    GameObject stagePortal;

    [Header("text/text1³Ö±â")]
    public GameObject WelBoss;
    public GameObject InTPBoss;

    void Start()
    {
        stagePortal = GameObject.Find("StagePortal");
        stagePortal.SetActive(false);
        StartCoroutine(CheckEnemy());
        Destroy(WelBoss, 3f);
    }


    IEnumerator CheckEnemy()
    {
        while (true)
        {
            int enemyCount = GameObject.FindGameObjectsWithTag("ENEMY").Length;

            if(enemyCount == 0)
            {
                stagePortal.SetActive(true);
                InTPBoss.SetActive(true);
                yield return new WaitForSeconds(5);

                break;
            }

            else
            {
                yield return new WaitForSeconds(0.5f);
            }


        }
        yield return null;
    }
}
