namespace SimpleAI
{
	public static class FlagUtils
	{
		public static bool TestFlag(int flags, int flag)
		{
			return (flags & flag) != 0;
		}

		public static int SetFlag(int flags, int flag)
		{
			return flags |= flag;
		}

		public static int ClearFlag(int flags, int flag)
		{
			return flags & ~flag;
		}
	}
}
