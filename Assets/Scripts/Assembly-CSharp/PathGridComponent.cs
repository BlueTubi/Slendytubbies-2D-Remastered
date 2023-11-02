using SimpleAI;
using SimpleAI.Navigation;
using UnityEngine;

public class PathGridComponent : PathTerrainComponent
{
	public int m_numberOfRows = 10;

	public int m_numberOfColumns = 10;

	public float m_cellSize = 1f;

	public bool m_debugShow = true;

	public Color m_debugColor = Color.white;

	public PathGrid PathGrid
	{
		get
		{
			return m_pathTerrain as PathGrid;
		}
	}

	private void Awake()
	{
		m_pathTerrain = new PathGrid();
		HeightmapComponent_UnityTerrain component = GetComponent<HeightmapComponent_UnityTerrain>();
		PathGrid.Awake(base.transform.position, m_numberOfRows, m_numberOfColumns, m_cellSize, m_debugShow, component);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = m_debugColor;
		if (m_debugShow)
		{
            SimpleAI.Grid.DebugDraw(base.transform.position, m_numberOfRows, m_numberOfColumns, m_cellSize, Gizmos.color);
		}
		Gizmos.DrawCube(base.transform.position, new Vector3(0.25f, 0.25f, 0.25f));
	}
}
