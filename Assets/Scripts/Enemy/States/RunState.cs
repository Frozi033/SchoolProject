using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RunState : State
{
    private Vector3 _targetPos;

    [SerializeField] private Vector3 _exitPos;

    public static bool GoToBed;
    
    public static Action FoundBedForMeEvent;
    
    public override void Init()
    {
        if (!GoToBed)
        {
            BedList.FoundedBedEvent += PositionAssigning;
            FoundBedForMeEvent.Invoke();
        }
        else
        {
            _targetPos = _exitPos;
        }
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
