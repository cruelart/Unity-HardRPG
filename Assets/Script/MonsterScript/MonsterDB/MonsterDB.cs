using NUnit.Framework;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterDB", menuName = "Scriptable Objects/MonsterDB")]
public class MonsterDB : ScriptableObject
{
    public int monsterID;

    public MonsterType monsterType;

    public Sprite monsterImage;

    public List<Stat> stats;

    public float viewAngle;

    public float detectionRange;

    public float attackRange;
}

public enum MonsterType
{
    Slime,
    Golem,
}
