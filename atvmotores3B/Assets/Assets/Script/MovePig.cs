using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform target;
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, 5 * Time.deltaTime);
        
        if (transform.position.x > target.position.x)
            sr.flipX = false;
        else sr.flipX = true;
    }
}
