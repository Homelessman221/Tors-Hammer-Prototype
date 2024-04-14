using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] IntVariable bossHealth;
    [SerializeField] float maxHealth;
    private void Start()
    {
        
    }
    public void bossTakeDamage()
    {
        print("Boss bar go down");
        healthBar.fillAmount = bossHealth.Value / maxHealth;
    }
}
