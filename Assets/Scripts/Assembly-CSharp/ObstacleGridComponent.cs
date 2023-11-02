using SimpleAI.Navigation;
using UnityEngine;

[RequireComponent(typeof(PathGridComponent))]
public class ObstacleGridComponent : MonoBehaviour
{
	public Color m_obstructedCellColor = Color.red;

	public bool m_show;

	public bool m_rasterizeEveryFrame = true;

	private PathGrid m_pathGrid;

	private void Start()
	{
		m_pathGrid = GetComponent<PathGridComponent>().PathGrid;
		Rasterize();
	}

	private void Update()
	{
		if (m_rasterizeEveryFrame)
		{
			Rasterize();
		}
	}

	public void Rasterize()
	{
		for (int i = 0; i < m_pathGrid.NumberOfCells; i++)
		{
			m_pathGrid.SetSolidity(i, false);
		}
		FootprintComponent[] array = (FootprintComponent[])Object.FindObjectsOfType(typeof(FootprintComponent));
		FootprintComponent[] array2 = array;
		foreach (FootprintComponent footprintComponent in array2)
		{
			int numObstructedCells = 0;
			int[] obstructedCells = footprintComponent.GetObstructedCells(m_pathGrid, out numObstructedCells);
			for (int k = 0; k < numObstructedCells; k++)
			{
				int num = obstructedCells[k];
				if (m_pathGrid.IsInBounds(num))
				{
					m_pathGrid.SetSolidity(num, true);
				}
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = m_obstructedCellColor;
		if (!m_show || m_pathGrid == null)
		{
			return;
		}
		for (int i = 0; i < m_pathGrid.NumberOfCells; i++)
		{
			if (m_pathGrid.IsBlocked(i))
			{
				Vector3 cellCenter = m_pathGrid.GetCellCenter(i);
				Vector3 size = new Vector3(m_pathGrid.CellSize, 0.5f, m_pathGrid.CellSize);
				Gizmos.DrawCube(cellCenter, size);
			}
		}
	}
}
