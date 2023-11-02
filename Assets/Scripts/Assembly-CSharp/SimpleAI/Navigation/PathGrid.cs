using UnityEngine;

namespace SimpleAI.Navigation
{
	public class PathGrid : SolidityGrid, IPlanningWorld, IPathTerrain
	{
		public enum eNeighborDirection
		{
			kNoNeighbor = -1,
			kLeft = 0,
			kTop = 1,
			kRight = 2,
			kBottom = 3,
			kNumNeighbors = 4
		}

		private IHeightmap m_heightmap;

		public PathGrid()
		{
			m_heightmap = null;
		}

		public void Awake(Vector3 origin, int numRows, int numCols, float cellSize, bool show, IHeightmap heightmap)
		{
			Awake(origin, numRows, numCols, cellSize, show);
			m_heightmap = heightmap;
		}

		public bool HasHeightMap()
		{
			return m_heightmap != null;
		}

		public int GetNeighbors(int index, ref int[] neighbors)
		{
			neighbors = new int[4];
			for (int i = 0; i < 4; i++)
			{
				neighbors[i] = GetNeighbor(index, (eNeighborDirection)i);
			}
			return 4;
		}

		public int GetNumNodes()
		{
			return base.NumberOfCells;
		}

		public float GetGCost(int startIndex, int goalIndex)
		{
			Vector3 pathNodePos = GetPathNodePos(startIndex);
			Vector3 pathNodePos2 = GetPathNodePos(goalIndex);
			return Vector3.Distance(pathNodePos, pathNodePos2);
		}

		public float GetHCost(int startIndex, int goalIndex)
		{
			Vector3 pathNodePos = GetPathNodePos(startIndex);
			Vector3 pathNodePos2 = GetPathNodePos(goalIndex);
			float num = 2f;
			float num2 = num * Vector3.Distance(pathNodePos, pathNodePos2);
			return num2 + Mathf.Abs(pathNodePos2.y - pathNodePos.y) * 1f;
		}

		public bool IsNodeBlocked(int index)
		{
			return IsBlocked(index);
		}

		public Vector3 GetPathNodePos(int index)
		{
			Vector3 cellPosition = GetCellPosition(index);
			cellPosition += new Vector3(m_cellSize / 2f, 0f, m_cellSize / 2f);
			cellPosition.y = GetTerrainHeight(cellPosition);
			return cellPosition;
		}

		public int GetPathNodeIndex(Vector3 pos)
		{
			int num = GetCellIndex(pos);
			if (!IsInBounds(num))
			{
				num = -1;
			}
			return num;
		}

		public void ComputePortalsForPathSmoothing(Vector3[] roughPath, out Vector3[] aLeftPortalEndPts, out Vector3[] aRightPortalEndPts)
		{
			GridPortalComputer.ComputePortals(roughPath, this, out aLeftPortalEndPts, out aRightPortalEndPts);
		}

		public Vector3 GetValidPathFloorPos(Vector3 position)
		{
			Vector3 vector = position;
			float num = m_cellSize / 4f;
			Bounds gridBounds = GetGridBounds();
			position.x = Mathf.Clamp(position.x, gridBounds.min.x + num, gridBounds.max.x - num);
			position.y = base.Origin.y;
			position.z = Mathf.Clamp(position.z, gridBounds.min.z + num, gridBounds.max.z - num);
			int cellIndex = GetCellIndex(position);
			if (IsBlocked(cellIndex))
			{
				int[] neighbors = null;
				int neighbors2 = GetNeighbors(cellIndex, ref neighbors);
				float num2 = float.MaxValue;
				for (int i = 0; i < neighbors2; i++)
				{
					int index = neighbors[i];
					if (!IsBlocked(index))
					{
						Vector3 cellCenter = GetCellCenter(index);
						float sqrMagnitude = (cellCenter - vector).sqrMagnitude;
						if (sqrMagnitude < num2)
						{
							num2 = sqrMagnitude;
							position = cellCenter;
						}
					}
				}
			}
			return position;
		}

		public float GetTerrainHeight(Vector3 position)
		{
			if (m_heightmap == null)
			{
				return base.Origin.y;
			}
			return m_heightmap.SampleHeight(position);
		}

		public eNeighborDirection GetNeighborDirection(int index, int neighborIndex)
		{
			for (int i = 0; i < 4; i++)
			{
				int neighbor = GetNeighbor(index, (eNeighborDirection)i);
				if (neighbor == neighborIndex)
				{
					return (eNeighborDirection)i;
				}
			}
			return eNeighborDirection.kNoNeighbor;
		}

		private int GetNeighbor(int index, eNeighborDirection neighborDirection)
		{
			Vector3 cellCenter = GetCellCenter(index);
			switch (neighborDirection)
			{
			case eNeighborDirection.kLeft:
				cellCenter.x -= m_cellSize;
				break;
			case eNeighborDirection.kTop:
				cellCenter.z += m_cellSize;
				break;
			case eNeighborDirection.kRight:
				cellCenter.x += m_cellSize;
				break;
			case eNeighborDirection.kBottom:
				cellCenter.z -= m_cellSize;
				break;
			}
			int num = GetCellIndex(cellCenter);
			if (!IsInBounds(num))
			{
				num = -1;
			}
			return num;
		}

		 bool IPathTerrain.IsInBounds(Vector3 position)
		{
			return IsInBounds(position);
		}
	}
}
