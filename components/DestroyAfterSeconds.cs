using UnityEngine;
using System.Collections;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float lifetime = 0.1f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}