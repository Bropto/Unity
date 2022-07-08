using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpCam : MonoBehaviour
{
    public GameObject prefHpBar;
    public GameObject canvas;

    RectTransform hpBar;

    public float height = 1.7f;

    private void Start()
    {
        hpBar = Instantiate(prefHpBar, canvas.transform). GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 _hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = _hpBarPos;
    }
}
