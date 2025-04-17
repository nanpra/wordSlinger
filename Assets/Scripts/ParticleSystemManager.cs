using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    [SerializeField] ParticleSystem BloodparticleSystem;
    [SerializeField] ParticleSystem gunShootparticleSystem;
    [SerializeField] ParticleSystem gunSmokeparticleSystem;
    [SerializeField] ParticleSystem gunHolsterparticleSystem;


    public void EnemyBloodEffect()
    {
        BloodparticleSystem.Play();
    }
    public void GunShootEffect()
    {
        gunShootparticleSystem.Play();
    }
    public void GunSmokEffect()
    {
        gunSmokeparticleSystem.Play();
    }
    public void GunHolsterEffect()
    {
        gunHolsterparticleSystem.Play();
    }
}
