using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public float Health { get; }
    public float MaxHealth { get; }

    public void SetHealth(float setHealth);
    public void ApplyDamage(float damage);
    public void Kill();
}
