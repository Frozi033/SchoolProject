using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BedState : State
{
    private int _currentId;
    public override void Init(int id)
    {
    }

    public override void Do()
    {
        if (IsFinished)
        {
            //BedList.FoundedBedEvent -= PositionAssigning;
            return;
        }
    }
}
