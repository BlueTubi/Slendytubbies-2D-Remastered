using UnityEngine;

namespace SimpleAI.Navigation
{
	public class NavTargetPos : INavTarget
	{
		private Vector3 m_targetPos;

		private IPathTerrain m_pathTerrain;

		public NavTargetPos(Vector3 targetPos, IPathTerrain pathTerrain)
		{
			m_targetPos = targetPos;
			m_pathTerrain = pathTerrain;
		}

		public Vector3 GetNavTargetPosition()
		{
			return m_pathTerrain.GetValidPathFloorPos(m_targetPos);
		}
	}
}
