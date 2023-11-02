using System;

namespace Boo.Lang.Environments
{
	public class InstantiatingEnvironment : IEnvironment
	{
		public TNeed Provide<TNeed>() where TNeed : class
		{
			return Activator.CreateInstance<TNeed>();
		}
	}
}
