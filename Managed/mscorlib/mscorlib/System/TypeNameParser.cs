using System;
using System.Reflection;
using System.Threading;

namespace System
{
	// Token: 0x020001FE RID: 510
	internal sealed class TypeNameParser
	{
		// Token: 0x060017C5 RID: 6085 RVA: 0x0005CE20 File Offset: 0x0005B020
		internal static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase, ref StackCrawlMark stackMark)
		{
			return TypeSpec.Parse(typeName).Resolve(assemblyResolver, typeResolver, throwOnError, ignoreCase);
		}
	}
}
