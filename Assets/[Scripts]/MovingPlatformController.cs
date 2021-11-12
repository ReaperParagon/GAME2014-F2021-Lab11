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
    [Range(0.05f, 0.1f)]
    public float distanceOffset;
    public bool isLooping;

    private bool movingIsActive;
    private Vector2 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        movingIsActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        float pingPongValue = (movingIsActive) ? Mathf.PingPong(Time.time * speed, distance) : 0.0f;

        if ((!isLooping) && (pingPongValue >= distance - distanceOffset))
        {
            movingIsActive = false;
        }

        switch (direction)
        {
            case MovingPlatformDirection.HORIZONTAL:
                transform.position = new Vector2(startingPosition.x + pingPongValue, transform.position.y);
                break;
            case MovingPlatformDirection.VERTICAL:
                transform.position = new Vector2(transform.position.x, startingPosition.y + pingPongValue);
                break;
            case MovingPlatformDirection.DIAGONAL_UP:
                transform.position = new Vector2(startingPosition.x + pingPongValue, startingPosition.y + pingPongValue);
                break;
            case MovingPlatformDirection.DIAGONAL_DOWN:
                transform.position = new Vector2(startingPosition.x + pingPongValue, startingPosition.y - pingPongValue);
                break;
        }
    }
}
