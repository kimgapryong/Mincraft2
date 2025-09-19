using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : BaseController
{
    public float moveSpeed = 1f;
    private void LateUpdate()
    {
        if (Manager.Player == null)
            return;

        Vector3 target = Manager.Player.transform.position;
        target.z = -10f;

        float t = 1f - Mathf.Exp(-moveSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, target, t);
    }
}
