namespace SimpleAI.Planning
{
	public class ReachedGoalNode_SuccessCondition : SuccessCondition
	{
		private Node m_goalNode;

		public void Awake(Node goalNode)
		{
			m_goalNode = goalNode;
		}

		public override bool Evaluate(Node currentNode)
		{
			if (m_goalNode == currentNode)
			{
				return true;
			}
			return false;
		}
	}
}
