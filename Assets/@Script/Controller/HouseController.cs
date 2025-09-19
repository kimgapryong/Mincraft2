using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    public Transform pos;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player == null)
            return;

        Manager.Player.transform.position = pos.transform.position;
        Camera.main.transform.position = pos.transform.position;

    }
}
