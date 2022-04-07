using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BedList : MonoBehaviour
{
    [SerializeField] private List<GameObject> EmptyBeds = new List<GameObject>();

    private int _bedId;
    private Vector3 _bedPosition;

    private GameObject _currentBed;
    //[SerializeField] private float _timeoutOfSearch = 5;

    public static Action<Vector3, int> FoundedBedEvent;

    void Awake()
    {
        RunState.FoundBedForMeEvent += FoundBed;
        SyringeObject.PatientIsHealthy += BedIsFree;
        //StartAdd();
    }

    private void BedIsOccupied(int id)
    {
        EmptyBeds.RemoveAt(id);
        EmptyBeds.Sort();
        Debug.Log("BedDelited");
    }
    private void BedIsFree(int InfectedtId)
    {
        EmptyBeds.Add(_currentBed);
        Debug.Log("BedTaked");
    }

    public void FoundBed(int id)
    {
        _bedId = Random.Range(0, EmptyBeds.Count);
        _currentBed = EmptyBeds[_bedId].gameObject;
        Debug.Log( _bedId + "BedID");
        Debug.Log(EmptyBeds.Count);
        _bedPosition = EmptyBeds[_bedId].gameObject.transform.position;
        FoundedBedEvent.Invoke(_bedPosition, id);
        BedIsOccupied(_bedId);
    }

   /* private void StartAdd()
    {
        var objects = GameObject.FindGameObjectsWithTag("Bed");
        for (int i = 0; i <= objects.Length - 1; i++)
        {
            EmptyBeds.Add(objects[i]);
        }
    }*/
}
