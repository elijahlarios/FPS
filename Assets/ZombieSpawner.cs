using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private MeshRenderer zone;

    // Generates spawn point within a plane
    public Vector3 randomSpawn()
    {
        Vector3 spawnPoint = new Vector3(
                UnityEngine.Random.Range(zone.bounds.min.x, zone.bounds.max.x),
                zone.bounds.center.y,
                UnityEngine.Random.Range(zone.bounds.min.z, zone.bounds.max.z));
        return spawnPoint;
    }
}