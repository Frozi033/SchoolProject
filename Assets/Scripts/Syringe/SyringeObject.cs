using System;
using UnityEngine;

[CreateAssetMenu]
public class SyringeObject : Syringe
{
    [SerializeField] private string _infectedName = "Name";
    [SerializeField] private GameObject _syringe;

    public static Action PatientIsHealthy;

    public override void Init()
    {
        StickmanCore.onSyringeTreatmentEvent += Treatment;
        SyringeTake();
    }

    private void Treatment(GameObject current)
    {
        if (current.CompareTag(_infectedName))
        {
            PatientIsHealthy.Invoke();
            _syringe.SetActive(false);
            Debug.Log("soper");
        }
    }

    private void SyringeTake()
    {
        _syringe.SetActive(true);
    }
}
