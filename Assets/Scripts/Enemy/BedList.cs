using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BedList : MonoBehaviour
{
    protected static List<GameObject> EmptyBeds = new List<GameObject>();

    private int _bedId;
    private Vector3 _bedPosition;
    private Vector3 _nullPos;

    private GameObject _currentBed;

    private void Start()
    {
        StartAdd();
    }

    public void BedIsOccupied(int id)
    {
        EmptyBeds.RemoveAt(id);
        Debug.Log("BedDelited");
    }
    protected void BedIsFree()
    {
        EmptyBeds.Add(_currentBed);
        Debug.Log("BedAdded");
    }

    protected Vector3 FoundBed()
    {
        if (EmptyBeds.Count > 0)
        {
            _bedId = Random.Range(0, EmptyBeds.Count);
            _currentBed = EmptyBeds[_bedId].gameObject;
            _bedPosition = EmptyBeds[_bedId].gameObject.transform.position;
        
            BedIsOccupied(_bedId);

            return _bedPosition;
        }

        return _nullPos;
    }

    private void StartAdd()
    {
        var objects = GameObject.FindGameObjectsWithTag("Bed");
        for (int i = 0; i < objects.Length; i++)
        {
            EmptyBeds.Add(objects[i]);
        }
    }
}
