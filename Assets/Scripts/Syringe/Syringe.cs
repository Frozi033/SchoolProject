using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Syringe : MonoBehaviour
{
    protected virtual void Healing(string infectedTag, string currentTag)
    {
        if (infectedTag == currentTag)
        {
            
        }
    }
}
