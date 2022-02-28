using UnityEngine;

[CreateAssetMenu]
public class SyringeObject : Syringe
{
    [SerializeField] private string _infectedName = "Name";
    [SerializeField] private GameObject _syringe;
    
    public delegate void StateNotify();

    public static event StateNotify SetPatienState;

    protected virtual void Init()
    {
         SyringeLogic.onSyringeTakeEvent += SyringeTake;
         StickmanCore.onSyringeTreatmentEvent += Treatment;
    }

    protected void Treatment(GameObject current)
    {
        if (current.CompareTag(_infectedName))
        {
            SetPatienState?.Invoke();
            _syringe.SetActive(false);
        }
    }

    protected void SyringeTake()
    {
        _syringe.SetActive(true);
    }
}
