using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private Transform platform;
    [SerializeField] private float speed;

    int direction = 1;   // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = currentMovementTarget(direction);

        platform.position = Vector2.MoveTowards(platform.position, target, speed * Time.deltaTime);
        platform.position = new Vector3(platform.position.x, platform.position.y, 204f);

        float distance = (target - (Vector2)platform.position).magnitude;

        if (distance <= 0.1f)
        {
            direction *= -1;
        }
    }

    Vector2 currentMovementTarget(int direction)
    {
        if (direction == 1)
        {
            return startPosition.position;
        }
        else
        {
            return endPosition.position;
        }
    }

    private void OnDrawGizmos()
    {
        if (platform && startPosition && endPosition)
        {
            Gizmos.DrawLine(platform.transform.position, startPosition.position);
            Gizmos.DrawLine(platform.transform.position, endPosition.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnityEngine.Debug.Log("Collision!!!");
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(platform.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.transform.SetParent(null);
    }
}
