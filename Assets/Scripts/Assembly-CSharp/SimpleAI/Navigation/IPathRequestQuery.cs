using System.Collections.Generic;
using SimpleAI.Planning;
using UnityEngine;

namespace SimpleAI.Navigation
{
	public interface IPathRequestQuery
	{
		LinkedList<Node> GetSolutionPath();

		Vector3[] GetSolutionPath(IPathTerrain world);

		Vector3 GetStartPos();

		Vector3 GetGoalPos();

		IPathAgent GetPathAgent();

		bool HasCompleted();

		bool HasSuccessfullyCompleted();

		bool HasFailed();
	}
}
