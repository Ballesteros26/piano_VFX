using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x0200036A RID: 874
	[StructLayout(LayoutKind.Sequential)]
	internal class MethodOnTypeBuilderInst : MethodInfo
	{
		// Token: 0x0600279F RID: 10143 RVA: 0x0008C7FD File Offset: 0x0008A9FD
		public MethodOnTypeBuilderInst(TypeBuilderInstantiation instantiation, MethodInfo base_method)
		{
			this.instantiation = instantiation;
			this.base_method = base_method;
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x0008C814 File Offset: 0x0008AA14
		internal MethodOnTypeBuilderInst(MethodOnTypeBuilderInst gmd, Type[] typeArguments)
		{
			this.instantiation = gmd.instantiation;
			this.base_method = gmd.base_method;
			this.method_arguments = new Type[typeArguments.Length];
			typeArguments.CopyTo(this.method_arguments, 0);
			this.generic_method_definition = gmd;
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x0008C864 File Offset: 0x0008AA64
		internal MethodOnTypeBuilderInst(MethodInfo method, Type[] typeArguments)
		{
			this.instantiation = method.DeclaringType;
			this.base_method = MethodOnTypeBuilderInst.ExtractBaseMethod(method);
			this.method_arguments = new Type[typeArguments.Length];
			typeArguments.CopyTo(this.method_arguments, 0);
			if (this.base_method != method)
			{
				this.generic_method_definition = method;
			}
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x0008C8C0 File Offset: 0x0008AAC0
		private static MethodInfo ExtractBaseMethod(MethodInfo info)
		{
			if (info is MethodBuilder)
			{
				return info;
			}
			if (info is MethodOnTypeBuilderInst)
			{
				return ((MethodOnTypeBuilderInst)info).base_method;
			}
			if (info.IsGenericMethod)
			{
				info = info.GetGenericMethodDefinition();
			}
			Type declaringType = info.DeclaringType;
			if (!declaringType.IsGenericType || declaringType.IsGenericTypeDefinition)
			{
				return info;
			}
			return (MethodInfo)declaringType.Module.ResolveMethod(info.MetadataToken);
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x0008C92A File Offset: 0x0008AB2A
		internal Type[] GetTypeArgs()
		{
			if (!this.instantiation.IsGenericType || this.instantiation.IsGenericParameter)
			{
				return null;
			}
			return this.instantiation.GetGenericArguments();
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x0008C954 File Offset: 0x0008AB54
		internal MethodInfo RuntimeResolve()
		{
			MethodInfo methodInfo = this.instantiation.InternalResolve().GetMethod(this.base_method);
			if (this.method_arguments != null)
			{
				Type[] array = new Type[this.method_arguments.Length];
				for (int i = 0; i < this.method_arguments.Length; i++)
				{
					array[i] = this.method_arguments[i].InternalResolve();
				}
				methodInfo = methodInfo.MakeGenericMethod(array);
			}
			return methodInfo;
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060027A5 RID: 10149 RVA: 0x0008C9BA File Offset: 0x0008ABBA
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060027A6 RID: 10150 RVA: 0x0008C9C2 File Offset: 0x0008ABC2
		public override string Name
		{
			get
			{
				return this.base_method.Name;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060027A7 RID: 10151 RVA: 0x0008C9BA File Offset: 0x0008ABBA
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060027A8 RID: 10152 RVA: 0x0008C9CF File Offset: 0x0008ABCF
		public override Type ReturnType
		{
			get
			{
				return this.base_method.ReturnType;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060027A9 RID: 10153 RVA: 0x0008C9DC File Offset: 0x0008ABDC
		public override Module Module
		{
			get
			{
				return this.base_method.Module;
			}
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x0008C9EC File Offset: 0x0008ABEC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.ReturnType.ToString());
			stringBuilder.Append(" ");
			stringBuilder.Append(this.base_method.Name);
			stringBuilder.Append("(");
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x0008CA44 File Offset: 0x0008AC44
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.base_method.GetMethodImplementationFlags();
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x0007F7B2 File Offset: 0x0007D9B2
		public override ParameterInfo[] GetParameters()
		{
			return this.GetParametersInternal();
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x00014B5A File Offset: 0x00012D5A
		internal override ParameterInfo[] GetParametersInternal()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060027B1 RID: 10161 RVA: 0x00086AA2 File Offset: 0x00084CA2
		public override int MetadataToken
		{
			get
			{
				return base.MetadataToken;
			}
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x0008CA51 File Offset: 0x0008AC51
		internal override int GetParametersCount()
		{
			return this.base_method.GetParametersCount();
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060027B4 RID: 10164 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x0008CA5E File Offset: 0x0008AC5E
		public override MethodAttributes Attributes
		{
			get
			{
				return this.base_method.Attributes;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060027B6 RID: 10166 RVA: 0x0008CA6B File Offset: 0x0008AC6B
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.base_method.CallingConvention;
			}
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x0008CA78 File Offset: 0x0008AC78
		public override MethodInfo MakeGenericMethod(params Type[] methodInstantiation)
		{
			if (!this.base_method.IsGenericMethodDefinition || this.method_arguments != null)
			{
				throw new InvalidOperationException("Method is not a generic method definition");
			}
			if (methodInstantiation == null)
			{
				throw new ArgumentNullException("methodInstantiation");
			}
			if (this.base_method.GetGenericArguments().Length != methodInstantiation.Length)
			{
				throw new ArgumentException("Incorrect length", "methodInstantiation");
			}
			for (int i = 0; i < methodInstantiation.Length; i++)
			{
				if (methodInstantiation[i] == null)
				{
					throw new ArgumentNullException("methodInstantiation");
				}
			}
			return new MethodOnTypeBuilderInst(this, methodInstantiation);
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x0008CB04 File Offset: 0x0008AD04
		public override Type[] GetGenericArguments()
		{
			if (!this.base_method.IsGenericMethodDefinition)
			{
				return null;
			}
			Type[] array = this.method_arguments ?? this.base_method.GetGenericArguments();
			Type[] array2 = new Type[array.Length];
			array.CopyTo(array2, 0);
			return array2;
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x0008CB46 File Offset: 0x0008AD46
		public override MethodInfo GetGenericMethodDefinition()
		{
			return this.generic_method_definition ?? this.base_method;
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060027BA RID: 10170 RVA: 0x0008CB58 File Offset: 0x0008AD58
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.base_method.ContainsGenericParameters)
				{
					return true;
				}
				if (!this.base_method.IsGenericMethodDefinition)
				{
					throw new NotSupportedException();
				}
				if (this.method_arguments == null)
				{
					return true;
				}
				Type[] array = this.method_arguments;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060027BB RID: 10171 RVA: 0x0008CBB3 File Offset: 0x0008ADB3
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.base_method.IsGenericMethodDefinition && this.method_arguments == null;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060027BC RID: 10172 RVA: 0x0008CBCD File Offset: 0x0008ADCD
		public override bool IsGenericMethod
		{
			get
			{
				return this.base_method.IsGenericMethodDefinition;
			}
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override MethodInfo GetBaseDefinition()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060027BE RID: 10174 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override ParameterInfo ReturnParameter
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060027BF RID: 10175 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0400147D RID: 5245
		private Type instantiation;

		// Token: 0x0400147E RID: 5246
		private MethodInfo base_method;

		// Token: 0x0400147F RID: 5247
		private Type[] method_arguments;

		// Token: 0x04001480 RID: 5248
		private MethodInfo generic_method_definition;
	}
}
