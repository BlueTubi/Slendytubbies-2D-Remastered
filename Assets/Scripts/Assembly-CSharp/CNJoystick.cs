using UnityEngine;

public class CNJoystick : MonoBehaviour
{
	private delegate void InputHandler();

	public bool remoteTesting;

	public PlacementSnap placementSnap = PlacementSnap.leftBottom;

	public Rect tapZone;

	private GameObject joystickBase;

	private GameObject joystick;

	private float joystickBaseRadius;

	private float frustumHeight;

	private float frustumWidth;

	private int myFingerId = -1;

	private Vector3 invokeTouchPosition;

	private Vector3 joystickRelativePosition;

	private Vector3 screenPointInUnits;

	private Vector3 relativeExtentSummand;

	private bool isTweaking;

	private InputHandler CurrentInputHandler;

	private float distanceToCamera = 0.5f;

	private float halfScreenHeight;

	private float halfScreenWidth;

	private float screenHeight;

	private float screenWidth;

	private Vector3 snapPosition;

	private Vector3 cleanPosition;

	private Transform joystickBaseTransform;

	private Transform joystickTransform;

	private Transform transformCache;

	public Camera CurrentCamera { get; set; }

	public event JoystickMoveEventHandler JoystickMovedEvent;

	public event FingerLiftedEventHandler FingerLiftedEvent;

	public event FingerTouchedEventHandler FingerTouchedEvent;

	private void Awake()
	{
		CurrentCamera = base.transform.parent.GetComponent<Camera>();
		transformCache = base.transform;
		joystickBase = transformCache.Find("Base").gameObject;
		joystickBaseTransform = joystickBase.transform;
		joystick = base.transform.Find("Joystick").gameObject;
		joystickTransform = joystick.transform;
		InitialCalculations();
		CurrentInputHandler = MouseInputHandler;
		if (remoteTesting)
		{
			CurrentInputHandler = TouchInputHandler;
		}
	}

	private void Update()
	{
		CurrentInputHandler();
	}

	private void TouchInputHandler()
	{
		int touchCount = Input.touchCount;
		if (!isTweaking)
		{
			for (int i = 0; i < touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				if (touch.phase == TouchPhase.Began && TouchOccured(touch.position))
				{
					myFingerId = touch.fingerId;
					if (this.FingerTouchedEvent != null)
					{
						this.FingerTouchedEvent();
					}
				}
			}
			return;
		}
		bool flag = false;
		for (int j = 0; j < touchCount; j++)
		{
			Touch touch2 = Input.GetTouch(j);
			if (myFingerId == touch2.fingerId && touch2.phase == TouchPhase.Ended)
			{
				ResetJoystickPosition();
				flag = true;
				if (this.FingerLiftedEvent != null)
				{
					this.FingerLiftedEvent();
				}
			}
		}
		if (!flag)
		{
			int num = FindMyFingerId();
			if (num != -1)
			{
				TweakJoystick(Input.GetTouch(num).position);
			}
		}
	}

	private void MouseInputHandler()
	{
		if (Input.GetMouseButtonDown(0))
		{
			TouchOccured(Input.mousePosition);
		}
		if (Input.GetMouseButton(0) && isTweaking)
		{
			TweakJoystick(Input.mousePosition);
		}
		if (Input.GetMouseButtonUp(0))
		{
			ResetJoystickPosition();
		}
	}

	private void InitialCalculations()
	{
		halfScreenHeight = CurrentCamera.orthographicSize;
		halfScreenWidth = halfScreenHeight * CurrentCamera.aspect;
		screenHeight = halfScreenHeight * 2f;
		screenWidth = halfScreenWidth * 2f;
		snapPosition = default(Vector3);
		cleanPosition = new Vector3(0f - halfScreenWidth, 0f - halfScreenHeight);
		switch (placementSnap)
		{
		case PlacementSnap.leftBottom:
			snapPosition.x = 0f - halfScreenWidth + tapZone.width / 2f - tapZone.x;
			snapPosition.y = 0f - halfScreenHeight + tapZone.height / 2f - tapZone.y;
			tapZone.x = 0f;
			tapZone.y = 0f;
			break;
		case PlacementSnap.leftTop:
			snapPosition.x = 0f - halfScreenWidth + tapZone.width / 2f - tapZone.x;
			snapPosition.y = halfScreenHeight - tapZone.height / 2f - tapZone.y;
			tapZone.x = 0f;
			tapZone.y = screenHeight - tapZone.height;
			break;
		case PlacementSnap.rightBottom:
			snapPosition.x = halfScreenWidth - tapZone.width / 2f - tapZone.x;
			snapPosition.y = 0f - halfScreenHeight + tapZone.height / 2f - tapZone.y;
			tapZone.x = screenWidth - tapZone.width;
			tapZone.y = 0f;
			break;
		case PlacementSnap.rightTop:
			snapPosition.x = halfScreenWidth - tapZone.width / 2f - tapZone.x;
			snapPosition.y = halfScreenHeight - tapZone.height / 2f - tapZone.y;
			tapZone.x = screenWidth - tapZone.width;
			tapZone.y = screenHeight - tapZone.height;
			break;
		}
		transformCache.localPosition = snapPosition;
		SpriteRenderer spriteRenderer = joystickBase.GetComponent<Renderer>() as SpriteRenderer;
		joystickBaseRadius = spriteRenderer.bounds.extents.x;
	}

	private bool TouchOccured(Vector3 touchPosition)
	{
		ScreenPointToRelativeFrustumPoint(touchPosition);
		if (tapZone.Contains(screenPointInUnits))
		{
			isTweaking = true;
			invokeTouchPosition = screenPointInUnits;
			transformCache.localPosition = cleanPosition;
			joystickBaseTransform.localPosition = invokeTouchPosition;
			joystickTransform.localPosition = invokeTouchPosition;
			return true;
		}
		return false;
	}

	private void TweakJoystick(Vector3 desiredPosition)
	{
		ScreenPointToRelativeFrustumPoint(desiredPosition);
		Vector3 relativeVector = screenPointInUnits - invokeTouchPosition;
		if (relativeVector.sqrMagnitude <= joystickBaseRadius * joystickBaseRadius)
		{
			joystickTransform.localPosition = screenPointInUnits;
			relativeVector /= joystickBaseRadius;
		}
		else
		{
			joystickTransform.localPosition = invokeTouchPosition + relativeVector.normalized * joystickBaseRadius;
			relativeVector.Normalize();
		}
		if (this.JoystickMovedEvent != null)
		{
			this.JoystickMovedEvent(relativeVector);
		}
	}

	private void ResetJoystickPosition()
	{
		isTweaking = false;
		transformCache.localPosition = snapPosition;
		joystickBaseTransform.localPosition = Vector3.zero;
		joystickTransform.localPosition = Vector3.zero;
		myFingerId = -1;
	}

	private void ScreenPointToRelativeFrustumPoint(Vector3 point)
	{
		float num = point.x / (float)Screen.width;
		float num2 = point.y / (float)Screen.height;
		screenPointInUnits.x = num * screenWidth;
		screenPointInUnits.y = num2 * screenHeight;
		screenPointInUnits.z = 0f;
	}

	private int FindMyFingerId()
	{
		int touchCount = Input.touchCount;
		for (int i = 0; i < touchCount; i++)
		{
			if (Input.GetTouch(i).fingerId == myFingerId)
			{
				return i;
			}
		}
		return -1;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Vector3 center = new Vector3(base.transform.position.x + tapZone.x, base.transform.position.y + tapZone.y, base.transform.position.z);
		Gizmos.DrawWireCube(center, new Vector3(tapZone.width, tapZone.height));
	}
}
