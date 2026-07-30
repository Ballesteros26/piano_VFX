using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x020003DB RID: 987
	internal class HGlobalUnmanagedMemoryStream : UnmanagedMemoryStream
	{
		// Token: 0x06002E73 RID: 11891 RVA: 0x000A5EB3 File Offset: 0x000A40B3
		public unsafe HGlobalUnmanagedMemoryStream(byte* pointer, long length, IntPtr ptr)
			: base(pointer, length, length, FileAccess.ReadWrite)
		{
			this.ptr = ptr;
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x000A5EC6 File Offset: 0x000A40C6
		protected override void Dispose(bool disposing)
		{
			if (this._isOpen)
			{
				Marshal.FreeHGlobal(this.ptr);
			}
			base.Dispose(disposing);
		}

		// Token: 0x04001812 RID: 6162
		private IntPtr ptr;
	}
}
