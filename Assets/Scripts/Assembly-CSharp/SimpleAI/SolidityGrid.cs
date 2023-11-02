using System;
using UnityEngine;

namespace SimpleAI
{
	public class SolidityGrid : Grid
	{
		private bool[,] m_solidList;

		public override void Awake(Vector3 origin, int numRows, int numCols, float cellSize, bool show)
		{
			base.Awake(origin, numRows, numCols, cellSize, show);
			m_solidList = new bool[numCols, numRows];
			for (int i = 0; i < numCols; i++)
			{
				for (int j = 0; j < numRows; j++)
				{
					m_solidList[i, j] = false;
				}
			}
		}

		public void SetSolidity(bool[,] solidityList)
		{
			m_solidList = (bool[,])solidityList.Clone();
		}

		public void SetSolidity(int cellIndex, bool bSolid)
		{
			if (IsInBounds(cellIndex))
			{
				int column = GetColumn(cellIndex);
				int row = GetRow(cellIndex);
				m_solidList[column, row] = bSolid;
			}
		}

		public void SetSolidity(Vector3 cellPos, bool bSolid)
		{
			int cellIndex = GetCellIndex(cellPos);
			SetSolidity(cellIndex, bSolid);
		}

		public void Raycast2D(Ray ray, out Vector3 isectPt)
		{
			isectPt = new Vector3(0f, 0f, 0f);
			int cellIndex = GetCellIndex(ray.origin);
			if (cellIndex < 0 || cellIndex >= base.NumberOfCells)
			{
				return;
			}
			int num = GetColumn(cellIndex);
			int num2 = GetRow(cellIndex);
			int num3 = Math.Sign(ray.direction.x);
			int num4 = Math.Sign(ray.direction.y);
			Vector3 cellPosition = GetCellPosition(cellIndex);
			float num5 = ((num3 >= 0) ? (cellPosition.x + m_cellSize) : cellPosition.x);
			float num6 = ((num4 >= 0) ? cellPosition.z : (cellPosition.z - m_cellSize));
			float num7 = Vector3.Angle(Grid.kXAxis, ray.direction);
			float f = num7 * ((float)Math.PI / 180f);
			float num8 = Mathf.Cos(f);
			float num9 = Mathf.Sin(f);
			float num10 = Math.Abs((num5 - ray.origin.x) / num8);
			float num11 = Math.Abs((num6 - ray.origin.y) / num9);
			float num12 = Math.Abs(m_cellSize / num8);
			float num13 = Math.Abs(m_cellSize / num9);
			bool flag = false;
			bool flag2 = false;
			int num14 = num;
			int num15 = num2;
			int index = -1;
			while (!flag)
			{
				if (num10 < num11)
				{
					num14 = num;
					num10 += num12;
					num += num3;
				}
				else
				{
					num15 = num2;
					num11 += num13;
					num2 += num4;
				}
				if (!IsInBounds(num, num2))
				{
					index = GetCellIndex(new Vector3(num15, num14, 0f));
					flag = true;
					flag2 = true;
				}
				else if (m_solidList[num, num2])
				{
					index = GetCellIndex(new Vector3(num2, num, 0f));
					flag = true;
				}
			}
			bool flag3 = false;
			if (flag2)
			{
				Bounds gridBounds = GetGridBounds();
				float distance = 0f;
				if (gridBounds.IntersectRay(ray, out distance))
				{
					isectPt = ray.GetPoint(distance);
				}
			}
			else
			{
				Bounds cellBounds = GetCellBounds(index);
				float distance2 = 0f;
				if (cellBounds.IntersectRay(ray, out distance2))
				{
					isectPt = ray.GetPoint(distance2);
				}
			}
		}

		public bool IsBlocked(Vector3 pos)
		{
			int cellIndex = GetCellIndex(pos);
			if (!IsInBounds(cellIndex))
			{
				return true;
			}
			int column = GetColumn(cellIndex);
			int row = GetRow(cellIndex);
			return m_solidList[column, row];
		}

		public bool IsBlocked(int index)
		{
			int row = GetRow(index);
			int column = GetColumn(index);
			if (!IsInBounds(column, row))
			{
				return true;
			}
			return m_solidList[column, row];
		}
	}
}
