using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

[CreateAssetMenu]
public class WaitForRunState : State
{
    public State RunState;
    
    public override void Init(int id)
    {
        //Enemy.EnemyAnimator.SetBool("Sleep", false);
    }
    public override void Do()
    {
        if (IsFinished)
        {
            return;
        }

       /* if (BedList.EmptyBeds.Count > 0)
        {
            IsFinished = true;
        }*/
    }
    
}
