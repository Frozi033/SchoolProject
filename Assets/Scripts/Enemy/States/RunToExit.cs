using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu]
public class RunToExit : State
{
    private static GameObject _exit;
    private static Vector3 _exitPos;

    public override void Init()
    {
        if (_exit == null)
        {
            _exit = GameObject.FindGameObjectWithTag("SpawnPoint");
            _exitPos = _exit.transform.position;
            Infected.MoveTo(_exitPos);
        }
        else
        {
            Infected.MoveTo(_exitPos);
        }
    }
    public override void Do()
    {
        if (IsFinished)
        {
            return;
        }
        Infected.IsComeToExit(_exitPos);
    }
}
