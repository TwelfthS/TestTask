using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    
    public GameObject poor;
    public GameObject casual;
    public GameObject middle;
    public GameObject rich;
    public int xp;
    public Animator animator;
    public AudioSource upgradeSound;
    public Slider statusBar;
    public Image fillArea;
    public TMP_Text statusText;
    public TMP_Text scoreText;
    public TMP_Text levelText;
    public GameObject failObj;
    private Color32 poorColor = new Color32(255, 78, 56, 255);
    private Color32 casualColor = new Color32(255, 78, 56, 255);
    private Color32 decentColor = new Color32(230, 230, 53, 255);
    public void AddXp(int xpToAdd) {
        int prevXp = xp;
        xp += xpToAdd;
        statusBar.value = Mathf.Min(xp / 100f, 1f);
        scoreText.text = xp.ToString();
        CheckChange(prevXp, xp);
        if (xp <= 0) {
            Fail();
        }
    }
    private void CheckChange(int prevXp, int xp) {
        if (prevXp < 40 && xp >= 40) {
            ChangeStatus(poor, casual, 1, true);
        }
        if (prevXp < 60 && xp >= 60) {
            ChangeStatus(casual, middle, 2, true);
        }
        if (prevXp < 100 && xp >= 100) {
            ChangeStatus(middle, rich, 3, true);
        }
        if (prevXp >= 40 && xp < 40) {
            ChangeStatus(casual, poor, 0, false);
        }
        if (prevXp >= 60 && xp < 60) {
            ChangeStatus(middle, casual, 1, false);
        }
        if (prevXp >= 100 && xp < 100) {
            ChangeStatus(rich, middle, 2, false);
        }
    }

    private void ChangeStatus(GameObject prevStatus, GameObject nextStatus, int richLevel, bool isUp) {
        prevStatus.SetActive(false);
        nextStatus.SetActive(true);
        animator.SetInteger("richLevel", richLevel);
        switch (richLevel) {
            case 0:
                statusText.text = "бомж";
                statusText.color = poorColor;
                fillArea.color = poorColor;
                break;
            case 1:
                statusText.text = "бедный";
                statusText.color = casualColor;
                fillArea.color = casualColor;
                break;
            case 2:
                statusText.text = "состоятельный";
                statusText.color = decentColor;
                fillArea.color = decentColor;
                break;
            case 3:
                statusText.text = "богатый";
                statusText.gameObject.SetActive(false);
                statusBar.gameObject.SetActive(false);
                break;
            default:
                break;
        }
        if (isUp) {
            animator.SetTrigger("isUpgraded");
            upgradeSound.Play();
        }
    }

    private void Fail() {
        failObj.SetActive(true);
        playerMovement.Stop();
        animator.SetBool("isDead", true);
        levelText.gameObject.SetActive(false);
        statusBar.gameObject.SetActive(false);
        statusText.gameObject.SetActive(false);
    }
}
