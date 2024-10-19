using UnityEngine;
using UnityEngine.AI;

public class AttackGuy : MonoBehaviour
{
	[SerializeField] float m_moveSpeed = 2;
	[SerializeField] Transform m_attackPoint;

	NavMeshAgent m_navAgent;
	Animator m_animator;
	bool m_bWalking;

	void Awake()
	{
		m_navAgent = GetComponent<NavMeshAgent>();
		m_animator = GetComponent<Animator>();

		SetWalking(true);

		m_navAgent.speed = m_moveSpeed;
	}

	void FixedUpdate()
	{
		if (m_bWalking)
			m_navAgent.SetDestination(m_attackPoint.position);
	}

	void SetWalking(bool bValue)
	{
		m_animator.SetBool("Walking", bValue);
		m_bWalking = bValue;

		if (bValue)
			m_animator.speed = m_moveSpeed;
		else
			m_animator.speed = 1;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "AttackPoint")
		{
			m_animator.SetBool("Attacking", true);
			SetWalking(false);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "AttackPoint")
		{
			m_animator.SetBool("Attacking", false);
			SetWalking(true);
		}
	}
}
