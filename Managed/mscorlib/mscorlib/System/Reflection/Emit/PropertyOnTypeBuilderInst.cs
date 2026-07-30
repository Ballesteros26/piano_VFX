using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000379 RID: 889
	[StructLayout(LayoutKind.Sequential)]
	internal class PropertyOnTypeBuilderInst : PropertyInfo
	{
		// Token: 0x0600288F RID: 10383 RVA: 0x00090A4A File Offset: 0x0008EC4A
		internal PropertyOnTypeBuilderInst(TypeBuilderInstantiation instantiation, PropertyInfo prop)
		{
			this.instantiation = instantiation;
			this.prop = prop;
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06002890 RID: 10384 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override PropertyAttributes Attributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06002891 RID: 10385 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override bool CanRead
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06002892 RID: 10386 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override bool CanWrite
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06002893 RID: 10387 RVA: 0x00090A60 File Offset: 0x0008EC60
		public override Type PropertyType
		{
			get
			{
				return this.instantiation.InflateType(this.prop.PropertyType);
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06002894 RID: 10388 RVA: 0x00090A78 File Offset: 0x0008EC78
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation.InflateType(this.prop.DeclaringType);
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06002895 RID: 10389 RVA: 0x00090A90 File Offset: 0x0008EC90
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06002896 RID: 10390 RVA: 0x00090A98 File Offset: 0x0008EC98
		public override string Name
		{
			get
			{
				return this.prop.Name;
			}
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x00090AA8 File Offset: 0x0008ECA8
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			MethodInfo getMethod = this.GetGetMethod(nonPublic);
			MethodInfo setMethod = this.GetSetMethod(nonPublic);
			int num = 0;
			if (getMethod != null)
			{
				num++;
			}
			if (setMethod != null)
			{
				num++;
			}
			MethodInfo[] array = new MethodInfo[num];
			num = 0;
			if (getMethod != null)
			{
				array[num++] = getMethod;
			}
			if (setMethod != null)
			{
				array[num] = setMethod;
			}
			return array;
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x00090B0C File Offset: 0x0008ED0C
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			MethodInfo methodInfo = this.prop.GetGetMethod(nonPublic);
			if (methodInfo != null && this.prop.DeclaringType == this.instantiation.generic_type)
			{
				methodInfo = TypeBuilder.GetMethod(this.instantiation, methodInfo);
			}
			return methodInfo;
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x00090B5C File Offset: 0x0008ED5C
		public override ParameterInfo[] GetIndexParameters()
		{
			MethodInfo getMethod = this.GetGetMethod(true);
			if (getMethod != null)
			{
				return getMethod.GetParameters();
			}
			return EmptyArray<ParameterInfo>.Value;
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x00090B88 File Offset: 0x0008ED88
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			MethodInfo methodInfo = this.prop.GetSetMethod(nonPublic);
			if (methodInfo != null && this.prop.DeclaringType == this.instantiation.generic_type)
			{
				methodInfo = TypeBuilder.GetMethod(this.instantiation, methodInfo);
			}
			return methodInfo;
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x00090BD6 File Offset: 0x0008EDD6
		public override string ToString()
		{
			return string.Format("{0} {1}", this.PropertyType, this.Name);
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040015CF RID: 5583
		private TypeBuilderInstantiation instantiation;

		// Token: 0x040015D0 RID: 5584
		private PropertyInfo prop;
	}
}
