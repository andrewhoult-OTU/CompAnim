using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
	[SerializeField]
	public PathManager pathManager;

	List<Waypoint> thePath;
	Waypoint target;

	public float MoveSpeed;
	public float RotateSpeed;


	public Animator animator;
	public bool isWalking;


	void Start()
	{
		thePath = pathManager.GetPath();
		if (thePath != null && thePath.Count > 0)
		{
			target = thePath[0];
		}

		isWalking = false;
		animator.SetBool("IsWalking", isWalking);
	}

	void MoveForward()
	{
		float stepSize = Time.deltaTime * MoveSpeed;
		float distanceToTarget = Vector3.Distance(transform.position, target.pos);
		if (distanceToTarget < stepSize)
		{
			// we will overshoot the target,
			// so we should do something smarter here
			return;
		}
		// take a step forward
		Vector3 moveDir = Vector3.forward;
		transform.Translate(moveDir * stepSize);
	}

	void RotateTowardsTarget()
	{
		float stepSize = RotateSpeed * Time.deltaTime;

		Vector3 targetDir = target.pos - transform.position;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
		transform.rotation = Quaternion.LookRotation(newDir);
	}

	void Update()
	{
		if (Input.anyKeyDown)
		{
			isWalking = !isWalking;
			animator.SetBool("IsWalking", isWalking);
		}

		if (isWalking)
		{
			RotateTowardsTarget();
			MoveForward();
		}
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			isWalking = false;
			animator.SetBool("IsWalking", isWalking);
			return;
		}
		target = pathManager.GetNextTarget();
	}
}
