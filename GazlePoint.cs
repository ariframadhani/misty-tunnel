using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazlePoint : MonoBehaviour {

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, 0.6f, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, .3f);
    }
}
