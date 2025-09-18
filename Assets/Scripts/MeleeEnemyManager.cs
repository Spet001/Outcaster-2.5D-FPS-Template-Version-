using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent), typeof(Animator), typeof(EnemyStats))]
public class MeleeEnemyAI : MonoBehaviour
{
    [Header("Configurações de Combate")]
    public float attackRange = 2.5f;
    public int attackDamage = 20;
    public float attackCooldown = 2f; // Tempo entre ataques da pra editar

    [Header("Referências")]
    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private EnemyStats enemyStats; // Referênciar o script de vida

    private float lastAttackTime = -999f;

    void Start()
    {
        // Pega as referências dos componentes no mesmo GameObject
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<EnemyStats>(); 

        // Encontra o jogador pela tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("IA do Inimigo: Jogador com a tag 'Player' não encontrado!, o player tem tag???", this);
            enabled = false; // Desativa a IA se não houver jogador
            return;
        }

        // Configurações essenciais para 2.5D
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        // A IA para de funcionar se estiver morta (verificando pelo seu script) ou se não houver jogador
        if (enemyStats.IsDead() || player == null) //TODO Precisamos adicionar o método IsDead() em EnemyStats
        {
            if (agent.isOnNavMesh) agent.isStopped = true;
            animator.SetBool("isWalking", false);
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= agent.stoppingDistance)
        {
            // Perto o suficiente para atacar
            agent.isStopped = true;
            animator.SetBool("isWalking", false);
            FacePlayer();

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
            }
        }
        else
        {
            // Longe, precisa andar
            agent.isStopped = false;
            agent.SetDestination(player.position);
            animator.SetBool("isWalking", true);
        }
    }

    private void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void Attack()
    {
        lastAttackTime = Time.time;
        animator.SetTrigger("attack"); // Dispara a animação de ataque
    }

    // MÉTODO CHAMADO PELO EVENTO DE ANIMAÇÃO
    // Este método causa o dano no jogador
    public void DealDamage()
    {
        if (enemyStats.IsDead()) return;

        // Verifica novamente a distância no momento exato do golpe
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            // Busca o script PlayerStats no jogador e chama o método TakeDamage
            PlayerStats playerHealth = player.GetComponent<PlayerStats>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }
}