using System.Collections;
using UnityEngine;

public class BonusLevel1 : MonoBehaviour
{
    [SerializeField] public Animator anim;
    [SerializeField] public GameManager gm;
    [SerializeField] public Transform playerRot;
    [SerializeField] public Transform bottle1;
    [SerializeField] public Transform bottle2;
    [SerializeField] public Transform bottle3;

    public MeshFilter BrokenGlassMesh;

  

    private void Update()
    {
        if(gm.isCorrect)
        {
            rotatePlayer();
        }
    }
    public void shootAnim()
    {
        anim.SetBool("shoot" ,true);
    }
    public void rotatePlayer()
    {
        gm.attempts = 10;
        if(gm.Correct == 1)
        {
            bottle1.transform.GetComponent<MeshFilter>().mesh = BrokenGlassMesh.mesh;
            bottle1.transform.GetComponentInChildren<ParticleSystem>().Play();
            shootAnim();
            StartCoroutine(falseDelay());
        }
        else if(gm.Correct == 2)
        {
            playerRot.LookAt(bottle2.position);
            bottle2.transform.GetComponent<MeshFilter>().mesh = BrokenGlassMesh.mesh;
            bottle2.transform.GetComponentInChildren<ParticleSystem>().Play();
            shootAnim();
            StartCoroutine(falseDelay());
        }
        else if(gm.Correct == 3)
        {
            playerRot.LookAt(bottle3.position);
            bottle3.transform.GetComponent<MeshFilter>().mesh = BrokenGlassMesh.mesh;
            bottle3.transform.GetComponentInChildren<ParticleSystem>().Play();
            shootAnim();
            StartCoroutine(falseDelay());
        }
    }

    IEnumerator falseDelay()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("shoot", false);
    }
}
