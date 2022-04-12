using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Pool))]
public  class SpawnPoint : MonoBehaviour
{
    [SerializeField] private float _time;
    
    private static Pool _pool;
    private Vector3 _spawnPoint;
    private void Start()
    {
        _pool = GetComponent<Pool>();
        
        _spawnPoint = gameObject.transform.position;
        StartCoroutine(SpawnDelay());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _pool.GetFreeElement(_spawnPoint);
        }
    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(_time);
        
        _pool.GetFreeElement(_spawnPoint);
        
        StartCoroutine(SpawnDelay());
    }
}
