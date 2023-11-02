using UnityEngine;

public class PlayerMovementPC : MonoBehaviour
{
	public Transform flashlight;

	protected float runSpeed = 12f;

	protected float walkspeed = 6f;

	private CharacterController characterController;

	private Camera mainCamera;

	private float gravity;

	private Vector3 totalMove;

	private bool tweakedLastFrame;

	public bool isrunning;

	protected float speed;

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		mainCamera = Camera.main;
		gravity = 0f - Physics.gravity.y;
		totalMove = Vector3.zero;
		tweakedLastFrame = false;
		speed = runSpeed;
	}

	private void Start()
	{
	}

	public void WalkSpeed(float theSpeed)
	{
		walkspeed = theSpeed;
	}

	public void RunSpeed(float theSpeed)
	{
		runSpeed = theSpeed;
	}

	private void Update()
	{
		if (!tweakedLastFrame)
		{
			totalMove = Vector3.zero;
		}
		if (!characterController.isGrounded)
		{
		}
		characterController.Move(totalMove * Time.deltaTime);
		tweakedLastFrame = false;
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			characterController.Move(Vector3.forward * Time.deltaTime * speed);
		}
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			characterController.Move(Vector3.back * Time.deltaTime * speed);
		}
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			characterController.Move(Vector3.left * Time.deltaTime * speed);
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			characterController.Move(Vector3.right * Time.deltaTime * speed);
		}
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Plane plane = new Plane(Vector3.up, base.transform.position);
		float enter = 0f;
		if (plane.Raycast(ray, out enter))
		{
			Vector3 point = ray.GetPoint(enter);
			Quaternion to = Quaternion.LookRotation(point - base.transform.position);
			flashlight.rotation = Quaternion.Slerp(base.transform.rotation, to, speed * Time.time);
		}
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			speed = runSpeed;
			isrunning = true;
		}
		else
		{
			speed = walkspeed;
			isrunning = false;
		}
		if (base.gameObject.tag == "Monster")
		{
			Vector3 position = base.transform.position;
			position.y = GameObject.FindWithTag("PlayerStartPoint").transform.position.y;
			base.transform.position = position;
		}
	}
}
