using Photon;
using UnityEngine;

public class TinkWinkiMechanics : Photon.MonoBehaviour
{
	private float CurDistance;

	public FindPlayers fp;

	public Transform target;

	public AIDetect targetScript;

	public float maxDistance = 20f;

	public float closestDistance = 1f;

	public float safeDistance = 4f;

	private int moveSpeed = 9;

	private int chaseSpeed = 10;

	public bool isSpotted;

	public int AIHealth = 100;

	public Transform model;

	public SmoothNPC snpc;

	public AudioClip scream;

	private bool togglevisible;

	private bool isscreaming;

	private float dis1 = 20f;

	private float dis2 = 10f;

	private int custardcount;

	private void Start()
	{
	}

	private void Update()
	{
		custardcount = GameObject.FindGameObjectsWithTag("custard").Length;
		if (custardcount <= 8 && custardcount > 5)
		{
			moveSpeed = 11;
		}
		else if (custardcount <= 5 && custardcount > 3)
		{
			moveSpeed = 12;
		}
		else if (custardcount <= 3 && custardcount > 2)
		{
			moveSpeed = 15;
		}
		else if (custardcount <= 2)
		{
			moveSpeed = 30;
		}
		else
		{
			moveSpeed = 9;
		}
		target = fp.target;
		targetScript = fp.target.gameObject.GetComponent<AIDetect>();
		if ((bool)target)
		{
			CurDistance = Vector3.Distance(target.position, base.transform.position);
			if (!isSpotted)
			{
				if (targetScript.CanFollowTarget)
				{
					if (CurDistance > safeDistance && CurDistance < maxDistance && base.photonView.isMine)
					{
						snpc.visual = 0;
						togglevisible = true;
						Vector3 position = target.position;
						position.y = 0f;
						base.transform.position = Vector3.MoveTowards(base.transform.position, position, (float)moveSpeed * Time.deltaTime);
					}
				}
				else if (base.photonView.isMine && togglevisible)
				{
					if (Random.Range(0, 2) == 0)
					{
						snpc.visual = 1;
					}
					togglevisible = false;
				}
			}
			if (isSpotted)
			{
				if (CurDistance > closestDistance && CurDistance < maxDistance)
				{
					if (base.photonView.isMine)
					{
						snpc.visual = 0;
						Vector3 position2 = target.position;
						position2.y = 0f;
						base.transform.position = Vector3.MoveTowards(base.transform.position, position2, (float)chaseSpeed * Time.deltaTime);
					}
				}
				else if (base.photonView.isMine)
				{
					snpc.visual = 1;
				}
			}
			if (AIHealth < 100 && CurDistance < maxDistance)
			{
				isSpotted = true;
			}
			if (CurDistance > maxDistance)
			{
				isSpotted = false;
			}
		}
		if (snpc.visual == 0)
		{
			model.gameObject.active = false;
			isscreaming = false;
			GetComponent<Collider>().enabled = false;
			return;
		}
		Vector3 position3 = target.position;
		if (CurDistance <= 24f)
		{
			position3.y = 0f;
			base.transform.position = Vector3.MoveTowards(base.transform.position, position3, (float)chaseSpeed * Time.deltaTime);
			GetComponent<Collider>().enabled = true;
			if (!GetComponent<AudioSource>().isPlaying && !isscreaming)
			{
				GetComponent<AudioSource>().clip = scream;
				GetComponent<AudioSource>().Play();
				isscreaming = true;
			}
		}
		model.gameObject.active = true;
	}
}
