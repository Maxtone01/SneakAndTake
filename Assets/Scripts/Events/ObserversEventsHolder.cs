using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserversEventsHolder<T> : MonoBehaviour
{
    public Action<T> onCollisionEnter;
    public Action<T> onCollisionExit;
    public Action<T> onTriggerEnter;
    public Action<T> onTriggerExit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<T>(out T component))
        {
            onCollisionEnter?.Invoke(component);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<T>(out T component))
        {
            onCollisionEnter?.Invoke(component);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<T>(out T component))
        {
            onTriggerEnter?.Invoke(component);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<T>(out T component))
        {
            onTriggerExit?.Invoke(component);
        }
    }
}
