using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

[CreateAssetMenu]
public class WaitForRunState : State
{
    public override void Init()
    {
        Infected.StartTimer();
    }
    public override void Do()
    {
        if (IsFinished)
        {
            return;
        }
    }
    
}
