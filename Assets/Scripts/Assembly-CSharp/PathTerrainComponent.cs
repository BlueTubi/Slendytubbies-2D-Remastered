using SimpleAI.Navigation;
using UnityEngine;

public abstract class PathTerrainComponent : MonoBehaviour
{
	protected IPathTerrain m_pathTerrain;

	public IPathTerrain PathTerrain
	{
		get
		{
			return m_pathTerrain;
		}
	}
}
