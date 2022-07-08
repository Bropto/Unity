using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    GameObject player;
    Transform camTr;

    float distance = 6;
    float height =3f;
    float damping = 0.01f;
    float targetOffset = 2f;

    Vector3 velocity = Vector3.zero;

    void Start()
    {
        camTr = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("PLAYER");
    }
    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 pos = player.transform.position + (-player.transform.forward * distance) + (Vector3.up * height);

            //camTr.position = Vector3.Slerp(camTr.position, pos, Time.deltaTime * damping);
            //camTr.position = Vector3.SmoothDamp(camTr.position, pos, ref velocity, damping);

            camTr.position = pos;

            //Å¸°ÙÀÇ ÇÇº¿À§Ä¡ º»´Ù
            //camTr.LookAt(player.transform.position + (player.transform.up * targetOffset));
            camTr.LookAt(player.transform.position + (Vector3.up * targetOffset));

        }
    }
}
