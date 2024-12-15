using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningPoint : MonoBehaviour
{
    public int turnDirection = 1;
    private bool isUsed = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && !isUsed)
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            player.Turn(turnDirection);
            isUsed = true;
        }
    }
}
