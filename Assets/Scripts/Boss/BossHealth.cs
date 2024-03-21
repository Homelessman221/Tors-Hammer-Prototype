using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private IntVariable bossHealth;


    private void Update()
    {
        if(bossHealth.Value <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("boss take damage");
        bossHealth.Value -= 1;
    }


}
