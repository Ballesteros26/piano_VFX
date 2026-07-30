using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Web
{
	// Token: 0x020000DD RID: 221
	internal class TempFileStream : FileStream
	{
		// Token: 0x06000BE9 RID: 3049 RVA: 0x0001FCBF File Offset: 0x0001DEBF
		public TempFileStream(string name)
			: base(name, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 8192)
		{
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0001FCD0 File Offset: 0x0001DED0
		public override bool CanRead
		{
			get
			{
				return this.read_mode;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0001FCD8 File Offset: 0x0001DED8
		public override bool CanWrite
		{
			get
			{
				return !this.read_mode;
			}
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0001FCE3 File Offset: 0x0001DEE3
		public void SavePosition()
		{
			this.saved_position = this.Position;
			this.Position = 0L;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0001FCF9 File Offset: 0x0001DEF9
		public void RestorePosition()
		{
			this.Position = this.saved_position;
			this.saved_position = -1L;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0001FD0F File Offset: 0x0001DF0F
		public void SetReadOnly()
		{
			this.read_mode = true;
			this.Position = 0L;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0001FD20 File Offset: 0x0001DF20
		public void SetWriteOnly()
		{
			this.read_mode = false;
			this.Position = 0L;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0001FD31 File Offset: 0x0001DF31
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.read_mode)
			{
				throw new InvalidOperationException("mode read");
			}
			base.Write(buffer, offset, count);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0001FD4F File Offset: 0x0001DF4F
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			if (!this.read_mode)
			{
				throw new InvalidOperationException("mode write");
			}
			return base.Read(buffer, offset, count);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0001FD70 File Offset: 0x0001DF70
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.disposed = true;
				base.Dispose(disposing);
				try
				{
					File.Delete(base.Name);
				}
				catch
				{
				}
			}
		}

		// Token: 0x040010BA RID: 4282
		private bool read_mode;

		// Token: 0x040010BB RID: 4283
		private bool disposed;

		// Token: 0x040010BC RID: 4284
		private long saved_position;
	}
}
