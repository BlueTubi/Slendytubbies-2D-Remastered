using System;
using UnityEngine;

namespace SimpleAI
{
	public static class PathUtils
	{
		public static bool AreVertsTheSame(Vector3 v1, Vector3 v2)
		{
			return (v1 - v2).sqrMagnitude < 1.0000001E-06f;
		}

		public static float CalcClockwiseAngle(Vector3 dir1, Vector3 dir2)
		{
			dir1.y = 0f;
			dir2.y = 0f;
			dir1.Normalize();
			dir2.Normalize();
			Vector3 rhs = Vector3.Cross(dir1, Vector3.up);
			rhs.Normalize();
			float num = Vector3.Dot(dir2, rhs);
			float num2 = 0f;
			float f = Vector3.Dot(dir1, dir2);
			if (num > 0f)
			{
				return (float)Math.PI * 2f - Mathf.Acos(f);
			}
			return Mathf.Acos(f);
		}
	}
}
