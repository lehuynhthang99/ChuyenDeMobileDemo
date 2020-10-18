using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankInfo : BaseMonoBehaviour,IEquatable<TankInfo>
{
    public int id;
    public Rigidbody rigidbody;
    public TankHealth tankHealth;
    public TankShootingBase tankShooting;
    public TankMovement tankMovement;
    public GameObject tankCanvas;
    public TankEffects tankEffects;
    public SkillBase skill;

    

    public bool Equals(TankInfo other)
    {
        return id == other.id;
    }
}
