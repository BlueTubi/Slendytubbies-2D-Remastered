using SimpleAI;
using UnityEngine;

public class FootprintComponent : MonoBehaviour
{
	public bool m_showBoundingBox = true;

	public float m_scale;

	private int[] m_obstructedCellPool;

	private int m_numObstructedCellPoolRows;

	private int m_numObstructedCellPoolColumns;

	private void Awake()
	{
		m_numObstructedCellPoolRows = 0;
		m_numObstructedCellPoolColumns = 0;
		m_obstructedCellPool = null;
	}

	public int[] GetObstructedCells(SimpleAI.Grid grid, out int numObstructedCells)
	{
		numObstructedCells = 0;
		if (GetComponent<Collider>() == null)
		{
			return null;
		}
		Bounds bounds = GetComponent<Collider>().bounds;
		bounds.Expand(m_scale);
		Vector3 vector = new Vector3(bounds.min.x, grid.Origin.y, bounds.max.z);
		Vector3 vector2 = new Vector3(bounds.max.x, grid.Origin.y, bounds.max.z);
		Vector3 vector3 = new Vector3(bounds.min.x, grid.Origin.y, bounds.min.z);
		Vector3 vector4 = new Vector3(bounds.max.x, grid.Origin.y, bounds.min.z);
		Vector3 normalized = (vector2 - vector).normalized;
		Vector3 normalized2 = (vector - vector3).normalized;
		float x = bounds.size.x;
		float z = bounds.size.z;
		if (m_showBoundingBox)
		{
			Debug.DrawLine(vector, vector2);
			Debug.DrawLine(vector2, vector4);
			Debug.DrawLine(vector4, vector3);
			Debug.DrawLine(vector3, vector);
		}
		UpdateObstructedCellPool(grid);
		for (int i = 0; i < m_numObstructedCellPoolRows; i++)
		{
			float num = (float)i * grid.CellSize;
			for (int j = 0; j < m_numObstructedCellPoolColumns; j++)
			{
				float num2 = (float)j * grid.CellSize;
				Vector3 pos = vector3 + normalized * num2 + normalized2 * num;
				pos.x = Mathf.Clamp(pos.x, bounds.min.x, bounds.max.x);
				pos.z = Mathf.Clamp(pos.z, bounds.min.z, bounds.max.z);
				if (grid.IsInBounds(pos))
				{
					int cellIndex = grid.GetCellIndex(pos);
					m_obstructedCellPool[numObstructedCells] = cellIndex;
					numObstructedCells++;
				}
				if (num2 > x)
				{
					break;
				}
			}
			if (num > z)
			{
				break;
			}
		}
		return m_obstructedCellPool;
	}

	private void UpdateObstructedCellPool(SimpleAI.Grid grid)
	{
		Bounds bounds = GetComponent<Collider>().bounds;
		bounds.Expand(m_scale);
		int num = (int)(bounds.size.z / grid.CellSize) + 2;
		int num2 = (int)(bounds.size.x / grid.CellSize) + 2;
		int num3 = num * num2;
		if (m_obstructedCellPool == null || num3 != m_obstructedCellPool.Length)
		{
			m_obstructedCellPool = new int[num3];
			m_numObstructedCellPoolRows = num;
			m_numObstructedCellPoolColumns = num2;
		}
		for (int i = 0; i < m_obstructedCellPool.Length; i++)
		{
			m_obstructedCellPool[i] = -1;
		}
	}
}
