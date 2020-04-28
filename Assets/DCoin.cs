using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DCoin : MonoBehaviour, IPointerDownHandler
{
    enum STATE { FALLING, GROUND};

    public float FALL_SPEED = 1f;
    public float VERTICAL_DESTINATION = -6f;
    public int GOLD_VALUE = 25;

    public SpriteRenderer sprite1;
    public SpriteRenderer sprite2;

    public float EXIST_TIME = 5f;
    public float VANISH_SPEED = 3f;

    float count = 0;
    STATE state;

    private void OnEnable()
    {
        Color color = sprite1.color;
        color.a = 1;
        sprite1.color = color;
        sprite2.color = color;
        state = STATE.FALLING;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DGameSystem.AddGold(GOLD_VALUE);
        DGameSystem.LoadPool("StarEffect",transform.position);

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (state == STATE.FALLING)
        {
            transform.position -= new Vector3(0, FALL_SPEED * Time.deltaTime);
            if (transform.position.y < VERTICAL_DESTINATION)
            {
                count = EXIST_TIME;
                state = STATE.GROUND;
            }
        }
        else
        {
            // Stay on the ground
            count -= Time.deltaTime;
            if (count < 0)
            {
                gameObject.SetActive(false);
                //Vanish();
            }
        }
    }

    public void Vanish()
    {
        Color color = sprite1.color;
        float a = color.a;
        a -= VANISH_SPEED * Time.deltaTime;
        if (a < 0)
        {
            gameObject.SetActive(false);
        }  
        else
        {
            color.a = a;
            sprite1.color = color;
            sprite2.color = color;
        }
    }
}
