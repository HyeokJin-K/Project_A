using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    #region Public Field

    public List<GameObject> playerSkillList;

    [ReadOnly]
    public List<GameObject> currentPlayerSkillList;

    #endregion    

    #region Private Field

    List<Dictionary<string, object>> skillDataTable = new List<Dictionary<string, object>>();

    [SerializeField]
    GameObject playerSkillSlotObject;

    #endregion

    //------------------------------------------------------------------------------------------------

    #region Unity LifeCycle    

    private void Awake()
    {
        skillDataTable = CSVReader.Read("DataTable/PlayerSkillDataTable");        

        for (int i = 0; i < skillDataTable.Count; i++)
        {
            AddNewPlayerSkill(skillDataTable[i]["Name"].ToString());
        }
    }

    float tt = 0f;

    private void Update()
    {        
        tt += Time.deltaTime;

        if(tt >= 1f)
        {
            tt = 0f;

            foreach(var skill in currentPlayerSkillList)
            {
                skill.GetComponent<ISkill>().ActivateSkill();
            }
        }
    }

    #endregion

    void InitSkillDataFromCSV(PlayerSkillData skillData, string skillName)      //  스킬 데이터들을 CSV 스킬 데이터 값들로 초기화
    {
        foreach (var tableData in skillDataTable)
        {
            if (tableData["Name"].ToString().Equals(skillName))
            {
                skillData.SetPlayerSkillData(tableData["Name"].ToString(), float.Parse(tableData["SkillPower"].ToString()),
                                     float.Parse(tableData["SkillDelay"].ToString()), int.Parse(tableData["RepeatCount"].ToString()));

                print($"{skillData.SkillName}에 데이터를 넣었습니다");
            }
        }
    }

    public void AddNewPlayerSkill(string name)      // 현재 플레이어 스킬 목록에 해당 스킬 추가
    {            
        foreach(var skill in playerSkillList)
        {
            string skillName = skill.name;
            
            if (name.Equals(skillName))
            {
                GameObject skillOb = Instantiate(skill, playerSkillSlotObject.transform);

                skillOb.name = skillName;

                InitSkillDataFromCSV(skillOb.GetComponent<ISkill>().GetPlayerSkillData(), skillName);

                currentPlayerSkillList.Add(skillOb);

                break;
            }            
        }
    }

    /*public void RemovePlayerSkill(string name)
    {
        foreach(var skill in currentPlayerSkillList)
        {
            string skillName = skill.name;         

            if (name.Equals(skillName))
            {                                
                currentPlayerSkillList.Remove(skill);

                Destroy(skill);

                break;
            }
        }
    }*/

    /*public List<string> GetCurrentSkillNameList()
    {
        List<string> skillNameList = new List<string>();

        foreach(var skillData in currentPlayerSkillList)
        {
            skillNameList.Add(skillData.name);
        }

        return skillNameList;
    }*/
}
