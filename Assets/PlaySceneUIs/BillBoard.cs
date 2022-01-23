using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Transform cam;

 
    // Update is called once per frame
    void LateUpdate()
    {
        FaceTarget();
    }

    void FaceTarget ()
    {
        Vector3 direction = (transform.position - cam.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
