using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
	// Token: 0x020001EA RID: 490
	internal struct UnSafeCharBuffer
	{
		// Token: 0x06001680 RID: 5760 RVA: 0x00059726 File Offset: 0x00057926
		[SecurityCritical]
		public unsafe UnSafeCharBuffer(char* buffer, int bufferSize)
		{
			this.m_buffer = buffer;
			this.m_totalSize = bufferSize;
			this.m_length = 0;
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00059740 File Offset: 0x00057940
		[SecuritySafeCritical]
		public unsafe void AppendString(string stringToAppend)
		{
			if (string.IsNullOrEmpty(stringToAppend))
			{
				return;
			}
			if (this.m_totalSize - this.m_length < stringToAppend.Length)
			{
				throw new IndexOutOfRangeException();
			}
			fixed (string text = stringToAppend)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				Buffer.Memcpy((byte*)(this.m_buffer + this.m_length), (byte*)ptr, stringToAppend.Length * 2);
			}
			this.m_length += stringToAppend.Length;
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x000597B4 File Offset: 0x000579B4
		public int Length
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x04000BD8 RID: 3032
		[SecurityCritical]
		private unsafe char* m_buffer;

		// Token: 0x04000BD9 RID: 3033
		private int m_totalSize;

		// Token: 0x04000BDA RID: 3034
		private int m_length;
	}
}
