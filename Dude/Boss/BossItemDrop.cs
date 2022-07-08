using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossItemDrop : MonoBehaviour
{
    public GameObject bossItemBox;
    public GameObject bossKey;

    Transform bossTr;

 

    

    private void Awake()
    {
        
        bossTr = GetComponent<Transform>();

    }



    public void MakeItemBox()
    {
        GameObject itemBox = Instantiate(bossItemBox);
        itemBox.transform.position = bossTr.transform.position + new Vector3(0, 0.5f, 0);
        //높이수정 1>>0.5 04.28

        Destroy(itemBox, 5f);
        
    }


    public void MakeKey()
    {
        GameObject itemKey = Instantiate(bossKey);
        itemKey.transform.position = bossTr.transform.position + new Vector3(0, 1, 0);
                                                                 //높이수정 4>>1 04.26
    }

  
}
