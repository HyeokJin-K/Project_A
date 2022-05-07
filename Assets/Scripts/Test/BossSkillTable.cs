using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillTable : MonoBehaviour
{
    public List<GameObject> skillObjectList = new List<GameObject>();

    private void OnEnable()
    {
        if(skillObjectList.Count < transform.childCount)
        {
            skillObjectList.Clear();

            for(int i = 0; i < transform.childCount; i++)
            {
                skillObjectList.Add(transform.GetChild(i).gameObject);
            }
        }
    }

}
