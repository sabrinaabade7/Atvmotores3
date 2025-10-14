using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] private float speed = 1f; // Velocidade do inimigo

    [Header("Detecção de chão")]
    [SerializeField] private Transform groundCheck; // Objeto para checar o chão
    [SerializeField] private float checkDistance = 0.5f; // Distância do raycast
    [SerializeField] private LayerMask groundLayer; // Camada do chão

    private bool movingRight = true;

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        // Move o inimigo
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        transform.Translate(direction * speed * Time.deltaTime);

        // Raycast para verificar se há chão à frente
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, groundLayer);
        if (!groundInfo.collider)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;

        // Inverte o sprite
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        // Visualiza o raycast no editor
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * checkDistance);
        }
    }
}