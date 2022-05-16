using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSkillData
{
    #region Private Field
    
    [SerializeField]
    string skillName;

    [SerializeField]
    float skillPower;

    [SerializeField]
    float skillDelay;

    [SerializeField]
    int repeatCount;

    #endregion
   

    #region Public Field

    public string SkillName { get => skillName; }    

    public float SkillPower { get => skillPower; }

    public float SkillDelay { get => skillDelay; }

    public int RepeatCount { get => repeatCount; }

    #endregion        

    //-------------------------------------------------------------------------------------------

    public void SetPlayerSkillData(string skillName, float skillPower, float skillDelay, int repeatCount)
    {
        this.skillName = skillName;

        this.skillPower = skillPower;

        this.skillDelay = skillDelay;

        this.repeatCount = repeatCount;
    }
}
