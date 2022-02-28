using UnityEngine;

public class SyringeLogic : StickmanCore
{
    public delegate void OnSyringeTake();

    public static event OnSyringeTake onSyringeTakeEvent;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && LifeStatus == Status.Live)
        {
            onSyringeTakeEvent?.Invoke();
            LifeStatus = StickmanCore.Status.SyringeTaken;
        }
    }
}
