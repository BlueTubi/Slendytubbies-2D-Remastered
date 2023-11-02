using UnityEngine;

namespace SimpleAI
{
	public class Grid
	{
		protected static readonly Vector3 kXAxis;

		protected static readonly Vector3 kZAxis;

		private static readonly float kDepth;

		protected int m_numberOfRows;

		protected int m_numberOfColumns;

		protected float m_cellSize;

		private Vector3 m_origin;

		public float Width
		{
			get
			{
				return (float)m_numberOfColumns * m_cellSize;
			}
		}

		public float Height
		{
			get
			{
				return (float)m_numberOfRows * m_cellSize;
			}
		}

		public Vector3 Origin
		{
			get
			{
				return m_origin;
			}
		}

		public int NumberOfCells
		{
			get
			{
				return m_numberOfRows * m_numberOfColumns;
			}
		}

		public float Left
		{
			get
			{
				return Origin.x;
			}
		}

		public float Right
		{
			get
			{
				return Origin.x + Width;
			}
		}

		public float Top
		{
			get
			{
				return Origin.z + Height;
			}
		}

		public float Bottom
		{
			get
			{
				return Origin.z;
			}
		}

		public float CellSize
		{
			get
			{
				return m_cellSize;
			}
		}

		static Grid()
		{
			kXAxis = new Vector3(1f, 0f, 0f);
			kZAxis = new Vector3(0f, 0f, 1f);
			kDepth = 1f;
		}

		public virtual void Awake(Vector3 origin, int numRows, int numCols, float cellSize, bool show)
		{
			m_origin = origin;
			m_numberOfRows = numRows;
			m_numberOfColumns = numCols;
			m_cellSize = cellSize;
		}

		public virtual void Update()
		{
		}

		public virtual void OnDrawGizmos()
		{
		}

		public static void DebugDraw(Vector3 origin, int numRows, int numCols, float cellSize, Color color)
		{
			float num = (float)numCols * cellSize;
			float num2 = (float)numRows * cellSize;
			for (int i = 0; i < numRows + 1; i++)
			{
				Vector3 vector = origin + (float)i * cellSize * kZAxis;
				Vector3 end = vector + num * kXAxis;
				Debug.DrawLine(vector, end, color);
			}
			for (int j = 0; j < numCols + 1; j++)
			{
				Vector3 vector2 = origin + (float)j * cellSize * kXAxis;
				Vector3 end2 = vector2 + num2 * kZAxis;
				Debug.DrawLine(vector2, end2, color);
			}
		}

		public Vector3 GetNearestCellCenter(Vector3 pos)
		{
			int cellIndex = GetCellIndex(pos);
			Vector3 cellPosition = GetCellPosition(cellIndex);
			cellPosition.x += m_cellSize / 2f;
			cellPosition.z += m_cellSize / 2f;
			return cellPosition;
		}

		public Vector3 GetCellCenter(int index)
		{
			Vector3 cellPosition = GetCellPosition(index);
			cellPosition.x += m_cellSize / 2f;
			cellPosition.z += m_cellSize / 2f;
			return cellPosition;
		}

		public Vector3 GetCellPosition(int index)
		{
			int row = GetRow(index);
			int column = GetColumn(index);
			float x = (float)column * m_cellSize;
			float z = (float)row * m_cellSize;
			return Origin + new Vector3(x, 0f, z);
		}

		public int GetCellIndex(Vector3 pos)
		{
			if (!IsInBounds(pos))
			{
				return -1;
			}
			pos -= Origin;
			int num = (int)(pos.x / m_cellSize);
			int num2 = (int)(pos.z / m_cellSize);
			return num2 * m_numberOfColumns + num;
		}

		public int GetCellIndexClamped(Vector3 pos)
		{
			pos -= Origin;
			int value = (int)(pos.x / m_cellSize);
			int value2 = (int)(pos.z / m_cellSize);
			value = Mathf.Clamp(value, 0, m_numberOfColumns - 1);
			value2 = Mathf.Clamp(value2, 0, m_numberOfRows - 1);
			return value2 * m_numberOfColumns + value;
		}

		public Bounds GetCellBounds(int index)
		{
			Vector3 cellPosition = GetCellPosition(index);
			cellPosition.x += m_cellSize / 2f;
			cellPosition.z += m_cellSize / 2f;
			return new Bounds(cellPosition, new Vector3(m_cellSize, kDepth, m_cellSize));
		}

		public Bounds GetGridBounds()
		{
			Vector3 center = Origin + Width / 2f * kXAxis + Height / 2f * kZAxis;
			return new Bounds(center, new Vector3(Width, kDepth, Height));
		}

		public int GetRow(int index)
		{
			return index / m_numberOfColumns;
		}

		public int GetColumn(int index)
		{
			return index % m_numberOfColumns;
		}

		public bool IsInBounds(int col, int row)
		{
			if (col < 0 || col >= m_numberOfColumns)
			{
				return false;
			}
			if (row < 0 || row >= m_numberOfRows)
			{
				return false;
			}
			return true;
		}

		public bool IsInBounds(int index)
		{
			return index >= 0 && index < NumberOfCells;
		}

		public bool IsInBounds(Vector3 pos)
		{
			return pos.x >= Left && pos.x <= Right && pos.z <= Top && pos.z >= Bottom;
		}
	}
}
