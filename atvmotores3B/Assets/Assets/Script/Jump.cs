using System;
using UnityEngine;
using TMPro; // Para usar o TextMeshProUGUI

public class Player : MonoBehaviour
{
    [Header("Movimentação")]
    public float velocidade = 40f;
    public float forcaDoPulo = 4f;

    private bool noChao = false;
    private bool andando = false;

    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Animator animator;

    [Header("Sistema de Mortes")]
    public int mortes = 0; // Contador de mortes
    public TextMeshProUGUI mortesText; // Texto da UI para mostrar as mortes

    private Vector3 posicaoInicial; // Posição inicial para resetar ao morrer

    private DeathCounter deathCounter;
    
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Guarda a posição inicial do jogador
        posicaoInicial = transform.position;

        // Atualiza o texto inicial das mortes
        if (mortesText != null)
            mortesText.text = "Mortes: " + mortes;

        deathCounter = GameObject.Find("Canvas").GetComponent<DeathCounter>();
    }

    void Update()
    {
        andando = false;

        // Movimento para esquerda
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-velocidade * Time.deltaTime, 0, 0);
            sprite.flipX = true;
            andando = true;
        }

        // Movimento para direita
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(velocidade * Time.deltaTime, 0, 0);
            sprite.flipX = false;
            andando = true;
        }

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            rb.AddForce(new Vector2(0, forcaDoPulo), ForceMode2D.Impulse);
        }

        // Ataque
        if (Input.GetKey(KeyCode.F))
        {
            animator.SetTrigger("Ataque");
        }

        // Animações
        animator.SetBool("Andando", andando);
        animator.SetBool("Pulo", !noChao);
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        // Detecta se está no chão
        if (colisao.gameObject.CompareTag("Chao"))
        {
            noChao = true;
        }

        // Detecta se tocou em algo perigoso (inimigo ou armadilha)
        if (colisao.gameObject.CompareTag("Perigo"))
        {
            Morrer();
        }
    }

    void OnCollisionExit2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Chao"))
        {
            noChao = false;
        }
    }

    // Função que trata a morte do jogador
    void Morrer()
    {
        mortes++; // Incrementa o número de mortes

        // Atualiza a UI
        if (mortesText != null)
            mortesText.text = "Mortes: " + mortes;

        // Reseta posição do jogador
        transform.position = posicaoInicial;

        // Reseta a física
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
        
        deathCounter.AddDeath();
    }
}