using System;
using System.Runtime.CompilerServices;

namespace Mono
{
	// Token: 0x02000018 RID: 24
	internal struct RuntimeGPtrArrayHandle
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00003E64 File Offset: 0x00002064
		internal unsafe RuntimeGPtrArrayHandle(RuntimeStructs.GPtrArray* value)
		{
			this.value = value;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003E6D File Offset: 0x0000206D
		internal unsafe RuntimeGPtrArrayHandle(IntPtr ptr)
		{
			this.value = (RuntimeStructs.GPtrArray*)(void*)ptr;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003E7B File Offset: 0x0000207B
		internal unsafe int Length
		{
			get
			{
				return this.value->len;
			}
		}

		// Token: 0x1700000C RID: 12
		internal IntPtr this[int i]
		{
			get
			{
				return this.Lookup(i);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003E91 File Offset: 0x00002091
		internal unsafe IntPtr Lookup(int i)
		{
			if (i >= 0 && i < this.Length)
			{
				return this.value->data[i];
			}
			throw new IndexOutOfRangeException();
		}

		// Token: 0x0600009A RID: 154
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void GPtrArrayFree(RuntimeStructs.GPtrArray* value);

		// Token: 0x0600009B RID: 155 RVA: 0x00003EBC File Offset: 0x000020BC
		internal static void DestroyAndFree(ref RuntimeGPtrArrayHandle h)
		{
			RuntimeGPtrArrayHandle.GPtrArrayFree(h.value);
			h.value = null;
		}

		// Token: 0x04000383 RID: 899
		private unsafe RuntimeStructs.GPtrArray* value;
	}
}
