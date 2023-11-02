using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Boo.Lang.Runtime
{
	public class ExtensionRegistry
	{
		[CompilerGenerated]
		private sealed class _003CUnRegister_003Ec__AnonStorey14
		{
			internal Type type;

			internal bool _003C_003Em__8(MemberInfo member)
			{
				return member.DeclaringType == type;
			}
		}

		private List<MemberInfo> _extensions = new List<MemberInfo>();

		private object _classLock = new object();

		public IEnumerable<MemberInfo> Extensions
		{
			get
			{
				return _extensions;
			}
		}

		public void Register(Type type)
		{
			lock (_classLock)
			{
				_extensions = AddExtensionMembers(CopyExtensions(), type);
			}
		}

		public void UnRegister(Type type)
		{
			_003CUnRegister_003Ec__AnonStorey14 _003CUnRegister_003Ec__AnonStorey = new _003CUnRegister_003Ec__AnonStorey14();
			_003CUnRegister_003Ec__AnonStorey.type = type;
			lock (_classLock)
			{
				List<MemberInfo> list = CopyExtensions();
				list.RemoveAll(_003CUnRegister_003Ec__AnonStorey._003C_003Em__8);
				_extensions = list;
			}
		}

		private static List<MemberInfo> AddExtensionMembers(List<MemberInfo> extensions, Type type)
		{
			MemberInfo[] members = type.GetMembers(BindingFlags.Static | BindingFlags.Public);
			foreach (MemberInfo memberInfo in members)
			{
				if (Attribute.IsDefined(memberInfo, typeof(ExtensionAttribute)) && !extensions.Contains(memberInfo))
				{
					extensions.Add(memberInfo);
				}
			}
			return extensions;
		}

		private List<MemberInfo> CopyExtensions()
		{
			return new List<MemberInfo>(_extensions);
		}
	}
}
