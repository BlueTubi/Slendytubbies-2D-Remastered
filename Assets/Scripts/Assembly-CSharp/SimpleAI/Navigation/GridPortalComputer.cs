using UnityEngine;

namespace SimpleAI.Navigation
{
	public static class GridPortalComputer
	{
		public static void ComputePortals(Vector3[] roughPath, PathGrid grid, out Vector3[] aLeftEndPts, out Vector3[] aRightEndPts)
		{
			aLeftEndPts = null;
			aRightEndPts = null;
			if (roughPath.Length >= 2)
			{
				aLeftEndPts = new Vector3[roughPath.Length - 1];
				aRightEndPts = new Vector3[roughPath.Length - 1];
				for (int i = 0; i < roughPath.Length - 1; i++)
				{
					Vector3 startPos = roughPath[i];
					Vector3 endPos = roughPath[i + 1];
					Vector3 leftPoint;
					Vector3 rightPoint;
					ComputePortal(startPos, endPos, grid, out leftPoint, out rightPoint);
					aLeftEndPts[i] = leftPoint;
					aRightEndPts[i] = rightPoint;
				}
			}
		}

		private static void ComputePortal(Vector3 startPos, Vector3 endPos, PathGrid grid, out Vector3 leftPoint, out Vector3 rightPoint)
		{
			leftPoint = Vector3.zero;
			rightPoint = Vector3.zero;
			int cellIndex = grid.GetCellIndex(startPos);
			int cellIndex2 = grid.GetCellIndex(endPos);
			Bounds cellBounds = grid.GetCellBounds(cellIndex);
			switch (grid.GetNeighborDirection(cellIndex, cellIndex2))
			{
			case PathGrid.eNeighborDirection.kTop:
				leftPoint = new Vector3(cellBounds.min.x, grid.Origin.y, cellBounds.max.z);
				rightPoint = new Vector3(cellBounds.max.x, grid.Origin.y, cellBounds.max.z);
				break;
			case PathGrid.eNeighborDirection.kRight:
				leftPoint = new Vector3(cellBounds.max.x, grid.Origin.y, cellBounds.max.z);
				rightPoint = new Vector3(cellBounds.max.x, grid.Origin.y, cellBounds.min.z);
				break;
			case PathGrid.eNeighborDirection.kBottom:
				leftPoint = new Vector3(cellBounds.max.x, grid.Origin.y, cellBounds.min.z);
				rightPoint = new Vector3(cellBounds.min.x, grid.Origin.y, cellBounds.min.z);
				break;
			case PathGrid.eNeighborDirection.kLeft:
				leftPoint = new Vector3(cellBounds.min.x, grid.Origin.y, cellBounds.min.z);
				rightPoint = new Vector3(cellBounds.min.x, grid.Origin.y, cellBounds.max.z);
				break;
			default:
				Debug.LogError("ComputePortal failed to find a neighbor");
				break;
			}
		}
	}
}
