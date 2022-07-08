using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageUI : MonoBehaviour
{
    public GameObject textObj;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(textObj, 5f);
    }

 
}
