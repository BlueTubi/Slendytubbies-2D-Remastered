using System.Collections;
using Photon;
using UnityEngine;

public class MainNPCMechanics : Photon.MonoBehaviour
{
	public UnityEngine.AI.NavMeshAgent agent;

	public Transform target;

	private Vector3 oldpos;

	public FindPlayers fp;

	public float spotDistance = 20f;

	public int moveSpeed = 6;

	public int chaseSpeed = 10;

	public bool isSpotted;

	public int AIHealth = 100;

	public Transform model;

	public AudioClip scream;

	public bool isScreaming;

	public float screamDelay = 2.5f;

	private float dis1 = 20f;

	private float dis2 = 10f;

	private int custardcount;

	public float CurDistance;

	public Quaternion corRot;

	private int orgmoveSpeed;

	private void Awake()
	{
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		fp = GetComponent<FindPlayers>();
		oldpos = base.transform.position;
		orgmoveSpeed = moveSpeed;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (!base.photonView.isMine)
		{
			agent.enabled = false;
		}
		else
		{
			agent.enabled = true;
		}
		custardcount = GameObject.FindGameObjectsWithTag("custard").Length;
		if (custardcount < 3)
		{
			moveSpeed = chaseSpeed;
		}
		else
		{
			moveSpeed = orgmoveSpeed;
		}
		target = fp.target;
		agent.updateRotation = false;
		if (!target)
		{
			return;
		}
		CurDistance = Vector3.Distance(target.position, base.transform.position);
		if (!isSpotted)
		{
			if (base.photonView.isMine)
			{
				Vector3 position = target.position;
				position.y = 0f;
				agent.speed = moveSpeed;
				agent.SetDestination(position);
			}
		}
		else
		{
			if (base.photonView.isMine)
			{
				Vector3 position2 = target.position;
				position2.y = 0f;
				agent.speed = chaseSpeed;
				agent.SetDestination(position2);
			}
			if (!GetComponent<AudioSource>().isPlaying && !isScreaming)
			{
				StartCoroutine(Scream());
			}
		}
		if (CurDistance < spotDistance)
		{
			isSpotted = true;
		}
		if (CurDistance > spotDistance)
		{
			isSpotted = false;
		}
	}

	private IEnumerator Scream()
	{
		isScreaming = true;
		GetComponent<AudioSource>().clip = scream;
		GetComponent<AudioSource>().Play();
		yield return new WaitForSeconds(screamDelay);
		isScreaming = false;
	}
}
