using UnityEngine;

namespace SimpleAI.Steering
{
	public class PolylinePathway : Pathway
	{
		private int pointCount;

		private Vector3[] points;

		private float segmentLength;

		private float segmentProjection;

		private Vector3 local;

		private Vector3 chosen;

		private Vector3 segmentNormal;

		private float[] lengths;

		private Vector3[] normals;

		private float totalPathLength;

		public Vector3[] Points
		{
			get
			{
				return points;
			}
		}

		public int PointCount
		{
			get
			{
				return pointCount;
			}
		}

		public PolylinePathway()
		{
		}

		public PolylinePathway(int _pointCount)
		{
			Initialize(_pointCount, null);
		}

		public PolylinePathway(int _pointCount, Vector3[] _points)
		{
			Initialize(_pointCount, _points);
		}

		public void ReInitialize(int _pointCount, Vector3[] _points)
		{
			Initialize(_pointCount, _points);
		}

		private void Initialize(int _pointCount, Vector3[] _points)
		{
			pointCount = _pointCount;
			totalPathLength = 0f;
			lengths = new float[pointCount];
			points = new Vector3[pointCount];
			normals = new Vector3[pointCount];
			if (_points == null)
			{
				return;
			}
			for (int i = 0; i < pointCount; i++)
			{
				int num = ((0 == 0) ? i : 0);
				points[i] = _points[num];
				if (i > 0)
				{
					normals[i] = points[i] - points[i - 1];
					lengths[i] = normals[i].magnitude;
					normals[i] *= 1f / lengths[i];
					totalPathLength += lengths[i];
				}
			}
		}

		public float GetTotalPathLength()
		{
			return totalPathLength;
		}

		public override float MapPointToPathDistance(Vector3 point)
		{
			float num = float.MaxValue;
			float num2 = 0f;
			float result = 0f;
			for (int i = 1; i < pointCount; i++)
			{
				segmentLength = lengths[i];
				segmentNormal = normals[i];
				float num3 = PointToSegmentDistance(point, points[i - 1], points[i]);
				if (num3 < num)
				{
					num = num3;
					result = num2 + segmentProjection;
				}
				num2 += segmentLength;
			}
			return result;
		}

		public override Vector3 MapPathDistanceToPoint(float pathDistance)
		{
			float num = pathDistance;
			if (pathDistance < 0f)
			{
				return points[0];
			}
			if (pathDistance >= totalPathLength)
			{
				return points[pointCount - 1];
			}
			Vector3 result = Vector3.zero;
			for (int i = 1; i < pointCount; i++)
			{
				segmentLength = lengths[i];
				if (segmentLength < num)
				{
					num -= segmentLength;
					continue;
				}
				float alpha = num / segmentLength;
				result = interpolate(alpha, points[i - 1], points[i]);
				break;
			}
			return result;
		}

		public float PointToSegmentDistance(Vector3 point, Vector3 ep0, Vector3 ep1)
		{
			local = point - ep0;
			segmentProjection = Vector3.Dot(segmentNormal, local);
			if (segmentProjection < 0f)
			{
				chosen = ep0;
				segmentProjection = 0f;
				return (point - ep0).magnitude;
			}
			if (segmentProjection > segmentLength)
			{
				chosen = ep1;
				segmentProjection = segmentLength;
				return (point - ep1).magnitude;
			}
			chosen = segmentNormal * segmentProjection;
			chosen += ep0;
			return (point - chosen).magnitude;
		}

		public static float interpolate(float alpha, float x0, float x1)
		{
			return x0 + (x1 - x0) * alpha;
		}

		public static Vector3 interpolate(float alpha, Vector3 x0, Vector3 x1)
		{
			return x0 + (x1 - x0) * alpha;
		}

		public static Vector3 TruncateLength(Vector3 vec, float maxLength)
		{
			float magnitude = vec.magnitude;
			Vector3 result = vec;
			if (magnitude > maxLength)
			{
				result.Normalize();
				result *= maxLength;
			}
			return result;
		}
	}
}
