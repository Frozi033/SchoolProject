using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    public State SleepState;

    private Vector3 BedPos;
    
    public delegate void Found();
    
    public static event Found FoundBedForMeEvent;
    public override void Init()
    {
        FoundBedForMeEvent?.Invoke();
        //BedList.FoundedBedEvent += Enemy.MoveTo;
    }

    public override void Do()
    {
        if (IsFinished)
        {
            return;
        }
        BedList.FoundedBedEvent += Enemy.MoveTo;
    }
}
