using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200071E RID: 1822
	[Serializable]
	internal enum BinaryTypeEnum
	{
		// Token: 0x04002801 RID: 10241
		Primitive,
		// Token: 0x04002802 RID: 10242
		String,
		// Token: 0x04002803 RID: 10243
		Object,
		// Token: 0x04002804 RID: 10244
		ObjectUrt,
		// Token: 0x04002805 RID: 10245
		ObjectUser,
		// Token: 0x04002806 RID: 10246
		ObjectArray,
		// Token: 0x04002807 RID: 10247
		StringArray,
		// Token: 0x04002808 RID: 10248
		PrimitiveArray
	}
}
