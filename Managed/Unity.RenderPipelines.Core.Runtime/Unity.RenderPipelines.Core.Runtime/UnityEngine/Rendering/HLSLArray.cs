using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000044 RID: 68
	[AttributeUsage(AttributeTargets.Field)]
	public class HLSLArray : Attribute
	{
		// Token: 0x06000185 RID: 389 RVA: 0x00007A15 File Offset: 0x00005C15
		public HLSLArray(int arraySize, Type elementType)
		{
			this.arraySize = arraySize;
			this.elementType = elementType;
		}

		// Token: 0x04000122 RID: 290
		public int arraySize;

		// Token: 0x04000123 RID: 291
		public Type elementType;
	}
}
