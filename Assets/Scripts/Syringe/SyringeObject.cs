using System;
using UnityEngine;

[CreateAssetMenu]
public class SyringeObject : Syringe
{
    [SerializeField] private string _infectedTag = "Tag";
    [SerializeField] private GameObject _syringe;

    public static Action<int> PatientIsHealthy;

    public void Init()
    {
        SyringeTake();
    }

    private void Treatment(int id, string tag)
    {
        if (tag == _infectedTag)
        {
            PatientIsHealthy.Invoke(id);
            _syringe.SetActive(false);
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
