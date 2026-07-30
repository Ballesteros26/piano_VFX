using System;
using UnityEngine.Scripting;

namespace Unity.Collections
{
	// Token: 0x02000055 RID: 85
	[RequiredByNativeCode]
	[AttributeUsage(256)]
	public sealed class NativeFixedLengthAttribute : Attribute
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00002D56 File Offset: 0x00000F56
		public NativeFixedLengthAttribute(int fixedLength)
		{
			this.FixedLength = fixedLength;
		}

		// Token: 0x04000103 RID: 259
		public int FixedLength;
	}
}
