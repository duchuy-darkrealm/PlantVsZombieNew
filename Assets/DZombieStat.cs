using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ZombieStat", menuName = "ZombieStat")]
public class DZombieStat : DStat
{
    public string zombieObjName;
    public Sprite sprite;
    public float cooldown = 5f;
    public int price = 100;

    public string spawnObjName;
    public float spawnRate = 5f;

    public float attackRate = 1f;
    public float attackRange = 1f;
    public float speed = 1f;

}
