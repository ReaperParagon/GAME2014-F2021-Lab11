using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    private GameController gameController;
    private Vector3 checkPointSpawn;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        checkPointSpawn = GetComponentInParent<CheckPoint>().spawnPointPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameController.SetSpawnPoint(checkPointSpawn);
        }
    }
}
