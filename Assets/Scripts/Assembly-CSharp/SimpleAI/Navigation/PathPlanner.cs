using SimpleAI.Planning;

namespace SimpleAI.Navigation
{
	public class PathPlanner : AStarPlanner
	{
		private IPathTerrain m_pathTerrain;

		public IPathTerrain PathTerrain
		{
			get
			{
				return m_pathTerrain;
			}
		}

		public override void Start(IPlanningWorld world)
		{
			base.Start(world);
			m_pathTerrain = world as IPathTerrain;
		}
	}
}
