using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSourceRef;
    [SerializeField]
    AudioClip shootclip, holsterClip, hammerUpClick, Death,BottleBreakClip;

    private void Awake()
    {
        audioSourceRef = GetComponent<AudioSource>();
    }
    public void shootSound()
    {
        audioSourceRef.clip = shootclip;
        audioSourceRef.Play();
    }
    public void gunHolster()
    {
        audioSourceRef.clip = holsterClip;
        audioSourceRef.Play();
    }
    public void gunHammerUp()
    {
        audioSourceRef.clip = hammerUpClick;
        audioSourceRef.Play();
    }
    public void DeathSound()
    {
        audioSourceRef.clip = Death;
        audioSourceRef.Play();
    }

    public void BottleBreak()
    {
        audioSourceRef.clip = BottleBreakClip;
        audioSourceRef.Play();
    }
}
