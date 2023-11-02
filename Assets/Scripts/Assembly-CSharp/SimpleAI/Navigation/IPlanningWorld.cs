namespace SimpleAI.Navigation
{
	public interface IPlanningWorld
	{
		int GetNeighbors(int index, ref int[] neighbors);

		int GetNumNodes();

		float GetHCost(int startNodeIndex, int destNodeIndex);

		float GetGCost(int startNodeIndex, int destNodeIndex);

		bool IsNodeBlocked(int index);
	}
}
