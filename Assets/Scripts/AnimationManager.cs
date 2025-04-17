using System.Collections;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] public Animator anim;
    [SerializeField] public GameManager gm;
    private void Update()
    {
        shootAnim();
    }

    public void shootAnim()
    {
        if (gm.attempts == 0)
        {
            StartCoroutine(dieDelayAnim());
        }
        else
        {
            switch (gm.Correct)
            {
                case 1:
                    anim.SetBool("Equipped", true);
                    break;
                case 2:
                    anim.SetBool("IsAiming", true);
                    anim.SetBool("Equipped", false);
                    break;
                case 3:
                    if (gm.e != null)
                    {
                        gm.e.StopTimer();
                    }

                    anim.SetBool("IsShooting", true);
                    anim.SetBool("IsAiming", false);
                    anim.SetBool("Equipped", false);
                    break;
                default:
                    anim.SetBool("Equipped", false);
                    anim.SetBool("IsAiming", false);
                    anim.SetBool("IsShooting", false);
                    break;
            }
        }
    }
    public void BulletAnimation()
    {

        gm.bulletScript.enabled = true;
        StartCoroutine(gm.bulletScript.MoveToTarget());
    }

    private IEnumerator dieDelayAnim()
    {
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("IsDead", true);
    }
}