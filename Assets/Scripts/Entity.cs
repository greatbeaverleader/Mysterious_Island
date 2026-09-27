using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected int lives = 5;

    public virtual void GetDamage()
    {
        lives--;
        if (lives < 1)
            Die();
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
