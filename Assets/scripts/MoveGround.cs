using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, -7), 4 * Time.deltaTime);
        if (transform.position.z <= -7)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 13);
        }
    }
}
