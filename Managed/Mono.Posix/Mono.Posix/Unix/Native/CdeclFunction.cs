using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Unix.Native
{
	// Token: 0x02000024 RID: 36
	public sealed class CdeclFunction
	{
		// Token: 0x060001FA RID: 506 RVA: 0x00007708 File Offset: 0x00005908
		public CdeclFunction(string library, string method)
			: this(library, method, typeof(void))
		{
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000771C File Offset: 0x0000591C
		public CdeclFunction(string library, string method, Type returnType)
		{
			this.library = library;
			this.method = method;
			this.returnType = returnType;
			this.overloads = new Hashtable();
			this.assemblyName = new AssemblyName();
			this.assemblyName.Name = "Mono.Posix.Imports." + library;
			this.assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(this.assemblyName, AssemblyBuilderAccess.Run);
			this.moduleBuilder = this.assemblyBuilder.DefineDynamicModule(this.assemblyName.Name);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000077A4 File Offset: 0x000059A4
		public object Invoke(object[] parameters)
		{
			Type[] parameterTypes = CdeclFunction.GetParameterTypes(parameters);
			return this.CreateMethod(parameterTypes).Invoke(null, parameters);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000077C8 File Offset: 0x000059C8
		private MethodInfo CreateMethod(Type[] parameterTypes)
		{
			string typeName = this.GetTypeName(parameterTypes);
			Hashtable hashtable = this.overloads;
			MethodInfo methodInfo2;
			lock (hashtable)
			{
				MethodInfo methodInfo = (MethodInfo)this.overloads[typeName];
				if (methodInfo != null)
				{
					methodInfo2 = methodInfo;
				}
				else
				{
					TypeBuilder typeBuilder = this.CreateType(typeName);
					typeBuilder.DefinePInvokeMethod(this.method, this.library, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static | MethodAttributes.PinvokeImpl, CallingConventions.Standard, this.returnType, parameterTypes, CallingConvention.Cdecl, CharSet.Ansi);
					methodInfo = typeBuilder.CreateType().GetMethod(this.method);
					this.overloads.Add(typeName, methodInfo);
					methodInfo2 = methodInfo;
				}
			}
			return methodInfo2;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00007878 File Offset: 0x00005A78
		private TypeBuilder CreateType(string typeName)
		{
			return this.moduleBuilder.DefineType(typeName, TypeAttributes.Public);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00007888 File Offset: 0x00005A88
		private static Type GetMarshalType(Type t)
		{
			switch (Type.GetTypeCode(t))
			{
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.SByte:
			case TypeCode.Int16:
			case TypeCode.Int32:
				return typeof(int);
			case TypeCode.Byte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
				return typeof(uint);
			case TypeCode.Int64:
				return typeof(long);
			case TypeCode.UInt64:
				return typeof(ulong);
			case TypeCode.Single:
			case TypeCode.Double:
				return typeof(double);
			default:
				return t;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00007910 File Offset: 0x00005B10
		private string GetTypeName(Type[] parameterTypes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[").Append(this.library).Append("] ")
				.Append(this.method);
			stringBuilder.Append("(");
			if (parameterTypes.Length != 0)
			{
				stringBuilder.Append(parameterTypes[0]);
			}
			for (int i = 1; i < parameterTypes.Length; i++)
			{
				stringBuilder.Append(",").Append(parameterTypes[i]);
			}
			stringBuilder.Append(") : ").Append(this.returnType.FullName);
			return stringBuilder.ToString();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000079B0 File Offset: 0x00005BB0
		private static Type[] GetParameterTypes(object[] parameters)
		{
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = CdeclFunction.GetMarshalType(parameters[i].GetType());
			}
			return array;
		}

		// Token: 0x04000092 RID: 146
		private readonly string library;

		// Token: 0x04000093 RID: 147
		private readonly string method;

		// Token: 0x04000094 RID: 148
		private readonly Type returnType;

		// Token: 0x04000095 RID: 149
		private readonly AssemblyName assemblyName;

		// Token: 0x04000096 RID: 150
		private readonly AssemblyBuilder assemblyBuilder;

		// Token: 0x04000097 RID: 151
		private readonly ModuleBuilder moduleBuilder;

		// Token: 0x04000098 RID: 152
		private Hashtable overloads;
	}
}
