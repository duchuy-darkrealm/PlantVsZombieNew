using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DItemHolder : MonoBehaviour,IPointerDownHandler, IPointerUpHandler,IDragHandler
{
    public PlantStat stat;
    public RectTransform cooldownRect;
    public TextMeshProUGUI textPrice;

    [HideInInspector]
    public float count;

    private void Start()
    {
        count = stat.startCount;
        cooldownRect.anchoredPosition = new Vector2(0, count / stat.cooldown * 50);
        textPrice.text = stat.price.ToString();
        GetComponent<Image>().sprite = stat.sprite;
    }

    public virtual void Update()
    {
        count += Time.deltaTime;

        if (count < stat.cooldown)
        {
            cooldownRect.anchoredPosition = new Vector2(0, count / stat.cooldown * 50);
        }

        if (DGameSystem.gold >= stat.price)
        {
            GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            GetComponent<Image>().color = new Color32(120, 120, 120, 255);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (count < stat.cooldown || DGameSystem.gold < stat.price)
            return;

        DGameSystem.virtualRect.position = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (count < stat.cooldown )
            return;

        if (DGameSystem.gold < stat.price)
        {
            return;
        }

        DGameSystem.virtualImage.enabled = true;
        DGameSystem.virtualImage.sprite = stat.sprite;
        DGameSystem.virtualRect.position = eventData.position;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (count < stat.cooldown || DGameSystem.gold < stat.price)
            return;

        Vector3 position = Camera.main.ScreenToWorldPoint(eventData.position);
        position.z = 0;
        DGameSystem.virtualImage.enabled = false;

        bool result = DGameSystem.instance.PlantTree(stat, position);

        if (result == true)
        {
            count = 0;
        }
    }
}
