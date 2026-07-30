using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200071F RID: 1823
	[Serializable]
	internal enum BinaryArrayTypeEnum
	{
		// Token: 0x0400280A RID: 10250
		Single,
		// Token: 0x0400280B RID: 10251
		Jagged,
		// Token: 0x0400280C RID: 10252
		Rectangular,
		// Token: 0x0400280D RID: 10253
		SingleOffset,
		// Token: 0x0400280E RID: 10254
		JaggedOffset,
		// Token: 0x0400280F RID: 10255
		RectangularOffset
	}
}
