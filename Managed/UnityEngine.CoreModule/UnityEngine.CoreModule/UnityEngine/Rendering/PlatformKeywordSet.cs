using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000388 RID: 904
	[UsedByNativeCode]
	public struct PlatformKeywordSet
	{
		// Token: 0x06001F9F RID: 8095 RVA: 0x00035FF8 File Offset: 0x000341F8
		private uint ComputeKeywordMask(BuiltinShaderDefine define)
		{
			return 1U << (int)(define % (BuiltinShaderDefine)32);
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x00036014 File Offset: 0x00034214
		public bool IsEnabled(BuiltinShaderDefine define)
		{
			return (this.m_Bits & this.ComputeKeywordMask(define)) > 0U;
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x00036037 File Offset: 0x00034237
		public void Enable(BuiltinShaderDefine define)
		{
			this.m_Bits |= this.ComputeKeywordMask(define);
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x0003604E File Offset: 0x0003424E
		public void Disable(BuiltinShaderDefine define)
		{
			this.m_Bits &= ~this.ComputeKeywordMask(define);
		}

		// Token: 0x04000B56 RID: 2902
		private const int k_SizeInBits = 32;

		// Token: 0x04000B57 RID: 2903
		internal uint m_Bits;
	}
}
