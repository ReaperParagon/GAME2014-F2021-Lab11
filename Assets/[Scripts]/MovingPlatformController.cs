using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    [Header("Movement")]
    public MovingPlatformDirection direction;
    [Range(0.1f, 10.0f)]
    public float speed;
    [Range(0.1f, 10.0f)]
    public float distance;
    public bool isLooping;

    private Vector2 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        Vector3 displacement = transform.position - new Vector3(startingPosition.x, startingPosition.y);
        if (displacement.sqrMagnitude >= (distance * distance) - 0.1f)  // 0.1f represents the stopping distance (how close it is to the end)
        {
            isLooping = false;
        }

        if (isLooping)
        {
            // Looping
            switch (direction)
            {
                case MovingPlatformDirection.HORIZONTAL:
                    transform.position = new Vector2(startingPosition.x + Mathf.PingPong(Time.time * speed, distance), transform.position.y);
                    break;
                case MovingPlatformDirection.VERTICAL:
                    transform.position = new Vector2(transform.position.x, startingPosition.y + Mathf.PingPong(Time.time * speed, distance));
                    break;
                case MovingPlatformDirection.DIAGONAL_UP:
                    transform.position = new Vector2(startingPosition.x + Mathf.PingPong(Time.time * speed, distance), startingPosition.y + Mathf.PingPong(Time.time * speed, distance));
                    break;
                case MovingPlatformDirection.DIAGONAL_DOWN:
                    transform.position = new Vector2(startingPosition.x + Mathf.PingPong(Time.time * speed, distance), startingPosition.y - Mathf.PingPong(Time.time * speed, distance));
                    break;
            }
        }
    }
}
