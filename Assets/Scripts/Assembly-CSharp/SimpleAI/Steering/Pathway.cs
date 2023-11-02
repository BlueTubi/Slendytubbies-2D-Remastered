using UnityEngine;

namespace SimpleAI.Steering
{
	public class Pathway
	{
		public virtual Vector3 MapPathDistanceToPoint(float pathDistance)
		{
			return Vector3.zero;
		}

		public virtual float MapPointToPathDistance(Vector3 point)
		{
			return 0f;
		}
	}
}
