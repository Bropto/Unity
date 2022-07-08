using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TeleportCtrl : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if ( other.CompareTag("STAGE1_TP"))
        {
            SceneManager.LoadScene("Stage1Scene");
        }
        if (other.CompareTag("BOSS1_TP"))
        {
            SceneManager.LoadScene("Boss1Scene");
        }

        if (other.CompareTag("STAGE2_TP"))
        {
            SceneManager.LoadScene("Stage2Scene");

        }
        if (other.CompareTag("BOSS2_TP"))
        {
            SceneManager.LoadScene("Boss2Scene");
        }

        if (other.CompareTag("STAGE3_TP"))
        {
            SceneManager.LoadScene("Stage3Scene");

        }
        if (other.CompareTag("BOSS3_TP"))
        {
            SceneManager.LoadScene("Boss3Scene");
        }





    }
}
