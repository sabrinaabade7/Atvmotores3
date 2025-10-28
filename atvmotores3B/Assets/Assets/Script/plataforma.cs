using UnityEngine;

public class ElevadorControlado : MonoBehaviour
{
    public Transform pontoSuperior;
    public Transform pontoInferior;
    public float velocidade = 2f;
    public bool estaSubindo = false;

    private Vector3 destinoAtual;

    void Start()
    {
        destinoAtual = transform.position;
    }

    void Update()
    {
        if (transform.position != destinoAtual)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinoAtual, velocidade * Time.deltaTime);
        }

        // Exemplo de controle pelo teclado:
        if (Input.GetKeyDown(KeyCode.E))
        {
            AlternarMovimento();
        }
    }

    public void AlternarMovimento()
    {
        if (estaSubindo)
        {
            destinoAtual = pontoInferior.position;
            estaSubindo = false;
        }
        else
        {
            destinoAtual = pontoSuperior.position;
            estaSubindo = true;
        }
    }
}