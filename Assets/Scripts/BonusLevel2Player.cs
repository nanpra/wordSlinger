using System.Collections;
using UnityEngine;

public class BonusLevel2Player : MonoBehaviour
{
    [SerializeField] public Animator anim;
    [SerializeField] public GameManager gm;
    public Transform playerRot;
    public Transform enemy1;
    public Transform enemy2;
    public Transform enemy3;

    private void Update()
    {
        if(gm.isCorrect)
        {
            shootAnim();
        }
        else if (gm.attempts == 0)
        {
            anim.SetBool("Dead", true);
        }
    }
    public void shootAnim()
    {
        if (gm.Correct >= 1 && gm.Correct <= 3)
        {
            playerRot.LookAt(GetEnemyTransform(gm.Correct));
            anim.SetBool("shoot", true);
            StartCoroutine(ShootFalseDelay());
        }
        else
        {
            anim.SetBool("shoot", false);
        }
    }

    Transform GetEnemyTransform(int index)
    {
        switch (index)
        {
            case 1:
                return enemy1;
            case 2:
                return enemy2;
            case 3:
                gm.bulletScript.enabled = true;
                gm.bulletScript.moveSpeed = 0.2f;
                StartCoroutine(gm.bulletScript.MoveToTarget());
                return enemy3;
            default:
                return null;
        }
    }

    private IEnumerator ShootFalseDelay()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("shoot", false);
    }
}
