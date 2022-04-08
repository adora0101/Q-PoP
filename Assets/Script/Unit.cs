using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int maxHealth;
    public int health;
    public int attack;
    public int heal;

    public bool takeDamage(int damage)
    {
        health = health - damage;

        if (health > 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
}
