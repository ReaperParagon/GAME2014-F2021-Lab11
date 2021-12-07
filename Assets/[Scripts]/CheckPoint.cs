using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CheckPoint : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform trigger;

    public Vector3 spawnPointPosition;
    public Vector3 triggerPosition;

    private Vector3 currentSpawnPosition;
    private Vector3 currentTriggerPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnPointPosition = spawnPoint.position;
        triggerPosition = trigger.position;
        currentSpawnPosition = spawnPointPosition;
        currentTriggerPosition = triggerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPositionsChanged();
    }

    public void CheckIfPositionsChanged()
    {
        if (spawnPointPosition != currentSpawnPosition)
        {
            spawnPoint.position = spawnPointPosition;
            currentSpawnPosition = spawnPointPosition;
        }

        if (triggerPosition != currentTriggerPosition)
        {
            trigger.position = triggerPosition;
            currentTriggerPosition = triggerPosition;
        }

    }

    
}
