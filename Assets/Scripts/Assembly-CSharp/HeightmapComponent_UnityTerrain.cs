using SimpleAI.Navigation;
using UnityEngine;

public class HeightmapComponent_UnityTerrain : MonoBehaviour, IHeightmap
{
	public Terrain m_terrain;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public float SampleHeight(Vector3 position)
	{
		return m_terrain.SampleHeight(position);
	}
}
