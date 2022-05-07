using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BossSkillData
{
    public string skillname;
    public float skillDelay;
    public float skillPower;

    public BossSkillData(string name, float delay, float power)
    {
        skillname = name;
        skillDelay = delay;
        skillPower = power;
    }
}

[Serializable]
public class Serialization<T>
{
    [SerializeField]
    List<T> target;
    public List<T> ToList() { return target; }

    public Serialization(List<T> target)
    {
        this.target = target;
    }
}
