using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000327 RID: 807
	internal abstract class RtFieldInfo : RuntimeFieldInfo
	{
		// Token: 0x06002369 RID: 9065
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object UnsafeGetValue(object obj);

		// Token: 0x0600236A RID: 9066 RVA: 0x000824FC File Offset: 0x000806FC
		internal void CheckConsistency(object target)
		{
			if ((this.Attributes & FieldAttributes.Static) == FieldAttributes.Static || this.DeclaringType.IsInstanceOfType(target))
			{
				return;
			}
			if (target == null)
			{
				throw new TargetException(Environment.GetResourceString("Non-static field requires a target."));
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Field '{0}' defined on type '{1}' is not a field on the target object which is of type '{2}'."), this.Name, this.DeclaringType, target.GetType()));
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x00082564 File Offset: 0x00080764
		[DebuggerHidden]
		[DebuggerStepThrough]
		internal void UnsafeSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			bool flag = false;
			RuntimeFieldHandle.SetValue(this, obj, value, null, this.Attributes, null, ref flag);
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x00082585 File Offset: 0x00080785
		[DebuggerStepThrough]
		[DebuggerHidden]
		public unsafe override void SetValueDirect(TypedReference obj, object value)
		{
			if (obj.IsNull)
			{
				throw new ArgumentException(Environment.GetResourceString("The TypedReference must be initialized."));
			}
			RuntimeFieldHandle.SetValueDirect(this, (RuntimeType)this.FieldType, (void*)(&obj), value, (RuntimeType)this.DeclaringType);
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x000825C0 File Offset: 0x000807C0
		[DebuggerStepThrough]
		[DebuggerHidden]
		public unsafe override object GetValueDirect(TypedReference obj)
		{
			if (obj.IsNull)
			{
				throw new ArgumentException(Environment.GetResourceString("The TypedReference must be initialized."));
			}
			return RuntimeFieldHandle.GetValueDirect(this, (RuntimeType)this.FieldType, (void*)(&obj), (RuntimeType)this.DeclaringType);
		}
	}
}
