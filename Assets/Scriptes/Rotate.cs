using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed;
    public Vector3 vec;


    private void Update()
    {
        transform.Rotate(vec, speed * Time.deltaTime);
    }
}
