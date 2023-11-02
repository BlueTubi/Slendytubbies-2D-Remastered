using UnityEngine;

namespace SimpleAI.Navigation
{
	public class PathPlanParams
	{
		private Vector3 m_startPos;

		private Vector3 m_goalPos;

		private INavTarget m_target;

		private float m_replanInterval;

		public Vector3 StartPos
		{
			get
			{
				return m_startPos;
			}
		}

		public Vector3 GoalPos
		{
			get
			{
				return m_goalPos;
			}
		}

		public float ReplanInterval
		{
			get
			{
				return m_replanInterval;
			}
		}

		public PathPlanParams(Vector3 startPos, INavTarget target, float replanInterval)
		{
			m_startPos = startPos;
			m_target = target;
			m_goalPos = target.GetNavTargetPosition();
			m_replanInterval = replanInterval;
		}

		public void UpdateStartAndGoalPos(Vector3 newStartPos)
		{
			m_startPos = newStartPos;
			m_goalPos = m_target.GetNavTargetPosition();
		}
	}
}
