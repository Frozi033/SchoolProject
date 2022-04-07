using System;
using UnityEngine;

[CreateAssetMenu]
public class SyringeObject : Syringe
{
    [SerializeField] private string _infectedTag = "Tag";
    [SerializeField] private GameObject _syringe;

    public static Action<int> PatientIsHealthy;

    public override void Init()
    {
        Enemy.TreatmentEvent += Treatment;
        SyringeTake();
        Debug.Log("Sobaka");
    }

    private void Treatment(int id, string tag)
    {
        if (tag == _infectedTag)
        {
            //current.
            PatientIsHealthy.Invoke(id);
            _syringe.SetActive(false);
            Debug.Log("soper");
        }
        else
        {
            Debug.Log(tag);
        }
    }

    private void SyringeTake()
    {
        _syringe.SetActive(true);
    }
}
