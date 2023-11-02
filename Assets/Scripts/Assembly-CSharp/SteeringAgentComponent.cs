using SimpleAI.Navigation;
using SimpleAI.Steering;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SteeringAgentComponent : MonoBehaviour
{
	public float m_arrivalDistance = 0.25f;

	public float m_maxSpeed = 2f;

	public float m_lookAheadDistance = 0.5f;

	public float m_slowingDistance = 1f;

	public float m_accelerationRate = 25f;

	public float m_gravitationalAccelerationRate;

	public Color m_debugPathColor = Color.yellow;

	public Color m_debugGoalColor = Color.red;

	public bool m_debugShowPath = true;

	public bool m_debugShowVelocity;

	private PolylinePathway m_path;

	private bool m_bArrived;

	private IPathTerrain m_pathTerrain;

	private void Awake()
	{
		m_path = null;
		m_bArrived = false;
		m_pathTerrain = null;
	}

	private void Update()
	{
		if (m_bArrived)
		{
			base.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
		else
		{
			if (m_path == null)
			{
				return;
			}
			float num = m_path.MapPointToPathDistance(base.transform.position);
			float pathDistance = num + m_lookAheadDistance;
			Vector3 vector = m_path.MapPathDistanceToPoint(pathDistance);
			vector.y = m_pathTerrain.GetTerrainHeight(vector);
			Vector3 velocity = Vector3.zero;
			Vector3 vector2 = m_path.Points[m_path.Points.Length - 1];
			vector2.y = m_pathTerrain.GetTerrainHeight(vector2);
			Vector3 position = base.transform.position;
			position.y = m_pathTerrain.GetTerrainHeight(position);
			float num2 = Vector3.Distance(position, vector2);
			if (num2 <= m_arrivalDistance)
			{
				if (!m_bArrived)
				{
					velocity = Vector3.zero;
					OnArrived();
				}
			}
			else
			{
				velocity = ComputeArrivalVelocity(vector, vector2, position, GetComponent<Rigidbody>().velocity);
			}
			GetComponent<Rigidbody>().velocity = velocity;
		}
	}

	private void OnDrawGizmos()
	{
		if (m_debugShowPath && m_path != null)
		{
			Gizmos.color = m_debugPathColor;
			for (int i = 1; i < m_path.PointCount; i++)
			{
				Vector3 from = m_path.Points[i - 1] + Vector3.up * 0.1f;
				Vector3 to = m_path.Points[i] + Vector3.up * 0.1f;
				Gizmos.DrawLine(from, to);
			}
			Gizmos.color = m_debugGoalColor;
			Vector3 center = m_path.Points[m_path.PointCount - 1] + Vector3.up * 0.1f;
			Gizmos.DrawWireSphere(center, m_arrivalDistance);
		}
		if (m_debugShowVelocity)
		{
			Gizmos.DrawRay(base.transform.position, GetComponent<Rigidbody>().velocity);
		}
	}

	public void SteerAlongPath(Vector3[] path, IPathTerrain pathTerrain)
	{
		m_pathTerrain = pathTerrain;
		m_bArrived = false;
		m_path = new PolylinePathway(path.Length, path);
	}

	public void StopSteering()
	{
		m_bArrived = false;
		m_path = null;
		GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

	private void OnArrived()
	{
		m_bArrived = true;
		SendMessageUpwards("OnSteeringRequestSucceeded", SendMessageOptions.DontRequireReceiver);
	}

	private Vector3 ComputeArrivalVelocity(Vector3 seekPos, Vector3 target, Vector3 position, Vector3 currentVelocity)
	{
		float magnitude = (target - position).magnitude;
		float value = m_maxSpeed * (magnitude / m_slowingDistance);
		float min = m_maxSpeed / 4f;
		float maxLength = Mathf.Clamp(value, min, m_maxSpeed);
		Vector3 vector = seekPos - position;
		vector.y = 0f;
		vector.Normalize();
		Vector3 vector2 = -Vector3.up * m_gravitationalAccelerationRate * GetComponent<Rigidbody>().mass;
		Vector3 vector3 = m_accelerationRate * vector + vector2;
		Vector3 vec = currentVelocity + vector3 * Time.deltaTime;
		return PolylinePathway.TruncateLength(vec, maxLength);
	}
}
