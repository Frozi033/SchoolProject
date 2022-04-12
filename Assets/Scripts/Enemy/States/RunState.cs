using UnityEngine;

[CreateAssetMenu]
public class RunState : State
{
    [SerializeField] private State WaitForRunState;

    private Vector3 _nullPos;
    private Vector3 _targetPos;

    public override void Init()
    {
        _targetPos = Infected.FoundBedInfected();
        if (_targetPos == _nullPos)
        {
            Infected.SetState(WaitForRunState);
        }
        else
        {
            Infected.MoveTo(_targetPos);
        }
    }
    public override void Do()
    {
        if (IsFinished)
        {
            return;
        }
        Infected.IsComeToBed(_targetPos);
    }

    /*private void PositionAssigning(Vector3 BedPosition, int id)
    {
        if (BedPosition != _targetPos)
        {
            _targetPos = BedPosition;
        }
        else
        {
            FoundBedForMeEvent?.Invoke(_currentId);
            Debug.Log("sSomos");
        }
    }
   private void GettingPosition*/
}
