using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    bool gameover = false;

    private void OnEnable()
    {
        gameover = false;
        Time.timeScale = 1f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameover) return;

        if (collision.CompareTag("Zombie"))
        {
            DGameSystem.LoadPool("AteYourBrains", new Vector3(0, 0));
            //Time.timeScale = 0.5f;
            DGameSystem.instance.ShowPlayAgain(4f);
        }
    }
}
