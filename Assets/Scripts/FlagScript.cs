using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagScript : MonoBehaviour
{
    public AudioSource flagsUpSound;
    public Animator animator;
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("flagsUp");
            flagsUpSound.Play();
        }
    }
}
