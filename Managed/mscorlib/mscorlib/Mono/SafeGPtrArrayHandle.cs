using System;

namespace Mono
{
	// Token: 0x02000023 RID: 35
	internal struct SafeGPtrArrayHandle : IDisposable
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00004009 File Offset: 0x00002209
		internal SafeGPtrArrayHandle(IntPtr ptr)
		{
			this.handle = new RuntimeGPtrArrayHandle(ptr);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004017 File Offset: 0x00002217
		public void Dispose()
		{
			RuntimeGPtrArrayHandle.DestroyAndFree(ref this.handle);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00004024 File Offset: 0x00002224
		internal int Length
		{
			get
			{
				return this.handle.Length;
			}
		}

		// Token: 0x1700000E RID: 14
		internal IntPtr this[int i]
		{
			get
			{
				return this.handle[i];
			}
		}

		// Token: 0x040003B4 RID: 948
		private RuntimeGPtrArrayHandle handle;
	}
}
