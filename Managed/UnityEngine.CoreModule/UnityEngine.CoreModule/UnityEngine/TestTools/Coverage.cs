using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.TestTools
{
	// Token: 0x020003EE RID: 1006
	[NativeType("Runtime/Scripting/ScriptingCoverage.h")]
	[NativeClass("ScriptingCoverage")]
	public static class Coverage
	{
		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060022CE RID: 8910
		public static extern bool enabled
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060022CF RID: 8911
		[FreeFunction("ScriptingCoverageGetCoverageForMethodInfoObject", ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern CoveredSequencePoint[] GetSequencePointsFor_Internal(MethodBase method);

		// Token: 0x060022D0 RID: 8912
		[FreeFunction("ScriptingCoverageResetForMethodInfoObject", ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void ResetFor_Internal(MethodBase method);

		// Token: 0x060022D1 RID: 8913 RVA: 0x0003AA70 File Offset: 0x00038C70
		[FreeFunction("ScriptingCoverageGetStatsForMethodInfoObject", ThrowsException = true)]
		private static CoveredMethodStats GetStatsFor_Internal(MethodBase method)
		{
			CoveredMethodStats coveredMethodStats;
			Coverage.GetStatsFor_Internal_Injected(method, out coveredMethodStats);
			return coveredMethodStats;
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x0003AA88 File Offset: 0x00038C88
		public static CoveredSequencePoint[] GetSequencePointsFor(MethodBase method)
		{
			bool flag = method == null;
			if (flag)
			{
				throw new ArgumentNullException("method");
			}
			return Coverage.GetSequencePointsFor_Internal(method);
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x0003AAB4 File Offset: 0x00038CB4
		public static CoveredMethodStats GetStatsFor(MethodBase method)
		{
			bool flag = method == null;
			if (flag)
			{
				throw new ArgumentNullException("method");
			}
			return Coverage.GetStatsFor_Internal(method);
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x0003AAE0 File Offset: 0x00038CE0
		public static CoveredMethodStats[] GetStatsFor(MethodBase[] methods)
		{
			bool flag = methods == null;
			if (flag)
			{
				throw new ArgumentNullException("methods");
			}
			CoveredMethodStats[] array = new CoveredMethodStats[methods.Length];
			for (int i = 0; i < methods.Length; i++)
			{
				array[i] = Coverage.GetStatsFor(methods[i]);
			}
			return array;
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x0003AB34 File Offset: 0x00038D34
		public static CoveredMethodStats[] GetStatsFor(Type type)
		{
			bool flag = type == null;
			if (flag)
			{
				throw new ArgumentNullException("type");
			}
			return Coverage.GetStatsFor(Enumerable.ToArray<MethodBase>(Enumerable.OfType<MethodBase>(type.GetMembers(62))));
		}

		// Token: 0x060022D6 RID: 8918
		[FreeFunction("ScriptingCoverageGetStatsForAllCoveredMethodsFromScripting", ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern CoveredMethodStats[] GetStatsForAllCoveredMethods();

		// Token: 0x060022D7 RID: 8919 RVA: 0x0003AB70 File Offset: 0x00038D70
		public static void ResetFor(MethodBase method)
		{
			bool flag = method == null;
			if (flag)
			{
				throw new ArgumentNullException("method");
			}
			Coverage.ResetFor_Internal(method);
		}

		// Token: 0x060022D8 RID: 8920
		[FreeFunction("ScriptingCoverageResetAllFromScripting", ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern void ResetAll();

		// Token: 0x060022D9 RID: 8921
		[MethodImpl(4096)]
		private static extern void GetStatsFor_Internal_Injected(MethodBase method, out CoveredMethodStats ret);
	}
}
