using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCtrl : MonoBehaviour
{
    Shield shieldCtrl;

    private void Start()
    {
        shieldCtrl = GetComponent<Shield>();
    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("E_ATTACK"))
        {
            DataController.Instance.sheildHp -= 10;
            if (DataController.Instance.sheildHp <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        else if (coll.collider.CompareTag("A_ATTACK"))
        {
            DataController.Instance.sheildHp -= coll.gameObject.GetComponent<MagicCtrl>().damage;

            if (DataController.Instance.sheildHp <= 0)
            {
                coll.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
        else if (coll.collider.CompareTag("M_ATTACK"))
        {
            DataController.Instance.sheildHp -= coll.gameObject.GetComponent<MagicCtrl>().damage;

            if (DataController.Instance.sheildHp <= 0)
            {
                coll.gameObject.SetActive(false);
                Destroy(gameObject);
                shieldCtrl.shieldbool = false;
            }
        }
    }

}
