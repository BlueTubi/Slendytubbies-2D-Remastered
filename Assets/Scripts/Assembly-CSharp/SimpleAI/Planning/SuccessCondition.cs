namespace SimpleAI.Planning
{
	public abstract class SuccessCondition
	{
		public abstract bool Evaluate(Node currentNode);
	}
}
