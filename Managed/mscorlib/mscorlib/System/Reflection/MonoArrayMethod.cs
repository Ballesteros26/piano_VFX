using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000309 RID: 777
	[StructLayout(LayoutKind.Sequential)]
	internal class MonoArrayMethod : MethodInfo
	{
		// Token: 0x06002175 RID: 8565 RVA: 0x0007F773 File Offset: 0x0007D973
		internal MonoArrayMethod(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			this.name = methodName;
			this.parent = arrayClass;
			this.ret = returnType;
			this.parameters = (Type[])parameterTypes.Clone();
			this.call_conv = callingConvention;
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x00002119 File Offset: 0x00000319
		[MonoTODO("Always returns this")]
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06002177 RID: 8567 RVA: 0x0007F7AA File Offset: 0x0007D9AA
		public override Type ReturnType
		{
			get
			{
				return this.ret;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06002178 RID: 8568 RVA: 0x0000A42E File Offset: 0x0000862E
		[MonoTODO("Not implemented.  Always returns null")]
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("Not implemented.  Always returns zero")]
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MethodImplAttributes.IL;
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x0007F7B2 File Offset: 0x0007D9B2
		[MonoTODO("Not implemented.  Always returns an empty array")]
		public override ParameterInfo[] GetParameters()
		{
			return this.GetParametersInternal();
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x0007F7BA File Offset: 0x0007D9BA
		internal override ParameterInfo[] GetParametersInternal()
		{
			return EmptyArray<ParameterInfo>.Value;
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("Not implemented.  Always returns 0")]
		internal override int GetParametersCount()
		{
			return 0;
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO("Not implemented")]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600217E RID: 8574 RVA: 0x0007F7C1 File Offset: 0x0007D9C1
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.mhandle;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("Not implemented.  Always returns zero")]
		public override MethodAttributes Attributes
		{
			get
			{
				return MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x0007F7C9 File Offset: 0x0007D9C9
		public override Type ReflectedType
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06002181 RID: 8577 RVA: 0x0007F7C9 File Offset: 0x0007D9C9
		public override Type DeclaringType
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x0007F7D1 File Offset: 0x0007D9D1
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x000330F9 File Offset: 0x000312F9
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x0007F7D9 File Offset: 0x0007D9D9
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x0007F7E2 File Offset: 0x0007D9E2
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x0007F7EC File Offset: 0x0007D9EC
		public override string ToString()
		{
			string text = string.Empty;
			ParameterInfo[] array = this.GetParameters();
			for (int i = 0; i < array.Length; i++)
			{
				if (i > 0)
				{
					text += ", ";
				}
				text += array[i].ParameterType.Name;
			}
			if (this.ReturnType != null)
			{
				return string.Concat(new string[]
				{
					this.ReturnType.Name,
					" ",
					this.Name,
					"(",
					text,
					")"
				});
			}
			return string.Concat(new string[] { "void ", this.Name, "(", text, ")" });
		}

		// Token: 0x040012DE RID: 4830
		internal RuntimeMethodHandle mhandle;

		// Token: 0x040012DF RID: 4831
		internal Type parent;

		// Token: 0x040012E0 RID: 4832
		internal Type ret;

		// Token: 0x040012E1 RID: 4833
		internal Type[] parameters;

		// Token: 0x040012E2 RID: 4834
		internal string name;

		// Token: 0x040012E3 RID: 4835
		internal int table_idx;

		// Token: 0x040012E4 RID: 4836
		internal CallingConventions call_conv;
	}
}
