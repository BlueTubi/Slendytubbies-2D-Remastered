using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Boo.Lang.Runtime.DynamicDispatching.Emitters;

namespace Boo.Lang.Runtime.DynamicDispatching
{
	internal class SliceDispatcherFactory : AbstractDispatcherFactory
	{
		[CompilerGenerated]
		private sealed class _003CCreateGetter_003Ec__AnonStorey11
		{
			internal FieldInfo field;

			internal object _003C_003Em__4(object o, object[] arguments)
			{
				return RuntimeServices.GetSlice(field.GetValue(o), string.Empty, arguments);
			}
		}

		[CompilerGenerated]
		private sealed class _003CCreateGetter_003Ec__AnonStorey12
		{
			internal MethodInfo getter;

			internal object _003C_003Em__5(object o, object[] arguments)
			{
				return RuntimeServices.GetSlice(getter.Invoke(o, null), string.Empty, arguments);
			}
		}

		[CompilerGenerated]
		private sealed class _003CCreateSetter_003Ec__AnonStorey13
		{
			internal FieldInfo field;

			internal object _003C_003Em__6(object o, object[] arguments)
			{
				return RuntimeServices.SetSlice(field.GetValue(o), string.Empty, arguments);
			}
		}

		public SliceDispatcherFactory(ExtensionRegistry extensions, object target, Type type, string name, params object[] arguments)
			: base(extensions, target, type, (name.Length != 0) ? name : RuntimeServices.GetDefaultMemberName(type), arguments)
		{
		}

		public Dispatcher CreateGetter()
		{
			MemberInfo[] array = ResolveMember();
			if (array.Length == 1)
			{
				return CreateGetter(array[0]);
			}
			return EmitMethodDispatcher(Getters(array));
		}

		private IEnumerable<MethodInfo> Getters(MemberInfo[] candidates)
		{
			foreach (MemberInfo info in candidates)
			{
				PropertyInfo p = info as PropertyInfo;
				if (p != null)
				{
					MethodInfo getter = p.GetGetMethod(true);
					if (getter != null)
					{
						yield return getter;
					}
				}
			}
		}

		private Dispatcher CreateGetter(MemberInfo member)
		{
			switch (member.MemberType)
			{
			case MemberTypes.Field:
			{
				_003CCreateGetter_003Ec__AnonStorey11 _003CCreateGetter_003Ec__AnonStorey2 = new _003CCreateGetter_003Ec__AnonStorey11();
				_003CCreateGetter_003Ec__AnonStorey2.field = (FieldInfo)member;
				return _003CCreateGetter_003Ec__AnonStorey2._003C_003Em__4;
			}
			case MemberTypes.Property:
			{
				_003CCreateGetter_003Ec__AnonStorey12 _003CCreateGetter_003Ec__AnonStorey = new _003CCreateGetter_003Ec__AnonStorey12();
				_003CCreateGetter_003Ec__AnonStorey.getter = ((PropertyInfo)member).GetGetMethod(true);
				if (_003CCreateGetter_003Ec__AnonStorey.getter == null)
				{
					throw MissingField();
				}
				if (_003CCreateGetter_003Ec__AnonStorey.getter.GetParameters().Length > 0)
				{
					return EmitMethodDispatcher(_003CCreateGetter_003Ec__AnonStorey.getter);
				}
				return _003CCreateGetter_003Ec__AnonStorey._003C_003Em__5;
			}
			default:
				throw MissingField();
			}
		}

		private Dispatcher EmitMethodDispatcher(MethodInfo candidate)
		{
			return EmitMethodDispatcher(new MethodInfo[1] { candidate });
		}

		private Dispatcher EmitMethodDispatcher(IEnumerable<MethodInfo> candidates)
		{
			CandidateMethod candidateMethod = AbstractDispatcherFactory.ResolveMethod(GetArgumentTypes(), candidates);
			if (candidateMethod == null)
			{
				throw MissingField();
			}
			return new MethodDispatcherEmitter(_type, candidateMethod, GetArgumentTypes()).Emit();
		}

		private MemberInfo[] ResolveMember()
		{
			MemberInfo[] member = _type.GetMember(_name, MemberTypes.Field | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.OptionalParamBinding);
			if (member.Length == 0)
			{
				throw MissingField();
			}
			return member;
		}

		public Dispatcher CreateSetter()
		{
			MemberInfo[] array = ResolveMember();
			if (array.Length == 1)
			{
				return CreateSetter(array[0]);
			}
			return EmitMethodDispatcher(Setters(array));
		}

		private IEnumerable<MethodInfo> Setters(MemberInfo[] candidates)
		{
			foreach (MemberInfo info in candidates)
			{
				PropertyInfo p = info as PropertyInfo;
				if (p != null)
				{
					MethodInfo setter = p.GetSetMethod(true);
					if (setter != null)
					{
						yield return setter;
					}
				}
			}
		}

		private Dispatcher CreateSetter(MemberInfo member)
		{
			switch (member.MemberType)
			{
			case MemberTypes.Field:
			{
				_003CCreateSetter_003Ec__AnonStorey13 _003CCreateSetter_003Ec__AnonStorey = new _003CCreateSetter_003Ec__AnonStorey13();
				_003CCreateSetter_003Ec__AnonStorey.field = (FieldInfo)member;
				return _003CCreateSetter_003Ec__AnonStorey._003C_003Em__6;
			}
			case MemberTypes.Property:
			{
				PropertyInfo propertyInfo = (PropertyInfo)member;
				if (propertyInfo.GetIndexParameters().Length > 0)
				{
					MethodInfo setMethod = propertyInfo.GetSetMethod(true);
					if (setMethod == null)
					{
						throw MissingField();
					}
					return EmitMethodDispatcher(setMethod);
				}
				return _003CCreateSetter_003Em__7;
			}
			default:
				throw MissingField();
			}
		}

		[CompilerGenerated]
		private object _003CCreateSetter_003Em__7(object o, object[] arguments)
		{
			return RuntimeServices.SetSlice(RuntimeServices.GetProperty(o, _name), string.Empty, arguments);
		}
	}
}
