using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NavMeshAgent agent;

    private Transform target;

    [Header("Settings")]
    [SerializeField] private float chaseRange = 20f;

    [SerializeField] private float attackRange = 2f;

    private EnemyMeleeAttack meleeAttack;

    private void Awake()
    {
        meleeAttack =
            GetComponent<EnemyMeleeAttack>();

        agent.enabled = false;
    }

    private void OnEnable()
    {
        Invoke(
            nameof(EnableAgent),
            0.05f);
    }

    private void EnableAgent()
    {
        if (agent == null)
            return;

        agent.enabled = true;
    }

    private void OnDisable()
    {
        if (agent != null)
        {
            agent.enabled = false;
        }
    }

    public void SetTarget(
        Transform newTarget)
    {
        target = newTarget;

        if (meleeAttack != null)
        {
            meleeAttack.SetTarget(
                newTarget);
        }
    }

    private void Update()
    {
        if (target == null)
            return;

        if (!agent.enabled)
            return;

        float distance =
            Vector3.Distance(
                transform.position,
                target.position);

        if (distance > chaseRange)
        {
            agent.isStopped = true;

            return;
        }

        if (distance <= attackRange)
        {
            agent.isStopped = true;

            transform.LookAt(
                target);

            meleeAttack.TryAttack();

            return;
        }

        agent.isStopped = false;

        agent.SetDestination(
            target.position);
    }
}