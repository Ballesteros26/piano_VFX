using System;

namespace System.Buffers
{
	// Token: 0x020009A6 RID: 2470
	public interface IRetainable
	{
		// Token: 0x06005A54 RID: 23124
		void Retain();

		// Token: 0x06005A55 RID: 23125
		bool Release();
	}
}
