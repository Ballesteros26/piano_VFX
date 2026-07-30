using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.IO
{
	// Token: 0x020003A3 RID: 931
	internal sealed class PinnedBufferMemoryStream : UnmanagedMemoryStream
	{
		// Token: 0x06002B3C RID: 11068 RVA: 0x0009A62F File Offset: 0x0009882F
		[SecurityCritical]
		private PinnedBufferMemoryStream()
		{
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x0009A638 File Offset: 0x00098838
		[SecurityCritical]
		internal unsafe PinnedBufferMemoryStream(byte[] array)
		{
			int num = array.Length;
			if (num == 0)
			{
				array = new byte[1];
				num = 0;
			}
			this._array = array;
			this._pinningHandle = new GCHandle(array, GCHandleType.Pinned);
			byte[] array2;
			byte* ptr;
			if ((array2 = this._array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			base.Initialize(ptr, (long)num, (long)num, FileAccess.Read, true);
			array2 = null;
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x0009A69C File Offset: 0x0009889C
		~PinnedBufferMemoryStream()
		{
			this.Dispose(false);
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x0009A6CC File Offset: 0x000988CC
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			if (this._isOpen)
			{
				this._pinningHandle.Free();
				this._isOpen = false;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040016B1 RID: 5809
		private byte[] _array;

		// Token: 0x040016B2 RID: 5810
		private GCHandle _pinningHandle;
	}
}
