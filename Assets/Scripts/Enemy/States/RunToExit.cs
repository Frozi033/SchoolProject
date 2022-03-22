using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RunToExit : State
{
    [SerializeField] private Vector3 _exitPos;
    public override void Do()
    {
        if (IsFinished)
        {
            return;
        }
        Enemy.MoveTo(_exitPos);
    }
}
