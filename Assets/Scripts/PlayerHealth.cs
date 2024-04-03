using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private IntVariable health;
    [SerializeField] private float iFrameTime;
    private float iFrameTimer;

    [SerializeField] private GameEvent playerTakeDamage;

    [SerializeField] private float blinkTime;
    private float blinkTimer;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        health.Value = 5;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if(iFrameTimer <= 0)
        {

            playerTakeDamage.Raise();
            health.Value -= 1;
            iFrameTimer = iFrameTime;
        }
    }

    private void Update()
    {
        if(health.Value <= 0)
        {
            SceneManager.LoadScene(0);
        }
        if(iFrameTimer > -1)
        {

        iFrameTimer -= Time.deltaTime;
            blinkTimer -= Time.deltaTime;
            if(blinkTimer < 0)
            {
                blinkTimer = blinkTime;
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }
        }
        if(iFrameTimer < 0)
        {
            spriteRenderer.enabled = true;
        }

    }

    //Add hurt state co ruotine stuff
}
