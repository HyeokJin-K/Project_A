using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptbale Object/Monster Data", order = int.MaxValue)]
public class MonsterData : ScriptableObject
{
    #region Private Field
    [SerializeField]
    string monsterName;
    [SerializeField]
    float healthPoint;
    [SerializeField]
    float attackPower;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float attackDelay;
    #endregion

    #region Public Field
    public string MonsterName { get => monsterName; }
    public float HealthPoint { get => healthPoint; }
    public float AttackPower { get => attackPower; }
    public float AttackDelay { get => attackDelay; }
    public float MoveSpeed { get => moveSpeed; }
    #endregion
}
