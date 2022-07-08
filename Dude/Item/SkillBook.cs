using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
    Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Start()
    {
        //AddExplosionForce( È¾Æø¹ß(°¡·Î) ,Æø¹ß½ÃÀÛÀ§Ä¡, Æø¹ß¹Ý°æ, Á¾Æø¹ß(¼¼·Î))
        rb.AddExplosionForce(1000, transform.position, 100f, 500);

    }




}
