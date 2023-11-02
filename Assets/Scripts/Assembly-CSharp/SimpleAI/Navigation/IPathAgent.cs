using UnityEngine;

namespace SimpleAI.Navigation
{
	public interface IPathAgent
	{
		Vector3 GetPathAgentFootPos();

		void OnPathAgentRequestSucceeded(IPathRequestQuery request);

		void OnPathAgentRequestFailed();
	}
}
