using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrees : MonoBehaviour
{
    [SerializeField] private float startZ;
    [SerializeField] private float endZ;
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        // Move tree backwards
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, endZ), speed * Time.deltaTime);

        // reset tree 
        if (transform.position.z <= endZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startZ);
        }
    }
}
