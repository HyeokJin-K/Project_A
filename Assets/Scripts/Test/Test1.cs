using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Test1 : MonoBehaviour
{
    public AudioListener ad;

    private void Start()
    {
        ad = ad ? ad : GetComponent<AudioListener>();
    }
}

public class MyTest
{
    public bool isActive;
    public string name;

    public MyTest(string name, bool isActive)
    {
        this.name = name;
        this.isActive = isActive;
    }
}

