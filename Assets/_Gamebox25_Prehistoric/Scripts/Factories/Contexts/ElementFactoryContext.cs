using UnityEngine;

public class ElementFactoryContext
{
    public GameObject Element { get; }
    public Vector3 SpawnPoint { get; }

    public ElementFactoryContext(GameObject prefab, Vector3 spawnPoint)
    {
        Element = prefab;
        SpawnPoint = spawnPoint;
    }
}