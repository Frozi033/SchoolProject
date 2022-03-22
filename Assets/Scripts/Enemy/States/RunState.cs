using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RunState : State
{
    private Vector3 _targetPos;
    

    public static bool GoToBed; // тут есть проблема, в общем, у нас все завязанно на статических событиях, а они отправляют уведомления всем экземплярам, поэтому, если мы лечим одного чупса, то лечатся сразу все, это надо пофиксить
    
    public static Action FoundBedForMeEvent;
    
    public override void Init()
    { 
        BedList.FoundedBedEvent += PositionAssigning;
        FoundBedForMeEvent.Invoke();
    }

    private void OnDisable()
    {
        BedList.FoundedBedEvent -= PositionAssigning;
    }

    public override void Do()
    {
        if (IsFinished)
        {
            return;
        }
        Enemy.MoveTo(_targetPos);
    }

    private void PositionAssigning(Vector3 BedPosition)
    {
        Debug.Log(BedPosition);
        if (BedPosition != _targetPos)
        {
            _targetPos = BedPosition;
        }
        else
        {
            FoundBedForMeEvent?.Invoke();
        }
    }
}
