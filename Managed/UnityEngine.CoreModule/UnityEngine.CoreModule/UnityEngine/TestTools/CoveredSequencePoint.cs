using System;
using System.Reflection;
using UnityEngine.Bindings;

namespace UnityEngine.TestTools
{
	// Token: 0x020003EC RID: 1004
	[NativeType(CodegenOptions.Custom, "ManagedCoveredSequencePoint", Header = "Runtime/Scripting/ScriptingCoverage.bindings.h")]
	public struct CoveredSequencePoint
	{
		// Token: 0x04000D10 RID: 3344
		public MethodBase method;

		// Token: 0x04000D11 RID: 3345
		public uint ilOffset;

		// Token: 0x04000D12 RID: 3346
		public uint hitCount;

		// Token: 0x04000D13 RID: 3347
		public string filename;

		// Token: 0x04000D14 RID: 3348
		public uint line;

		// Token: 0x04000D15 RID: 3349
		public uint column;
	}
}
