using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001B5 RID: 437
	internal class ScriptingUtility
	{
		// Token: 0x060013E6 RID: 5094 RVA: 0x0002059C File Offset: 0x0001E79C
		[RequiredByNativeCode]
		private static bool IsManagedCodeWorking()
		{
			ScriptingUtility.TestClass testClass = new ScriptingUtility.TestClass
			{
				value = 42
			};
			return testClass.value == 42;
		}

		// Token: 0x020001B6 RID: 438
		private struct TestClass
		{
			// Token: 0x04000658 RID: 1624
			public int value;
		}
	}
}
