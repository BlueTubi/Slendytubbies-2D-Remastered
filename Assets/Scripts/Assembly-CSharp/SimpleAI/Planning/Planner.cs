using SimpleAI.Navigation;

namespace SimpleAI.Planning
{
	public abstract class Planner
	{
		public enum ePlanStatus
		{
			kInvalid = -1,
			kPlanning = 0,
			kPlanSucceeded = 1,
			kPlanFailed = 2
		}

		protected ePlanStatus m_planStatus;

		protected int m_maxNumberOfNodes;

		private IPlanningWorld m_world;

		protected IPlanningWorld World
		{
			get
			{
				return m_world;
			}
		}

		public Planner()
		{
			m_planStatus = ePlanStatus.kInvalid;
			m_maxNumberOfNodes = 0;
		}

		public virtual void Awake(int maxNumberOfNodes)
		{
			m_maxNumberOfNodes = maxNumberOfNodes;
		}

		public virtual void Start(IPlanningWorld world)
		{
			m_world = world;
		}

		public virtual int Update(int numCyclesToConsume)
		{
			return 0;
		}

		public virtual void OnDrawGizmos()
		{
		}

		public bool HasPlanSucceeded()
		{
			return m_planStatus == ePlanStatus.kPlanSucceeded;
		}

		public bool HasPlanFailed()
		{
			return m_planStatus == ePlanStatus.kPlanFailed;
		}

		public bool HasPlanCompleted()
		{
			return m_planStatus == ePlanStatus.kPlanFailed || m_planStatus == ePlanStatus.kPlanSucceeded;
		}
	}
}
