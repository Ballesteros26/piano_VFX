using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection
{
	// Token: 0x0200032D RID: 813
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class MonoCMethod : RuntimeConstructorInfo
	{
		// Token: 0x060023D1 RID: 9169 RVA: 0x00082DF3 File Offset: 0x00080FF3
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MonoMethodInfo.GetMethodImplementationFlags(this.mhandle);
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x00082E00 File Offset: 0x00081000
		public override ParameterInfo[] GetParameters()
		{
			return MonoMethodInfo.GetParametersInfo(this.mhandle, this);
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x00082E00 File Offset: 0x00081000
		internal override ParameterInfo[] GetParametersInternal()
		{
			return MonoMethodInfo.GetParametersInfo(this.mhandle, this);
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x00082E10 File Offset: 0x00081010
		internal override int GetParametersCount()
		{
			ParameterInfo[] parametersInfo = MonoMethodInfo.GetParametersInfo(this.mhandle, this);
			if (parametersInfo != null)
			{
				return parametersInfo.Length;
			}
			return 0;
		}

		// Token: 0x060023D5 RID: 9173
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object InternalInvoke(object obj, object[] parameters, out Exception exc);

		// Token: 0x060023D6 RID: 9174 RVA: 0x00082E32 File Offset: 0x00081032
		[DebuggerHidden]
		[DebuggerStepThrough]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			if (obj == null)
			{
				if (!base.IsStatic)
				{
					throw new TargetException("Instance constructor requires a target");
				}
			}
			else if (!this.DeclaringType.IsInstanceOfType(obj))
			{
				throw new TargetException("Constructor does not match target type");
			}
			return this.DoInvoke(obj, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x00082E70 File Offset: 0x00081070
		private object DoInvoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			ParameterInfo[] parametersInfo = MonoMethodInfo.GetParametersInfo(this.mhandle, this);
			MonoMethod.ConvertValues(binder, parameters, parametersInfo, culture, invokeAttr);
			if (obj == null && this.DeclaringType.ContainsGenericParameters)
			{
				throw new MemberAccessException("Cannot create an instance of " + this.DeclaringType + " because Type.ContainsGenericParameters is true.");
			}
			if ((invokeAttr & BindingFlags.CreateInstance) != BindingFlags.Default && this.DeclaringType.IsAbstract)
			{
				throw new MemberAccessException(string.Format("Cannot create an instance of {0} because it is an abstract class", this.DeclaringType));
			}
			return this.InternalInvoke(obj, parameters);
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x00082F00 File Offset: 0x00081100
		public object InternalInvoke(object obj, object[] parameters)
		{
			object obj2 = null;
			Exception ex;
			try
			{
				obj2 = this.InternalInvoke(obj, parameters, out ex);
			}
			catch (Exception ex2)
			{
				throw new TargetInvocationException(ex2);
			}
			if (ex != null)
			{
				throw ex;
			}
			if (obj != null)
			{
				return null;
			}
			return obj2;
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x00082F40 File Offset: 0x00081140
		[DebuggerHidden]
		[DebuggerStepThrough]
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this.DoInvoke(null, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060023DA RID: 9178 RVA: 0x00082F4E File Offset: 0x0008114E
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return new RuntimeMethodHandle(this.mhandle);
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060023DB RID: 9179 RVA: 0x00082F5B File Offset: 0x0008115B
		public override MethodAttributes Attributes
		{
			get
			{
				return MonoMethodInfo.GetAttributes(this.mhandle);
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060023DC RID: 9180 RVA: 0x00082F68 File Offset: 0x00081168
		public override CallingConventions CallingConvention
		{
			get
			{
				return MonoMethodInfo.GetCallingConvention(this.mhandle);
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060023DD RID: 9181 RVA: 0x00082F75 File Offset: 0x00081175
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.DeclaringType.ContainsGenericParameters;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060023DE RID: 9182 RVA: 0x00082F82 File Offset: 0x00081182
		public override Type ReflectedType
		{
			get
			{
				return this.reftype;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060023DF RID: 9183 RVA: 0x00082F8A File Offset: 0x0008118A
		public override Type DeclaringType
		{
			get
			{
				return MonoMethodInfo.GetDeclaringType(this.mhandle);
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060023E0 RID: 9184 RVA: 0x00082F97 File Offset: 0x00081197
		public override string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return MonoMethod.get_name(this);
			}
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x000330F9 File Offset: 0x000312F9
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x0007F7D9 File Offset: 0x0007D9D9
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x0007F7E2 File Offset: 0x0007D9E2
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x00082FAE File Offset: 0x000811AE
		public override MethodBody GetMethodBody()
		{
			return MethodBase.GetMethodBody(this.mhandle);
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x00082FBC File Offset: 0x000811BC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Void ");
			stringBuilder.Append(this.Name);
			stringBuilder.Append("(");
			ParameterInfo[] parameters = this.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(parameters[i].ParameterType.Name);
			}
			if (this.CallingConvention == CallingConventions.Any)
			{
				stringBuilder.Append(", ...");
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x000824A4 File Offset: 0x000806A4
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x060023E7 RID: 9191
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int get_core_clr_security_level();

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060023E8 RID: 9192 RVA: 0x00083054 File Offset: 0x00081254
		public override bool IsSecurityTransparent
		{
			get
			{
				return this.get_core_clr_security_level() == 0;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x0008305F File Offset: 0x0008125F
		public override bool IsSecurityCritical
		{
			get
			{
				return this.get_core_clr_security_level() > 0;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060023EA RID: 9194 RVA: 0x0008306A File Offset: 0x0008126A
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return this.get_core_clr_security_level() == 1;
			}
		}

		// Token: 0x0400134F RID: 4943
		internal IntPtr mhandle;

		// Token: 0x04001350 RID: 4944
		private string name;

		// Token: 0x04001351 RID: 4945
		private Type reftype;
	}
}
