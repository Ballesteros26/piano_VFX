using System;
using System.Runtime.InteropServices;

namespace Mono.Mozilla
{
	// Token: 0x02000037 RID: 55
	internal class AsciiString : IDisposable
	{
		// Token: 0x0600018C RID: 396 RVA: 0x00002530 File Offset: 0x00000730
		public AsciiString(string value)
		{
			this.unmanagedContainer = new AsciiString.nsStringContainer();
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(AsciiString.nsStringContainer)));
			Marshal.StructureToPtr<AsciiString.nsStringContainer>(this.unmanagedContainer, intPtr, false);
			this.handle = new HandleRef(typeof(AsciiString.nsStringContainer), intPtr);
			Base.gluezilla_CStringContainerInit(this.handle);
			this.String = value;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000025A4 File Offset: 0x000007A4
		~AsciiString()
		{
			this.Dispose(false);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000025D4 File Offset: 0x000007D4
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					Base.gluezilla_CStringContainerFinish(this.handle);
					Marshal.FreeHGlobal(this.handle.Handle);
				}
				this.disposed = true;
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00002603 File Offset: 0x00000803
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00002612 File Offset: 0x00000812
		public HandleRef Handle
		{
			get
			{
				this.dirty = true;
				return this.handle;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00002624 File Offset: 0x00000824
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00002662 File Offset: 0x00000862
		public string String
		{
			get
			{
				if (this.dirty)
				{
					IntPtr intPtr;
					bool flag;
					Base.gluezilla_CStringGetData(this.handle, out intPtr, out flag);
					this.str = Marshal.PtrToStringAnsi(intPtr);
					this.dirty = false;
				}
				return this.str;
			}
			set
			{
				if (this.str != value)
				{
					this.str = value;
					Base.gluezilla_CStringSetData(this.handle, this.str, (uint)this.str.Length);
				}
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00002696 File Offset: 0x00000896
		public int Length
		{
			get
			{
				return this.String.Length;
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000026A3 File Offset: 0x000008A3
		public override string ToString()
		{
			return this.String;
		}

		// Token: 0x04000088 RID: 136
		private bool disposed;

		// Token: 0x04000089 RID: 137
		private AsciiString.nsStringContainer unmanagedContainer;

		// Token: 0x0400008A RID: 138
		private HandleRef handle;

		// Token: 0x0400008B RID: 139
		private string str = string.Empty;

		// Token: 0x0400008C RID: 140
		private bool dirty;

		// Token: 0x02000149 RID: 329
		[StructLayout(LayoutKind.Sequential)]
		private class nsStringContainer
		{
			// Token: 0x04000165 RID: 357
			private IntPtr v;

			// Token: 0x04000166 RID: 358
			private IntPtr d1;

			// Token: 0x04000167 RID: 359
			private uint d2;

			// Token: 0x04000168 RID: 360
			private IntPtr d3;
		}
	}
}
