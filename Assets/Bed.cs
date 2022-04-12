using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    [SerializeField] private float _heightOnBed;

    private Vector3 _bedPos;
    private void Start()
    {
        _bedPos = gameObject.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Infected>().CurrentBed == _bedPos)
        {
            var position = _bedPos;
            position.y = _heightOnBed;
            other.gameObject.transform.position = position;
            other.gameObject.transform.rotation = Quaternion.Inverse(gameObject.transform.rotation);
        }
    }
}
