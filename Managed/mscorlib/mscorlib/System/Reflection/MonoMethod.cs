using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x0200032B RID: 811
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class MonoMethod : RuntimeMethodInfo
	{
		// Token: 0x060023A0 RID: 9120 RVA: 0x000829B4 File Offset: 0x00080BB4
		internal MonoMethod()
		{
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000829BC File Offset: 0x00080BBC
		internal MonoMethod(RuntimeMethodHandle mhandle)
		{
			this.mhandle = mhandle.Value;
		}

		// Token: 0x060023A2 RID: 9122
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string get_name(MethodBase method);

		// Token: 0x060023A3 RID: 9123
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MonoMethod get_base_method(MonoMethod method, bool definition);

		// Token: 0x060023A4 RID: 9124 RVA: 0x000829D1 File Offset: 0x00080BD1
		public override MethodInfo GetBaseDefinition()
		{
			return MonoMethod.get_base_method(this, true);
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x000829DA File Offset: 0x00080BDA
		internal override MethodInfo GetBaseMethod()
		{
			return MonoMethod.get_base_method(this, false);
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x000829E3 File Offset: 0x00080BE3
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return MonoMethodInfo.GetReturnParameterInfo(this);
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060023A7 RID: 9127 RVA: 0x000829EB File Offset: 0x00080BEB
		public override Type ReturnType
		{
			get
			{
				return MonoMethodInfo.GetReturnType(this.mhandle);
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060023A8 RID: 9128 RVA: 0x000829E3 File Offset: 0x00080BE3
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return MonoMethodInfo.GetReturnParameterInfo(this);
			}
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000829F8 File Offset: 0x00080BF8
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MonoMethodInfo.GetMethodImplementationFlags(this.mhandle);
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x00082A08 File Offset: 0x00080C08
		public override ParameterInfo[] GetParameters()
		{
			ParameterInfo[] parametersInfo = MonoMethodInfo.GetParametersInfo(this.mhandle, this);
			if (parametersInfo.Length == 0)
			{
				return parametersInfo;
			}
			ParameterInfo[] array = new ParameterInfo[parametersInfo.Length];
			Array.FastCopy(parametersInfo, 0, array, 0, parametersInfo.Length);
			return array;
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x00082A3F File Offset: 0x00080C3F
		internal override ParameterInfo[] GetParametersInternal()
		{
			return MonoMethodInfo.GetParametersInfo(this.mhandle, this);
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x00082A4D File Offset: 0x00080C4D
		internal override int GetParametersCount()
		{
			return MonoMethodInfo.GetParametersInfo(this.mhandle, this).Length;
		}

		// Token: 0x060023AD RID: 9133
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object InternalInvoke(object obj, object[] parameters, out Exception exc);

		// Token: 0x060023AE RID: 9134 RVA: 0x00082A60 File Offset: 0x00080C60
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			ParameterInfo[] parametersInternal = this.GetParametersInternal();
			MonoMethod.ConvertValues(binder, parameters, parametersInternal, culture, invokeAttr);
			if (this.ContainsGenericParameters)
			{
				throw new InvalidOperationException("Late bound operations cannot be performed on types or methods for which ContainsGenericParameters is true.");
			}
			object obj2 = null;
			Exception ex;
			try
			{
				obj2 = this.InternalInvoke(obj, parameters, out ex);
			}
			catch (ThreadAbortException)
			{
				throw;
			}
			catch (Exception ex2)
			{
				throw new TargetInvocationException(ex2);
			}
			if (ex != null)
			{
				throw ex;
			}
			return obj2;
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x00082AD8 File Offset: 0x00080CD8
		internal static void ConvertValues(Binder binder, object[] args, ParameterInfo[] pinfo, CultureInfo culture, BindingFlags invokeAttr)
		{
			if (args == null)
			{
				if (pinfo.Length == 0)
				{
					return;
				}
				throw new TargetParameterCountException();
			}
			else
			{
				if (pinfo.Length != args.Length)
				{
					throw new TargetParameterCountException();
				}
				for (int i = 0; i < args.Length; i++)
				{
					object obj = args[i];
					ParameterInfo parameterInfo = pinfo[i];
					if (obj == Type.Missing)
					{
						if (parameterInfo.DefaultValue == DBNull.Value)
						{
							throw new ArgumentException(Environment.GetResourceString("Missing parameter does not have a default value."), "parameters");
						}
						args[i] = parameterInfo.DefaultValue;
					}
					else
					{
						RuntimeType runtimeType = (RuntimeType)parameterInfo.ParameterType;
						args[i] = runtimeType.CheckValue(obj, binder, culture, invokeAttr);
					}
				}
				return;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060023B0 RID: 9136 RVA: 0x00082B66 File Offset: 0x00080D66
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return new RuntimeMethodHandle(this.mhandle);
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060023B1 RID: 9137 RVA: 0x00082B73 File Offset: 0x00080D73
		public override MethodAttributes Attributes
		{
			get
			{
				return MonoMethodInfo.GetAttributes(this.mhandle);
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060023B2 RID: 9138 RVA: 0x00082B80 File Offset: 0x00080D80
		public override CallingConventions CallingConvention
		{
			get
			{
				return MonoMethodInfo.GetCallingConvention(this.mhandle);
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060023B3 RID: 9139 RVA: 0x00082B8D File Offset: 0x00080D8D
		public override Type ReflectedType
		{
			get
			{
				return this.reftype;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x00082B95 File Offset: 0x00080D95
		public override Type DeclaringType
		{
			get
			{
				return MonoMethodInfo.GetDeclaringType(this.mhandle);
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x00082BA2 File Offset: 0x00080DA2
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

		// Token: 0x060023B6 RID: 9142 RVA: 0x000330F9 File Offset: 0x000312F9
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x0007F7D9 File Offset: 0x0007D9D9
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x0007F7E2 File Offset: 0x0007D9E2
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x060023B9 RID: 9145
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void GetPInvoke(out PInvokeAttributes flags, out string entryPoint, out string dllName);

		// Token: 0x060023BA RID: 9146 RVA: 0x00082BBC File Offset: 0x00080DBC
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			MonoMethodInfo methodInfo = MonoMethodInfo.GetMethodInfo(this.mhandle);
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				num++;
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				array[num++] = new PreserveSigAttribute();
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				array[num++] = DllImportAttribute.GetCustomAttribute(this);
			}
			return array;
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x00082C40 File Offset: 0x00080E40
		public override MethodInfo MakeGenericMethod(params Type[] methodInstantiation)
		{
			if (methodInstantiation == null)
			{
				throw new ArgumentNullException("methodInstantiation");
			}
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException("not a generic method definition");
			}
			if (this.GetGenericArguments().Length != methodInstantiation.Length)
			{
				throw new ArgumentException("Incorrect length");
			}
			bool flag = false;
			foreach (Type type in methodInstantiation)
			{
				if (type == null)
				{
					throw new ArgumentNullException();
				}
				if (!(type is RuntimeType))
				{
					flag = true;
				}
			}
			if (flag)
			{
				return new MethodOnTypeBuilderInst(this, methodInstantiation);
			}
			MethodInfo methodInfo = this.MakeGenericMethod_impl(methodInstantiation);
			if (methodInfo == null)
			{
				throw new ArgumentException(string.Format("The method has {0} generic parameter(s) but {1} generic argument(s) were provided.", this.GetGenericArguments().Length, methodInstantiation.Length));
			}
			return methodInfo;
		}

		// Token: 0x060023BC RID: 9148
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MethodInfo MakeGenericMethod_impl(Type[] types);

		// Token: 0x060023BD RID: 9149
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern Type[] GetGenericArguments();

		// Token: 0x060023BE RID: 9150
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MethodInfo GetGenericMethodDefinition_impl();

		// Token: 0x060023BF RID: 9151 RVA: 0x00082CF3 File Offset: 0x00080EF3
		public override MethodInfo GetGenericMethodDefinition()
		{
			MethodInfo genericMethodDefinition_impl = this.GetGenericMethodDefinition_impl();
			if (genericMethodDefinition_impl == null)
			{
				throw new InvalidOperationException();
			}
			return genericMethodDefinition_impl;
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060023C0 RID: 9152
		public override extern bool IsGenericMethodDefinition
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060023C1 RID: 9153
		public override extern bool IsGenericMethod
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x00082D0C File Offset: 0x00080F0C
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.IsGenericMethod)
				{
					Type[] genericArguments = this.GetGenericArguments();
					for (int i = 0; i < genericArguments.Length; i++)
					{
						if (genericArguments[i].ContainsGenericParameters)
						{
							return true;
						}
					}
				}
				return this.DeclaringType.ContainsGenericParameters;
			}
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x00082D4D File Offset: 0x00080F4D
		public override MethodBody GetMethodBody()
		{
			return MethodBase.GetMethodBody(this.mhandle);
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x000824A4 File Offset: 0x000806A4
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x060023C5 RID: 9157
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int get_core_clr_security_level();

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x00082D5A File Offset: 0x00080F5A
		public override bool IsSecurityTransparent
		{
			get
			{
				return this.get_core_clr_security_level() == 0;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060023C7 RID: 9159 RVA: 0x00082D65 File Offset: 0x00080F65
		public override bool IsSecurityCritical
		{
			get
			{
				return this.get_core_clr_security_level() > 0;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060023C8 RID: 9160 RVA: 0x00082D70 File Offset: 0x00080F70
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return this.get_core_clr_security_level() == 1;
			}
		}

		// Token: 0x0400134C RID: 4940
		internal IntPtr mhandle;

		// Token: 0x0400134D RID: 4941
		private string name;

		// Token: 0x0400134E RID: 4942
		private Type reftype;
	}
}
