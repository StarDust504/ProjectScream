using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private Transform character;

    [Header("Stats")]
    [SerializeField] private bool bIsMoving;
    [SerializeField] private float speed;

    private int direction = 1;
    private bool switchSide;
    // Start is called before the first frame update
    void Start()
    {
        if (!bIsMoving)
            InvokeRepeating("SwitchSideTimer", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (bIsMoving)
        {
            Vector2 target = GetCurrentTarget(direction);
            transform.position = Vector2.MoveTowards(character.position, target, speed * Time.deltaTime);
            character.position = new Vector3(character.position.x, character.position.y, 204f);
            float distance = (target - (Vector2)character.position).magnitude;

            if (distance <= 0.1f)
            {
                direction *= -1;
                Flip();
            }
        }
    }
    Vector2 GetCurrentTarget(int direction)
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

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        if (character && startPosition && endPosition)
        {
            Gizmos.DrawLine(character.transform.position, startPosition.position);
            Gizmos.DrawLine(character.transform.position, endPosition.position);
        }
    }
    private void SwitchSideTimer()
    {
        Flip();
    }
}
