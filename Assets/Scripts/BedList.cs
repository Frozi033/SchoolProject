using System.Collections.Generic;
using UnityEngine;

public class BedList : MonoBehaviour
{
    public static List<GameObject> EmptyBeds = new List<GameObject>();

    private int BedId;
    private Vector3 BedPosition;
    
    public delegate void Founded(Vector3 BedPosition);
    
    public static event Founded FoundedBedEvent;

    void Start()
    {
        RunState.FoundBedForMeEvent += FoundBed;
    }

    private void BedIsOccupied(int id)
    {
        //List<>
    }
    private void BedIsFree(int id)
    {
        //List<>
    }

    private void FoundBed()
    {
        BedId = Random.Range(0, EmptyBeds.Count);
        BedPosition = EmptyBeds[BedId].gameObject.transform.position;
        FoundedBedEvent?.Invoke(BedPosition);
    }
    
}
