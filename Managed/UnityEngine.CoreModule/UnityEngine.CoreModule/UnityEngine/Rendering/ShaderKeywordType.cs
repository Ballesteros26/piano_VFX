using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000389 RID: 905
	[UsedByNativeCode]
	[NativeHeader("Runtime/Shaders/ShaderKeywordSet.h")]
	public enum ShaderKeywordType
	{
		// Token: 0x04000B59 RID: 2905
		None,
		// Token: 0x04000B5A RID: 2906
		BuiltinDefault = 2,
		// Token: 0x04000B5B RID: 2907
		BuiltinExtra = 6,
		// Token: 0x04000B5C RID: 2908
		BuiltinAutoStripped = 10,
		// Token: 0x04000B5D RID: 2909
		UserDefined = 16
	}
}
