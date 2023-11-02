using System.Runtime.CompilerServices;

namespace Boo.Lang.Environments
{
	public static class EnvironmentBoundValue
	{
		public static EnvironmentBoundValue<T> Capture<T>() where T : class
		{
			return Return(My<T>.Instance);
		}

		public static EnvironmentBoundValue<T> Return<T>(T value)
		{
			return Create(ActiveEnvironment.Instance, value);
		}

		public static EnvironmentBoundValue<T> Create<T>(IEnvironment environment, T value)
		{
			return new EnvironmentBoundValue<T>(environment, value);
		}
	}
	public struct EnvironmentBoundValue<T>
	{
		[CompilerGenerated]
		private sealed class _003CSelect_003Ec__AnonStoreyF<TResult>
		{
			internal Function<T, TResult> selector;

			internal T v;

			internal EnvironmentBoundValue<TResult> r;

			internal void _003C_003Em__1()
			{
				r = EnvironmentBoundValue.Return(selector(v));
			}
		}

		public readonly T Value;

		public readonly IEnvironment Environment;

		public EnvironmentBoundValue(IEnvironment environment, T value)
		{
			Environment = environment;
			Value = value;
		}

		public EnvironmentBoundValue<TResult> Select<TResult>(Function<T, TResult> selector)
		{
			_003CSelect_003Ec__AnonStoreyF<TResult> _003CSelect_003Ec__AnonStoreyF = new _003CSelect_003Ec__AnonStoreyF<TResult>();
			_003CSelect_003Ec__AnonStoreyF.selector = selector;
			_003CSelect_003Ec__AnonStoreyF.v = Value;
			_003CSelect_003Ec__AnonStoreyF.r = default(EnvironmentBoundValue<TResult>);
			ActiveEnvironment.With(Environment, _003CSelect_003Ec__AnonStoreyF._003C_003Em__1);
			return _003CSelect_003Ec__AnonStoreyF.r;
		}
	}
}
