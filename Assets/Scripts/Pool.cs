using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pool : MonoBehaviour // реализация пула
{
    [SerializeField] private List<PoolObject> _infectedTypes;
    [SerializeField] private Transform _container;
    [SerializeField] private int _minCapacity;
    [SerializeField] private int _maxCapacity;
    [SerializeField] private bool _autoExpend;

    private List<PoolObject> _pool;
    private int _index;
    private int _id;

    private void OnValidate()
    {
        if (_autoExpend)
        {
            _maxCapacity = Int32.MaxValue;
        }
    }

    private void Start()
    {
        CreatPool();
    }

    private void CreatPool()
    {
        _pool = new List<PoolObject>(_minCapacity);
        for (int i = 0; i < _minCapacity; i++)
        {
            CreatElement();

        }
    }

    private PoolObject CreatElement(bool isActiveByDefault = false)
    {
        _index = Random.Range(0, _infectedTypes.Count);
        var createdObject = Instantiate(_infectedTypes[_index], _container);
        createdObject.gameObject.SetActive(isActiveByDefault);

        _pool.Add(createdObject);
        return createdObject;
    }

    public int GetMyId(PoolObject me)
    {
        for (int i = 0; i < _pool.Count; i++) // не получается так как в пуле все клоны и по факту они никак не отличаются
        {
            if (me == _pool[i])
            {
                _id = i;
                break;
            }
        }
        return _id;
    }

    public bool TryGetElement(out PoolObject element)
    {
        foreach (var item in _pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                element = item;
                item.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public PoolObject GetFreeElement(Vector3 position, Quaternion rotation)
    {
        var element = GetFreeElement(position);
        element.transform.rotation = rotation;
        return element;
    }

    public PoolObject GetFreeElement(Vector3 position)
    {
        var element = GetFreeElement();
        element.transform.position = position;
        return element;
    }

    public PoolObject GetFreeElement()
    {
        if (TryGetElement(out var element))
        {
            return element;
        }

        if (_autoExpend)
        {
            return CreatElement(true);
        }

        if (_pool.Count < _maxCapacity)
        {
            return CreatElement(true);
        }
        
        throw new Exception("Pool is over");
    }
}
