using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BedState : State
{
    public override void Init()
    {
        Enemy.EnemyAnimator.SetBool("Sleep", true);
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
