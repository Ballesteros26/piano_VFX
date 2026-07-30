using System;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000063 RID: 99
	internal class UniString : IDisposable
	{
		// Token: 0x06000283 RID: 643 RVA: 0x00004150 File Offset: 0x00002350
		public UniString(string value)
		{
			this.unmanagedContainer = new UniString.nsStringContainer();
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(UniString.nsStringContainer)));
			Marshal.StructureToPtr<UniString.nsStringContainer>(this.unmanagedContainer, intPtr, false);
			this.handle = new HandleRef(typeof(UniString.nsStringContainer), intPtr);
			Base.gluezilla_StringContainerInit(this.handle);
			this.String = value;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000041C4 File Offset: 0x000023C4
		~UniString()
		{
			this.Dispose(false);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000041F4 File Offset: 0x000023F4
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					Base.gluezilla_StringContainerFinish(this.handle);
					Marshal.FreeHGlobal(this.handle.Handle);
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00004223 File Offset: 0x00002423
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00004232 File Offset: 0x00002432
		public HandleRef Handle
		{
			get
			{
				this.dirty = true;
				return this.handle;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00004244 File Offset: 0x00002444
		// (set) Token: 0x06000289 RID: 649 RVA: 0x00004282 File Offset: 0x00002482
		public string String
		{
			get
			{
				if (this.dirty)
				{
					IntPtr intPtr;
					bool flag;
					Base.gluezilla_StringGetData(this.handle, out intPtr, out flag);
					this.str = Marshal.PtrToStringUni(intPtr);
					this.dirty = false;
				}
				return this.str;
			}
			set
			{
				if (this.str != value)
				{
					this.str = value;
					Base.gluezilla_StringSetData(this.handle, this.str, (uint)this.str.Length);
				}
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600028A RID: 650 RVA: 0x000042B6 File Offset: 0x000024B6
		public int Length
		{
			get
			{
				return this.String.Length;
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000042C3 File Offset: 0x000024C3
		public override string ToString()
		{
			return this.String;
		}

		// Token: 0x040000D5 RID: 213
		private bool disposed;

		// Token: 0x040000D6 RID: 214
		private UniString.nsStringContainer unmanagedContainer;

		// Token: 0x040000D7 RID: 215
		private HandleRef handle;

		// Token: 0x040000D8 RID: 216
		private string str = string.Empty;

		// Token: 0x040000D9 RID: 217
		private bool dirty;

		// Token: 0x0200014B RID: 331
		[StructLayout(LayoutKind.Sequential)]
		private class nsStringContainer
		{
			// Token: 0x0400016B RID: 363
			private IntPtr v;

			// Token: 0x0400016C RID: 364
			private IntPtr d1;

			// Token: 0x0400016D RID: 365
			private uint d2;

			// Token: 0x0400016E RID: 366
			private IntPtr d3;
		}
	}
}
