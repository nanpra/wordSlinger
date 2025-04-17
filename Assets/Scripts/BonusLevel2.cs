using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusLevel2 : MonoBehaviour
{

    [SerializeField] public GameManager gm;
    [SerializeField] public List<Animator> BonusEnemyAnimator;


    [SerializeField] public float totalTime = 8.0f;
    [SerializeField] public float currentTime;
    private bool isRunning;
    [SerializeField] public TextMeshProUGUI timerText;
    [SerializeField] public Animator instructionsAnim;
    private int correctIndex;

    private void Start()
    {
        currentTime = totalTime;
        isRunning = true;
    }

    public void Update()
    {
        DeadAnim();
        WrongWordAnimation();
        UpdateTimerDisplay();
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 7)
            {
                instructionsAnim.SetBool("timeUp", false);
            }

            if (currentTime <= 0.5f)
            {
                gm.attempts--;
                Handheld.Vibrate();
                instructionsAnim.SetBool("timeUp", true);
                WrongWordAnimation();
                isRunning = false;
                ResetTimer();
            }
        }
    }
    public void DeadAnim()
    {
        correctIndex = 3 - gm.Correct;


        if (correctIndex >= 0 && correctIndex < BonusEnemyAnimator.Count)
        {
            if (correctIndex == 0)
            {
                StartCoroutine(DeathDelayForLastEnemy());
                
            }
            else
            {
                BonusEnemyAnimator[correctIndex].SetBool("IsDead", true);
            }
        }
    }

    public void WrongWordAnimation()
    {
        if (gm.wrongWord || currentTime < 1f)
        {
            int attempts = gm.attempts;

            if (BonusEnemyAnimator.Count == 0)
                return;

            foreach (var animator in BonusEnemyAnimator)
            {
                animator.SetBool("Equipped", attempts == 2);
                animator.SetBool("IsAiming", attempts == 1);
                animator.SetBool("IsShooting", attempts == 0);
            }

            if (attempts == 0) StopTimer();
        }
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        isRunning = true;
        currentTime = totalTime;
        currentTime -= Time.deltaTime;
    }

    private void UpdateTimerDisplay()
    {
        timerText.text = ": 0" + Mathf.Ceil(currentTime).ToString();
    }

    private IEnumerator DeathDelayForLastEnemy()
    {
        yield return new WaitForSeconds(1.5f);
        BonusEnemyAnimator[correctIndex].SetBool("IsDead", true);
    }
}