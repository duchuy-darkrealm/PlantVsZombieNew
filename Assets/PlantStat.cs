using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlantStat", menuName = "PlantStat")]
public class PlantStat : DStat
{
    public string plantObjName;
    public Sprite sprite;
    public float cooldown = 5f;
    public int price = 100;
    public string spawnObjName;
    public float spawnRate = 5f;
    public float startCount = 0;
}
