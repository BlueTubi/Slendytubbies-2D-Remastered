using System.Collections.Generic;
using SimpleAI.Navigation;
using UnityEngine;

namespace SimpleAI.Planning
{
	public class AStarPlanner : Planner
	{
		protected BinaryHeap<Node> m_openNodes;

		protected Pool<Node> m_nodePool;

		protected Dictionary<int, Pool<Node>.Node> m_expandedNodes;

		protected Node m_startNode;

		protected Node m_goalNode;

		protected LinkedList<Node> m_solution;

		protected SuccessCondition m_successCondition;

		private ReachedGoalNode_SuccessCondition m_reachedGoalNodeSuccessCondition;

		public LinkedList<Node> Solution
		{
			get
			{
				return m_solution;
			}
		}

		public override void Awake(int maxNumberOfNodes)
		{
			base.Awake(maxNumberOfNodes);
			m_openNodes = new BinaryHeap<Node>();
			m_openNodes.Capacity = maxNumberOfNodes;
			m_nodePool = new Pool<Node>(maxNumberOfNodes);
			m_expandedNodes = new Dictionary<int, Pool<Node>.Node>(maxNumberOfNodes);
			m_solution = new LinkedList<Node>();
			m_reachedGoalNodeSuccessCondition = new ReachedGoalNode_SuccessCondition();
		}

		public override void Start(IPlanningWorld world)
		{
			base.Start(world);
		}

		public override int Update(int numCyclesToConsume)
		{
			if (m_planStatus != 0)
			{
				return 0;
			}
			int num = 0;
			while (num < numCyclesToConsume)
			{
				m_planStatus = RunSingleAStarCycle();
				num++;
				if (m_planStatus == ePlanStatus.kPlanFailed || m_planStatus == ePlanStatus.kPlanSucceeded)
				{
					break;
				}
			}
			return num;
		}

		public void StartANewPlan(int startNodeIndex, int goalNodeIndex)
		{
			if (startNodeIndex == -1 || goalNodeIndex == -1)
			{
				m_planStatus = ePlanStatus.kPlanFailed;
				return;
			}
			m_nodePool.Clear();
			m_openNodes.Clear();
			m_solution.Clear();
			m_expandedNodes.Clear();
			m_startNode = GetNode(startNodeIndex).Item;
			m_goalNode = GetNode(goalNodeIndex).Item;
			m_reachedGoalNodeSuccessCondition.Awake(m_goalNode);
			m_successCondition = m_reachedGoalNodeSuccessCondition;
			m_startNode.G = 0f;
			m_startNode.H = base.World.GetHCost(m_startNode.Index, m_goalNode.Index);
			m_startNode.F = m_startNode.G + m_startNode.H;
			m_startNode.Parent = null;
			OpenNode(m_startNode);
			m_planStatus = ePlanStatus.kPlanning;
		}

		protected ePlanStatus RunSingleAStarCycle()
		{
			if (m_openNodes.Count == 0)
			{
				return ePlanStatus.kPlanFailed;
			}
			Node node = m_openNodes.Remove();
			CloseNode(node);
			if (PlanSucceeded(node))
			{
				ConstructSolution();
				return ePlanStatus.kPlanSucceeded;
			}
			if (PlanFailed(node))
			{
				return ePlanStatus.kPlanFailed;
			}
			int[] neighbors = null;
			int neighbors2 = base.World.GetNeighbors(node.Index, ref neighbors);
			for (int i = 0; i < neighbors2; i++)
			{
				int num = neighbors[i];
				if (num == -1)
				{
					continue;
				}
				Pool<Node>.Node node2 = GetNode(num);
				if (m_expandedNodes.Count == m_maxNumberOfNodes)
				{
					Debug.LogWarning("Pathplan failed because it reached the max node count. Try increasing the Max Number Of Nodes Per Planner variable on the PathManager, through the Inspector window.");
					return ePlanStatus.kPlanFailed;
				}
				switch (node2.Item.State)
				{
				case Node.eState.kUnvisited:
					RecordParentNodeAndPathCosts(node2.Item, node);
					OpenNode(node2.Item);
					break;
				case Node.eState.kOpen:
				{
					float gCost = base.World.GetGCost(node.Index, node2.Item.Index);
					float num2 = node.G + gCost;
					if (num2 < node2.Item.G)
					{
						RecordParentNodeAndPathCosts(node2.Item, node);
						m_openNodes.Remove(node2.Item);
						m_openNodes.Add(node2.Item);
					}
					break;
				}
				}
			}
			return ePlanStatus.kPlanning;
		}

		protected void RecordParentNodeAndPathCosts(Node node, Node parentNode)
		{
			node.G = parentNode.G + base.World.GetGCost(parentNode.Index, node.Index);
			node.H = base.World.GetHCost(node.Index, m_goalNode.Index);
			node.F = node.G + node.H;
			node.Parent = parentNode;
		}

		protected Pool<Node>.Node GetNode(int nodeIndex)
		{
			Pool<Node>.Node value;
			if (!m_expandedNodes.TryGetValue(nodeIndex, out value))
			{
				return CreateNode(nodeIndex);
			}
			return value;
		}

		protected bool PlanFailed(Node currentNode)
		{
			if (currentNode == m_startNode)
			{
				return false;
			}
			if (m_openNodes.Count == m_maxNumberOfNodes)
			{
				Debug.LogWarning("Pathplan failed because it reached the max node count. Try increasing the Max Number Of Nodes Per Planner variable on the PathManager, through the Inspector window.");
				return true;
			}
			return false;
		}

		protected void ConstructSolution()
		{
			for (Node node = m_goalNode; node != m_startNode; node = node.Parent)
			{
				m_solution.AddFirst(node);
			}
			m_solution.AddFirst(m_startNode);
		}

		protected bool PlanSucceeded(Node currentNode)
		{
			return m_successCondition.Evaluate(currentNode);
		}

		protected Pool<Node>.Node CreateNode(int nodeIndex)
		{
			Pool<Node>.Node node = m_nodePool.Get();
			m_expandedNodes[nodeIndex] = node;
			Node.eState state = Node.eState.kUnvisited;
			if (base.World.IsNodeBlocked(nodeIndex))
			{
				state = Node.eState.kBlocked;
			}
			node.Item.Awake(nodeIndex, state);
			return node;
		}

		protected void OpenNode(Node node)
		{
			node.State = Node.eState.kOpen;
			m_openNodes.Add(node);
		}

		protected void CloseNode(Node node)
		{
			node.State = Node.eState.kClosed;
		}
	}
}
