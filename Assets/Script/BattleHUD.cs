using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Slider healthBar;
    public Text text;
    public void setHUD(Unit unit)
    {

        healthBar.maxValue = unit.maxHealth;
        healthBar.value = unit.health;
        text.text = unit.unitName;
    }
}
