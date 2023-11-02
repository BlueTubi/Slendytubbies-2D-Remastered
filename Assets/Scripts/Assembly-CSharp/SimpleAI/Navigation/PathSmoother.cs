using UnityEngine;

namespace SimpleAI.Navigation
{
	public static class PathSmoother
	{
		public static Vector3[] Smooth(Vector3[] roughPath, Vector3 startPos, Vector3 goalPos, Vector3[] aLeftPortalEndPts, Vector3[] aRightPortalEndPts)
		{
			Vector3[] array = null;
			if (roughPath.Length <= 2)
			{
				return new Vector3[2] { startPos, goalPos };
			}
			return ApplyFunnelPathSmoothing(aLeftPortalEndPts, aRightPortalEndPts, roughPath.Length + 1, startPos, goalPos);
		}

		private static Vector3[] ApplyFunnelPathSmoothing(Vector3[] aLeftEndPts, Vector3[] aRightEndPts, int maxNumSmoothPts, Vector3 startPos, Vector3 destPos)
		{
			int num = aLeftEndPts.Length + aRightEndPts.Length + 2 + 2;
			Vector3[] array = new Vector3[num];
			array[0] = startPos;
			array[1] = startPos;
			int num2 = 0;
			for (int i = 2; i < num - 2; i += 2)
			{
				array[i] = aLeftEndPts[num2];
				array[i + 1] = aRightEndPts[num2];
				num2++;
			}
			array[num - 2] = destPos;
			array[num - 1] = destPos;
			Vector3[] array2 = new Vector3[maxNumSmoothPts];
			int num3 = StringPull(array, num, array2, maxNumSmoothPts);
			Vector3[] array3 = new Vector3[num3];
			for (int j = 0; j < num3; j++)
			{
				array3[j] = array2[j];
			}
			return array3;
		}

		private static float TriArea2(Vector3 a, Vector3 b, Vector3 c)
		{
			float num = b.x - a.x;
			float num2 = b.z - a.z;
			float num3 = c.x - a.x;
			float num4 = c.z - a.z;
			return num3 * num2 - num * num4;
		}

		public static int StringPull(Vector3[] portals, int nportalPts, Vector3[] pts, int maxPts)
		{
			int num = 0;
			Vector3 vector = portals[0];
			Vector3 vector2 = portals[2];
			Vector3 vector3 = portals[3];
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			pts[0] = vector;
			num++;
			for (int i = 1; i < nportalPts / 2 && num < maxPts; i++)
			{
				Vector3 vector4 = portals[i * 2];
				Vector3 vector5 = portals[i * 2 + 1];
				if (TriArea2(vector, vector3, vector5) <= 0f)
				{
					if (!PathUtils.AreVertsTheSame(vector, vector3) && !(TriArea2(vector, vector2, vector5) > 0f))
					{
						pts[num] = vector2;
						num++;
						vector = vector2;
						num2 = num3;
						vector2 = vector;
						vector3 = vector;
						num3 = num2;
						num4 = num2;
						i = num2;
						continue;
					}
					vector3 = vector5;
					num4 = i;
				}
				if (TriArea2(vector, vector2, vector4) >= 0f)
				{
					if (PathUtils.AreVertsTheSame(vector, vector2) || TriArea2(vector, vector3, vector4) < 0f)
					{
						vector2 = vector4;
						num3 = i;
						continue;
					}
					pts[num] = vector3;
					num++;
					vector = vector3;
					num2 = num4;
					vector2 = vector;
					vector3 = vector;
					num3 = num2;
					num4 = num2;
					i = num2;
				}
			}
			if (num < maxPts)
			{
				pts[num] = portals[nportalPts - 1];
				num++;
			}
			return num;
		}
	}
}
