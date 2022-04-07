using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private float _time;
    public static int idInfected { get; private set; }

    
    public static Pool _pool { get; private set; }
    private Transform _spawnPoint;
    private void Start()
    {
        _spawnPoint = GetComponent<Transform>();
        _pool = GetComponent<Pool>();
        idInfected = 0;
        _pool.GetFreeElement(_spawnPoint.position);
        StartCoroutine(SpawnDelay());
    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(_time);
        idInfected++;
        _pool.GetFreeElement(_spawnPoint.position);
        StartCoroutine(SpawnDelay());
    }
}
