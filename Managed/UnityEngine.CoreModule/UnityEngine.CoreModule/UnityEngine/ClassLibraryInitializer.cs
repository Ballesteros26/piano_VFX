using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001A1 RID: 417
	internal static class ClassLibraryInitializer
	{
		// Token: 0x0600130F RID: 4879 RVA: 0x0001F424 File Offset: 0x0001D624
		[RequiredByNativeCode]
		private static void Init()
		{
			UnityLogWriter.Init();
		}
	}
}
