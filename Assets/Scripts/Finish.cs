using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public AudioSource finishSound;
    public Animator animator;
    private bool levelCompleted = false;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player" && !levelCompleted) {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            player.Stop();
            finishSound.Play();
            levelCompleted = true;
            animator.SetBool("finishedLevel", true);
            Invoke("CompleteLevel", 3f);
        }
    }

    private void CompleteLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
