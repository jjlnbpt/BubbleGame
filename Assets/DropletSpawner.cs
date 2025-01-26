using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletSpawner : MonoBehaviour
{

    public GameObject dropletPrefab;
    public int numDroplets;

    public void SpawnDroplets()
    {
        for (int i = 0; i < numDroplets; i++)
        {
            Instantiate(dropletPrefab, transform, false);
        }
    }
}
