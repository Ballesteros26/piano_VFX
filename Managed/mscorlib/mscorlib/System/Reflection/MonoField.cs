using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000328 RID: 808
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class MonoField : RtFieldInfo
	{
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x00082602 File Offset: 0x00080802
		public override FieldAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06002370 RID: 9072 RVA: 0x0008260A File Offset: 0x0008080A
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				return this.fhandle;
			}
		}

		// Token: 0x06002371 RID: 9073
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type ResolveType();

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06002372 RID: 9074 RVA: 0x00082612 File Offset: 0x00080812
		public override Type FieldType
		{
			get
			{
				if (this.type == null)
				{
					this.type = this.ResolveType();
				}
				return this.type;
			}
		}

		// Token: 0x06002373 RID: 9075
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type GetParentType(bool declaring);

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x00082634 File Offset: 0x00080834
		public override Type ReflectedType
		{
			get
			{
				return this.GetParentType(false);
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x0008263D File Offset: 0x0008083D
		public override Type DeclaringType
		{
			get
			{
				return this.GetParentType(true);
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06002376 RID: 9078 RVA: 0x00082646 File Offset: 0x00080846
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x000330F9 File Offset: 0x000312F9
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x0007F7D9 File Offset: 0x0007D9D9
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x0007F7E2 File Offset: 0x0007D9E2
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x0600237A RID: 9082
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal override extern int GetFieldOffset();

		// Token: 0x0600237B RID: 9083
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object GetValueInternal(object obj);

		// Token: 0x0600237C RID: 9084 RVA: 0x00082650 File Offset: 0x00080850
		public override object GetValue(object obj)
		{
			if (!base.IsStatic)
			{
				if (obj == null)
				{
					throw new TargetException("Non-static field requires a target");
				}
				if (!this.DeclaringType.IsAssignableFrom(obj.GetType()))
				{
					throw new ArgumentException(string.Format("Field {0} defined on type {1} is not a field on the target object which is of type {2}.", this.Name, this.DeclaringType, obj.GetType()), "obj");
				}
			}
			if (!base.IsLiteral)
			{
				this.CheckGeneric();
			}
			return this.GetValueInternal(obj);
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x000826C2 File Offset: 0x000808C2
		public override string ToString()
		{
			return string.Format("{0} {1}", this.FieldType, this.name);
		}

		// Token: 0x0600237E RID: 9086
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetValueInternal(FieldInfo fi, object obj, object value);

		// Token: 0x0600237F RID: 9087 RVA: 0x000826DC File Offset: 0x000808DC
		public override void SetValue(object obj, object val, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			if (!base.IsStatic)
			{
				if (obj == null)
				{
					throw new TargetException("Non-static field requires a target");
				}
				if (!this.DeclaringType.IsAssignableFrom(obj.GetType()))
				{
					throw new ArgumentException(string.Format("Field {0} defined on type {1} is not a field on the target object which is of type {2}.", this.Name, this.DeclaringType, obj.GetType()), "obj");
				}
			}
			if (base.IsLiteral)
			{
				throw new FieldAccessException("Cannot set a constant field");
			}
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			this.CheckGeneric();
			if (val != null)
			{
				val = ((RuntimeType)this.FieldType).CheckValue(val, binder, culture, invokeAttr);
			}
			MonoField.SetValueInternal(this, obj, val);
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x00082780 File Offset: 0x00080980
		internal MonoField Clone(string newName)
		{
			return new MonoField
			{
				name = newName,
				type = this.type,
				attrs = this.attrs,
				klass = this.klass,
				fhandle = this.fhandle
			};
		}

		// Token: 0x06002381 RID: 9089
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern object GetRawConstantValue();

		// Token: 0x06002382 RID: 9090 RVA: 0x000824A4 File Offset: 0x000806A4
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x000827BE File Offset: 0x000809BE
		private void CheckGeneric()
		{
			if (this.DeclaringType.ContainsGenericParameters)
			{
				throw new InvalidOperationException("Late bound operations cannot be performed on fields with types for which Type.ContainsGenericParameters is true.");
			}
		}

		// Token: 0x06002384 RID: 9092
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int get_core_clr_security_level();

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x000827D8 File Offset: 0x000809D8
		public override bool IsSecurityTransparent
		{
			get
			{
				return this.get_core_clr_security_level() == 0;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x000827E3 File Offset: 0x000809E3
		public override bool IsSecurityCritical
		{
			get
			{
				return this.get_core_clr_security_level() > 0;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x000827EE File Offset: 0x000809EE
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return this.get_core_clr_security_level() == 1;
			}
		}

		// Token: 0x04001342 RID: 4930
		internal IntPtr klass;

		// Token: 0x04001343 RID: 4931
		internal RuntimeFieldHandle fhandle;

		// Token: 0x04001344 RID: 4932
		private string name;

		// Token: 0x04001345 RID: 4933
		private Type type;

		// Token: 0x04001346 RID: 4934
		private FieldAttributes attrs;
	}
}
