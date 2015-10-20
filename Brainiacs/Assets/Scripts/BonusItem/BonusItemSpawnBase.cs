using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Brainiacs.Generate;

//--MG
public abstract class BonusItemSpawnBase
{
    public GameObject prefab;
    
    abstract public void SetReady();



    public void Activate()
    {
        
    }
}
