using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public GameManager gm;
    [SerializeField] public Animator anim;

    [SerializeField] public float totalTime = 8.0f; 
    [SerializeField] public float currentTime;
    private bool isRunning;
    [SerializeField] public TextMeshProUGUI timerText;
    [SerializeField] public Animator instructionsAnim;

   

    private void Start()
    {
        currentTime = totalTime;
        isRunning = true;
    }

    public void Update()
    {
        if (gm.Correct == 3)
        {
            StartCoroutine(dieDelayAnim());
        }
        wrongWordAnimation();
        UpdateTimerDisplay();
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            if(currentTime < 7)
            {
                instructionsAnim.SetBool("timeUp", false);
            }

            if (currentTime <= 0.5f)
            {
                gm.attempts--;
                Handheld.Vibrate();
                instructionsAnim.SetBool("timeUp", true);
                wrongWordAnimation();
                isRunning = false;
                ResetTimer();
            }
        }
    }

    public void wrongWordAnimation()
    {
        if (gm.wrongWord || currentTime < 1f)
        {
            if (gm.attempts == 2 ) 
            {
                anim.SetBool("Equipped", true);
            }
            else if(gm.attempts == 1 )
            {
                anim.SetBool("IsAiming", true);
                anim.SetBool("Equipped", false);
            }
            else if(gm.attempts == 0)
            {
                StopTimer();
                anim.SetBool("IsShooting", true);
                anim.SetBool("IsAiming", false);
                anim.SetBool("Equipped", false);
            }
            else
            {
                anim.SetBool("Equipped", false);
                anim.SetBool("IsAiming", false);
                anim.SetBool("IsShooting", false);
               
            }
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
    private IEnumerator dieDelayAnim()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("IsDead", true);
        StopTimer();
    }
}
