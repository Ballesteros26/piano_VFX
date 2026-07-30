using System;
using System.Dynamic.Utils;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002BC RID: 700
	internal sealed class AssemblyGen
	{
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x0003E314 File Offset: 0x0003C514
		private static AssemblyGen Assembly
		{
			get
			{
				if (AssemblyGen.s_assembly == null)
				{
					Interlocked.CompareExchange<AssemblyGen>(ref AssemblyGen.s_assembly, new AssemblyGen(), null);
				}
				return AssemblyGen.s_assembly;
			}
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0003E334 File Offset: 0x0003C534
		private AssemblyGen()
		{
			AssemblyName assemblyName = new AssemblyName("Snippets");
			CustomAttributeBuilder[] array = new CustomAttributeBuilder[]
			{
				new CustomAttributeBuilder(typeof(SecurityTransparentAttribute).GetConstructor(Type.EmptyTypes), Array.Empty<object>())
			};
			AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run, array);
			this._myModule = assemblyBuilder.DefineDynamicModule(assemblyName.Name);
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0003E398 File Offset: 0x0003C598
		private TypeBuilder DefineType(string name, Type parent, TypeAttributes attr)
		{
			ContractUtils.RequiresNotNull(name, "name");
			ContractUtils.RequiresNotNull(parent, "parent");
			StringBuilder stringBuilder = new StringBuilder(name);
			int num = Interlocked.Increment(ref this._index);
			stringBuilder.Append("$");
			stringBuilder.Append(num);
			stringBuilder.Replace('+', '_').Replace('[', '_').Replace(']', '_')
				.Replace('*', '_')
				.Replace('&', '_')
				.Replace(',', '_')
				.Replace('\\', '_');
			name = stringBuilder.ToString();
			return this._myModule.DefineType(name, attr, parent);
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0003E437 File Offset: 0x0003C637
		internal static TypeBuilder DefineDelegateType(string name)
		{
			return AssemblyGen.Assembly.DefineType(name, typeof(MulticastDelegate), TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.AutoClass);
		}

		// Token: 0x040009FA RID: 2554
		private static AssemblyGen s_assembly;

		// Token: 0x040009FB RID: 2555
		private readonly ModuleBuilder _myModule;

		// Token: 0x040009FC RID: 2556
		private int _index;
	}
}
