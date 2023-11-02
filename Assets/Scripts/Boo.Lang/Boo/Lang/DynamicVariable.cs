using System;
using System.Runtime.CompilerServices;

namespace Boo.Lang
{
	public class DynamicVariable<T>
	{
		[CompilerGenerated]
		private sealed class _003CWith_003Ec__AnonStoreyE
		{
			internal Action<T> code;

			internal T value;

			internal void _003C_003Em__0()
			{
				code(value);
			}
		}

		private T _current;

		public T Value
		{
			get
			{
				return _current;
			}
		}

		public DynamicVariable()
		{
			_current = default(T);
		}

		public DynamicVariable(T initialValue)
		{
			_current = initialValue;
		}

		[Obsolete("Use With(T, System.Action) and access the variable value directly from the closure")]
		public void With(T value, Action<T> code)
		{
			_003CWith_003Ec__AnonStoreyE _003CWith_003Ec__AnonStoreyE = new _003CWith_003Ec__AnonStoreyE();
			_003CWith_003Ec__AnonStoreyE.code = code;
			_003CWith_003Ec__AnonStoreyE.value = value;
			With(_003CWith_003Ec__AnonStoreyE.value, _003CWith_003Ec__AnonStoreyE._003C_003Em__0);
		}

		public void With(T value, Procedure code)
		{
			T current = _current;
			_current = value;
			try
			{
				code();
			}
			finally
			{
				_current = current;
			}
		}
	}
}
