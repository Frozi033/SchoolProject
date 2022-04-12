using System;
using UnityEngine;

public class SyringeTable : MonoBehaviour
{
    [SerializeField] private String tag;
    [SerializeField] private GameObject _currentSyringe;

    public static Action<GameObject> SyringeTakenEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.tag = tag;
            SyringeTakenEvent?.Invoke(_currentSyringe);
        }
    }
}
