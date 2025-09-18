using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Death Effects")]
    public GameObject deathEffectPrefab;
    public AudioClip deathSound;
    private AudioSource audioSource;

    private Renderer rend;
    private Collider coll;
    private Animator animator; 
    private bool isDead = false; 

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        rend = GetComponentInChildren<Renderer>(); //InChildren é bom para pegar o sprite filho
        coll = GetComponent<Collider>();
        animator = GetComponent<Animator>(); 
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return; // Não recebe dano se já estiver morto

        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return; // Garante que a lógica de morte rode apenas uma vez
        isDead = true;

        Debug.Log($"{gameObject.name} died!");

        if (animator != null)
        {
            animator.SetTrigger("die"); //  ISSO VAI ATIVAR A ANIMAÇÃO DE MORTE
        }

        if (deathEffectPrefab)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        if (audioSource && deathSound)
        {
            audioSource.PlayOneShot(deathSound);
        }

        if (coll) coll.enabled = false; // Desativa o colisor

        // A IA vai desativar o renderer, então podemos remover essa linha
        // if (rend) rend.enabled = false;

        Destroy(gameObject, 5f); // Aumenta o tempo para a animação de morte tocar
    }

    // MÉTODO PÚBLICO PARA A IA SABER SE O INIMIGO MORREU
    public bool IsDead() // <-- ADICIONE ESTE MÉTODO INTEIRO
    {
        return isDead;
    }
}