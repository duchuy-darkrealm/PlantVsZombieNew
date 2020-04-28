using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(SpriteRenderer))]
public class DAnimator : MonoBehaviour
{
    // Prior Animation is animation that occur in priority.
    // After finish, prior animation will jump to normal animation.
    // So that say, prior animation will occur only one time, then jump to normal animation.
    public bool active = true;
    public bool attacking = false;

    public SpriteRenderer hatRenderer;
    public SpriteRenderer spriteRenderer;
    public Sprite test;

    public Sprite[] spritesheet;
    public Sprite[] hatsheet;

    [HideInInspector]
    public Sprite[] priorsheet;
    public float RATE = 0.2f;
    public float DELAY_RANGE = 0.2f;

    [HideInInspector]
    public int frameshow = 0;
    [HideInInspector]
    public int priorframe = 0;
    private bool isDoingPriorAnimation = false;

    private float count;
    private float priorcount;

    public enum EndOfAnimation { DoNothing, SetActiveFalse, Destroy, SetActiveFalseParent, DestroyParent }
    public EndOfAnimation endOfAnimation = EndOfAnimation.DoNothing;

    void Start()
    {
        priorcount = RATE + 1f;
        count = Random.Range(0f, DELAY_RANGE); //make animation pause for random time
        spriteRenderer = GetComponent<SpriteRenderer>();

        SpriteRenderer[] children = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer child in children)
        {
            if (child.transform.parent == transform)
            {
                hatRenderer = child;
                break;
            }
        }

        // TODO: add hat!
        if (hatRenderer != null)
            hatRenderer.sprite = test;
    }

    void Update()
    {
        if (!active) return;

        if (spritesheet.Length == 0) return;

        if (isDoingPriorAnimation)
        {
            if (priorsheet == null)
                return;

            priorcount += Time.deltaTime;

            if (priorcount > RATE)
            {
                priorcount = 0;
                DrawNextPriorFrame();
            }
        }
        else
        {
            if (spritesheet == null) return;

            count += Time.deltaTime;
            if (count > RATE)
            {
                count = 0;
                DrawNextFrame();
            }
        }
    }

    private void OnEnable()
    {
        count = 0;
        frameshow = 0;
        priorframe = 0;
    }

    void DrawNextPriorFrame()
    {
        GetComponent<SpriteRenderer>().sprite = priorsheet[priorframe];
        priorframe++;

        if (priorframe == priorsheet.Length)
        {
            if (DoEndOfAnimation()) return;
            isDoingPriorAnimation = false;
        }
    }

    public virtual void DrawNextFrame()
    {
        frameshow++;

        if (frameshow >= spritesheet.Length)
        {
            if (DoEndOfAnimation()) return;
            frameshow = 0;
        }

        GetComponent<SpriteRenderer>().sprite = spritesheet[frameshow];

        if (hatsheet.Length != 0)
            hatRenderer.sprite = hatsheet[frameshow];
    }

    public void StartPriorAnimation(Sprite[] frames)
    {
        priorcount = RATE + 1f;
        priorframe = 0;
        priorsheet = frames;
        isDoingPriorAnimation = true;
    }

    public bool DoEndOfAnimation()
    {
        if (endOfAnimation == EndOfAnimation.DoNothing) return false;
        if (endOfAnimation == EndOfAnimation.SetActiveFalse) gameObject.SetActive(false);
        if (endOfAnimation == EndOfAnimation.Destroy) Destroy(gameObject);
        if (endOfAnimation == EndOfAnimation.SetActiveFalseParent) transform.parent.gameObject.SetActive(false);
        if (endOfAnimation == EndOfAnimation.DestroyParent) Destroy(transform.parent.gameObject);
        return true;
    }
}
