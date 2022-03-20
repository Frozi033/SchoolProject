using System;
using Unity.VisualScripting;
using UnityEngine;

public class SyringeTable : StickmanCore
{
    [SerializeField] private Syringe _currentSyringe;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && LifeStatus == Status.Live)
        {
            _currentSyringe.Init();
            LifeStatus = Status.SyringeTaken;
            Debug.Log(LifeStatus);
        }
    }
}
