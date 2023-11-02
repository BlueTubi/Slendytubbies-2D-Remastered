using UnityEngine;

namespace SimpleAI.Navigation
{
	public interface IHeightmap
	{
		float SampleHeight(Vector3 position);
	}
}
