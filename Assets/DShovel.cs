using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DShovel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Sprite shovelSprite;

    private void Start()
    {
        GetComponent<Image>().sprite = shovelSprite;
    }

    public void OnDrag(PointerEventData eventData)
    {
        DGameSystem.virtualRect.position = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DGameSystem.virtualImage.enabled = true;
        DGameSystem.virtualImage.sprite = shovelSprite;
        DGameSystem.virtualRect.position = eventData.position;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(eventData.position);
        position.z = 0;
        DGameSystem.virtualImage.enabled = false;

        DGameSystem.instance.RemoveTree(position);
    }

}
