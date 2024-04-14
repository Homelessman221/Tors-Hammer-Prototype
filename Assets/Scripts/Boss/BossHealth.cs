using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private IntVariable bossHealth;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hurtColor;
    private Color defaultColor;
    private float timeColored;

    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;


    [SerializeField] GameEvent bossTakesDamage;
    private void Start()
    {
        defaultColor = spriteRenderer.color;
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");
        bossHealth.Value = 40;
    }
    private void Update()
    {
        if(bossHealth.Value <= 0)
        {
            SceneManager.LoadScene(2);
        }
        
        if(timeColored >= 0)
        {

        timeColored -= Time.deltaTime;
        }


        if (timeColored <= 0)
        {
           
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("boss take damage");
        bossHealth.Value -= 1;
        timeColored = 0.1f;
        spriteRenderer.color = hurtColor;
        spriteRenderer.material.shader = shaderGUItext;
        StartCoroutine(SetColorDefault());
        bossTakesDamage.Raise();
    }

    private IEnumerator SetColorDefault()
    {
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = defaultColor;
        spriteRenderer.material.shader = shaderSpritesDefault;
    }



}
