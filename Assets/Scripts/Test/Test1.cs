using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Test1 : MonoBehaviour
{

    void Start()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("PlayerSkillData");

        int count = 0;
        foreach (var d in data)
        {
            print($"{data[count]["Name"]} {data[count]["SkillPower"]} {data[count]["SkillDelay"]}");
            count++;
        }        
    }

}

[System.Serializable]
public class Data1
{
    [SerializeField]
    public string name;

    [SerializeField]
    public int number;

    [SerializeField]
    public float value;

    public Data1(string name, int num, float value)
    {
        this.name = name;

        number = num;

        this.value = value;
    }
    public Data1(string name, int num)
    {
        this.name = name;

        number = num;
    }
}
