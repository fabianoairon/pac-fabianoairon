using System;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int Lives;

    public event Action<int> OnRemovedLife;

    public void RemoveLife()
    {
        Lives--;
        OnRemovedLife?.Invoke(Lives);
    }
}
