using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RunState : State
{
    private Vector3 _targetPos;

    private int _currentId;

    public static bool GoToBed; // тут есть проблема, в общем, у нас все завязанно на статических событиях, а они отправляют уведомления всем экземплярам, поэтому, если мы лечим одного чупса, то лечатся сразу все, это надо пофиксить
    
    public static Action<int> FoundBedForMeEvent;
    
    public override void Init(int id)
    { 
        BedList.FoundedBedEvent += PositionAssigning;
        this._currentId = id;
        Debug.Log("Compas");
        FoundBedForMeEvent.Invoke(_currentId);
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

    private void PositionAssigning(Vector3 BedPosition, int id)
    {
        Debug.Log(id + " ID in run");
        if (BedPosition != _targetPos && _currentId == id)
        {
            _targetPos = BedPosition;
        }
        else
        {
            FoundBedForMeEvent?.Invoke(_currentId);
            Debug.Log("sSomos");
        }
    }
}
