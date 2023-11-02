using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Boo.Lang.Runtime.DynamicDispatching.Emitters
{
	internal class SetFieldEmitter : DispatcherEmitter
	{
		private readonly FieldInfo _field;

		private Type _argumentType;

		public SetFieldEmitter(FieldInfo field, Type argumentType)
			: base(field.DeclaringType, field.Name + "=")
		{
			_field = field;
			_argumentType = argumentType;
		}

		protected override void EmitMethodBody()
		{
			LocalBuilder value = DeclareLocal(_field.FieldType);
			EmitLoadValue();
			StoreLocal(value);
			if (_field.IsStatic)
			{
				LoadLocal(value);
				_il.Emit(OpCodes.Stsfld, _field);
			}
			else
			{
				EmitLoadTargetObject(_field.DeclaringType);
				LoadLocal(value);
				_il.Emit(OpCodes.Stfld, _field);
			}
			LoadLocal(value);
			EmitReturn(_field.FieldType);
		}

		private void EmitLoadValue()
		{
			EmitArgArrayElement(0);
			EmitCoercion(_argumentType, _field.FieldType, CandidateMethod.CalculateArgumentScore(_field.FieldType, _argumentType));
		}
	}
}
