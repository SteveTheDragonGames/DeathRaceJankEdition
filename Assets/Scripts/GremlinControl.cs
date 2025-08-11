using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class GremlinControl : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float minX = -3.37f;
    public float maxX = 3.37f;
    public float minY = -1.3f;
    public float maxY = 3f; 

    private Vector2 moveDirection;

    float nextDirectionChangeTime = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        moveDirection = Random.insideUnitCircle.normalized;

        nextDirectionChangeTime = Time.time + Random.Range(1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        BorderCheck();
        GremlinMove();
        GremlinChangeDirection();
       
    }

    private void GremlinMove()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void BorderCheck()
    {
        Vector2 t = transform.position;
        if (t.x < minX || t.x > maxX)
        {
            moveDirection.x = -moveDirection.x;
        }
        if (t.y < minY || t.y > maxY)
        {
            moveDirection.y = -moveDirection.y;
        }
    }

    void GremlinChangeDirection()
    {
        if (Time.time > nextDirectionChangeTime)
        {
            nextDirectionChangeTime = Time.time + Random.Range(1f, 3f);
            int newX = Random.Range(-1, 2);
            int newY = Random.Range(-1, 2);
            moveDirection = new Vector2(newX, newY);
        }
    }
}
