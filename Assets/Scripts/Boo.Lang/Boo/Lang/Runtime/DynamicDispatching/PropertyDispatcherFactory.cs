using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Boo.Lang.Runtime.DynamicDispatching.Emitters;

namespace Boo.Lang.Runtime.DynamicDispatching
{
	public class PropertyDispatcherFactory : AbstractDispatcherFactory
	{
		[CompilerGenerated]
		private sealed class _003CReflectionBasedFieldDispatcherFor_003Ec__AnonStorey10
		{
			internal FieldInfo field;

			internal object _003C_003Em__2(object target, object[] args)
			{
				return field.GetValue(target);
			}

			internal object _003C_003Em__3(object target, object[] args)
			{
				object obj = args[0];
				field.SetValue(target, RuntimeServices.Coerce(obj, field.FieldType));
				return obj;
			}
		}

		public PropertyDispatcherFactory(ExtensionRegistry extensions, object target, Type type, string name, params object[] arguments)
			: base(extensions, target, type, name, arguments)
		{
		}

		public Dispatcher CreateSetter()
		{
			return Create(SetOrGet.Set);
		}

		public Dispatcher CreateGetter()
		{
			return Create(SetOrGet.Get);
		}

		private Dispatcher Create(SetOrGet gos)
		{
			MemberInfo[] member = _type.GetMember(_name, MemberTypes.Field | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.OptionalParamBinding);
			if (member.Length == 0)
			{
				return FindExtension(GetCandidateExtensions(gos));
			}
			if (member.Length > 1)
			{
				throw new AmbiguousMatchException(Builtins.join(member, ", "));
			}
			return EmitDispatcherFor(member[0], gos);
		}

		private Dispatcher FindExtension(IEnumerable<MethodInfo> candidates)
		{
			CandidateMethod candidateMethod = ResolveExtension(candidates);
			if (candidateMethod != null)
			{
				return EmitExtensionDispatcher(candidateMethod);
			}
			throw MissingField();
		}

		private IEnumerable<MethodInfo> GetCandidateExtensions(SetOrGet gos)
		{
			foreach (PropertyInfo p in GetExtensions<PropertyInfo>(MemberTypes.Property))
			{
				MethodInfo i = Accessor(p, gos);
				if (i != null)
				{
					yield return i;
				}
			}
		}

		private static MethodInfo Accessor(PropertyInfo p, SetOrGet gos)
		{
			return (gos != SetOrGet.Get) ? p.GetSetMethod(true) : p.GetGetMethod(true);
		}

		private Dispatcher EmitDispatcherFor(MemberInfo info, SetOrGet gos)
		{
			MemberTypes memberType = info.MemberType;
			if (memberType == MemberTypes.Property)
			{
				return EmitPropertyDispatcher((PropertyInfo)info, gos);
			}
			return EmitFieldDispatcher((FieldInfo)info, gos);
		}

		private Dispatcher EmitFieldDispatcher(FieldInfo field, SetOrGet gos)
		{
			if (field.IsLiteral)
			{
				return ReflectionBasedFieldDispatcherFor(field, gos);
			}
			return (gos != SetOrGet.Get) ? new SetFieldEmitter(field, GetArgumentTypes()[0]).Emit() : new GetFieldEmitter(field).Emit();
		}

		private static Dispatcher ReflectionBasedFieldDispatcherFor(FieldInfo field, SetOrGet gos)
		{
			_003CReflectionBasedFieldDispatcherFor_003Ec__AnonStorey10 _003CReflectionBasedFieldDispatcherFor_003Ec__AnonStorey = new _003CReflectionBasedFieldDispatcherFor_003Ec__AnonStorey10();
			_003CReflectionBasedFieldDispatcherFor_003Ec__AnonStorey.field = field;
			switch (gos)
			{
			case SetOrGet.Get:
				return _003CReflectionBasedFieldDispatcherFor_003Ec__AnonStorey._003C_003Em__2;
			case SetOrGet.Set:
				return _003CReflectionBasedFieldDispatcherFor_003Ec__AnonStorey._003C_003Em__3;
			default:
				throw new ArgumentException();
			}
		}

		private Dispatcher EmitPropertyDispatcher(PropertyInfo property, SetOrGet gos)
		{
			Type[] argumentTypes = GetArgumentTypes();
			MethodInfo methodInfo = Accessor(property, gos);
			if (methodInfo == null)
			{
				throw MissingField();
			}
			CandidateMethod candidateMethod = AbstractDispatcherFactory.ResolveMethod(argumentTypes, new MethodInfo[1] { methodInfo });
			if (candidateMethod == null)
			{
				throw MissingField();
			}
			if (gos == SetOrGet.Get)
			{
				return new MethodDispatcherEmitter(_type, candidateMethod, argumentTypes).Emit();
			}
			return new SetPropertyEmitter(_type, candidateMethod, argumentTypes).Emit();
		}
	}
}
