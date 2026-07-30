using System;

namespace System.Threading.Tasks
{
	// Token: 0x020004F4 RID: 1268
	internal class Shared<T>
	{
		// Token: 0x06003A0C RID: 14860 RVA: 0x000D263D File Offset: 0x000D083D
		internal Shared(T value)
		{
			this.Value = value;
		}

		// Token: 0x04001E6A RID: 7786
		internal T Value;
	}
}
