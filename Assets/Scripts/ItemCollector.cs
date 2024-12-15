using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public AudioSource collectSound;
    public AudioSource aouSound;
    public StatusManager statusManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Money")) {
            statusManager.AddXp(1);
            Destroy(other.gameObject);
            collectSound.Play();
        } else if (other.gameObject.CompareTag("Drink")) {
           statusManager.AddXp(-20);
            Destroy(other.gameObject);
            aouSound.Play();
        } else if (other.gameObject.CompareTag("GateGood")) {
            statusManager.AddXp(20);
            collectSound.Play();
        } else if (other.gameObject.CompareTag("GateBad")) {
            statusManager.AddXp(-20);
            aouSound.Play();
        }
    }
}
