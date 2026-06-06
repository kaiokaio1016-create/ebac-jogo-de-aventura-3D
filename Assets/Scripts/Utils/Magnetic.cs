using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Magnetic : MonoBehaviour
{
    public float dist = .2f;
    public float coinSpeed = 3f;

    private void Update()
    {
        if(Vector3.Distance(transform.position, Player3D.Instance.transform.position) > dist) 
        {
            coinSpeed++;
            transform.position = Vector3.MoveTowards(transform.position, Player3D.Instance.transform.position, Time.deltaTime * coinSpeed);
        }
    }
}
