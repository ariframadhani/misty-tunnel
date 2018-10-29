using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazlePoint : MonoBehaviour {
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, .3f);
    }
}
