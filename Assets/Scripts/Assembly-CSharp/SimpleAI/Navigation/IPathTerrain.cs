using UnityEngine;

namespace SimpleAI.Navigation
{
	public interface IPathTerrain : IPlanningWorld
	{
		int GetPathNodeIndex(Vector3 pos);

		Vector3 GetPathNodePos(int index);

		void ComputePortalsForPathSmoothing(Vector3[] roughPath, out Vector3[] aPortalLeftEndPts, out Vector3[] aPortalRightEndPts);

		bool IsInBounds(Vector3 position);

		Vector3 GetValidPathFloorPos(Vector3 position);

		float GetTerrainHeight(Vector3 position);
	}
}
