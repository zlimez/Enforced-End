using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkydivingCloud : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * 15 * Time.deltaTime;
        if (transform.position.y > 5.2) {
            transform.position = new Vector3(Random.Range(-8.0f, 8.0f), -7.0f, 0);
        }
    }
}
