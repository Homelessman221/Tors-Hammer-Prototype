using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private IntVariable bossHealth;

    private void OnTriggerEnter(Collider other)
    {
        print("boss take damage");
        bossHealth.Value -= 1;
    }
    private void Update()
    {
        if(bossHealth.Value <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
