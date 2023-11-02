using UnityEngine;

namespace SimpleAI.Navigation
{
	public class NavTargetGameObject : INavTarget
	{
		private GameObject m_targetGameObject;

		private IPathTerrain m_pathTerrain;

		public NavTargetGameObject(GameObject targetGameObject, IPathTerrain pathTerrain)
		{
			m_targetGameObject = targetGameObject;
			m_pathTerrain = pathTerrain;
		}

		public Vector3 GetNavTargetPosition()
		{
			return m_pathTerrain.GetValidPathFloorPos(m_targetGameObject.transform.position);
		}
	}
}
