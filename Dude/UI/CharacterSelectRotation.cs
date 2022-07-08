using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CharacterSelectRotation : MonoBehaviour, IDragHandler, IEndDragHandler

{
    public GameObject character;
    float speed = 5000;
    Vector3 rot;
    Vector3 origin;
    void Start()
    {
        rot = character.transform.localEulerAngles;
        origin = rot;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rot.y += Input.GetAxis("Mouse X") * speed * Time.deltaTime;
        character.transform.localEulerAngles = -rot;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        character.transform.localEulerAngles = origin;
        rot = origin;
    }

}
