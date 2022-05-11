using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillData", menuName = "Scriptbale Object/PlayerSkillData", order = int.MaxValue)]
public class PlayerSkillData : ScriptableObject
{
    #region Private Field
    [SerializeField]
    Sprite skillSprite;
    [SerializeField]
    string skillName;
    [SerializeField]
    float initSkillPower;
    [SerializeField]
    float initSkillDelay;
    [SerializeField]
    int initRepeatCount;

    [SerializeField, ReadOnly]
    float skillPower;
    [SerializeField, ReadOnly]
    float skillDelay;
    [SerializeField, ReadOnly]
    int repeatCount;
    #endregion
   

    #region Public Field
    public Sprite SkillSprite { get => skillSprite; }
    public string SkillName { get => skillName; }    
    public float SkillPower { get => skillPower + initSkillPower; set => skillPower = value; }
    public float SkillDelay { get => skillDelay + initSkillDelay; set => skillDelay = value; }
    public int RepeatCount { get => repeatCount + initRepeatCount; set => repeatCount = value; }
    #endregion
}
