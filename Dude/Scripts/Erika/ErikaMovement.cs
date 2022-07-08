using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErikaMovement : MonoBehaviour
{
    Transform playerTr;
    Animator animator;

    readonly int hashIsRun = Animator.StringToHash("IsRun");
    readonly int hashIsJump = Animator.StringToHash("IsJump");


    public float moveSpeed = 10f;
    public float turnSpeed = 500f;


    void Start()
    {
        playerTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");




        Vector3 moveDir = Vector3.forward * v + Vector3.right * h;
        playerTr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);

        if (DataController.Instance.UIManager.GetComponent<MariaSkillTreeUI>().isClick_K == false
              && DataController.Instance.UIManager.GetComponent<ESCManager>().A_ESC_Window == false)
        {
            playerTr.Rotate(Vector3.up * r * turnSpeed * Time.deltaTime);
        }
        PlayerAnim(h, v);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            animator.SetTrigger(hashIsJump);

        }


    }

    void PlayerAnim(float h, float v)
    {
        if (Mathf.Approximately(h, 0) && Mathf.Approximately(v, 0))
        {
            animator.SetBool(hashIsRun, false);
        }
        else
        {
            animator.SetBool(hashIsRun, true);
        }
        animator.SetFloat("horizontal", h);
        animator.SetFloat("vertical", v);

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

}

