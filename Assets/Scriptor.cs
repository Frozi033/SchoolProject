using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scriptor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        other.gameObject.transform.SetParent(other.gameObject.transform, true);
    }
}
