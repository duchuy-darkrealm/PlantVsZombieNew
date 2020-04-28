using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    void Update()
    {
        if (CheckWin())
            Win();
    }

    public bool CheckWin()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        for (int i = 0; i < zombies.Length; i++)
        {
            if (zombies[i].activeSelf == true)
                return false;
        }
        return true;
    }

    public void Win()
    {
        DGameSystem.instance.ShowMessage("You Win!");
        DGameSystem.instance.ShowPlayAgain(2f);
    }
}
