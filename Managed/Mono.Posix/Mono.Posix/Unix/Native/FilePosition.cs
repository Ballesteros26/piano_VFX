using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Unix.Native
{
	// Token: 0x02000029 RID: 41
	public sealed class FilePosition : MarshalByRefObject, IDisposable, IEquatable<FilePosition>
	{
		// Token: 0x06000357 RID: 855 RVA: 0x00009490 File Offset: 0x00007690
		public FilePosition()
		{
			IntPtr intPtr = Stdlib.CreateFilePosition();
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException("Unable to malloc fpos_t!");
			}
			this.pos = new HandleRef(this, intPtr);
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000358 RID: 856 RVA: 0x000094CE File Offset: 0x000076CE
		internal HandleRef Handle
		{
			get
			{
				return this.pos;
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000094D6 File Offset: 0x000076D6
		public void Dispose()
		{
			this.Cleanup();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000094E4 File Offset: 0x000076E4
		private void Cleanup()
		{
			if (this.pos.Handle != IntPtr.Zero)
			{
				Stdlib.free(this.pos.Handle);
				this.pos = new HandleRef(this, IntPtr.Zero);
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000951E File Offset: 0x0000771E
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"(",
				base.ToString(),
				" ",
				this.GetDump(),
				")"
			});
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00009558 File Offset: 0x00007758
		private string GetDump()
		{
			if (FilePosition.FilePositionDumpSize <= 0)
			{
				return "internal error";
			}
			StringBuilder stringBuilder = new StringBuilder(FilePosition.FilePositionDumpSize + 1);
			if (Stdlib.DumpFilePosition(stringBuilder, this.Handle, FilePosition.FilePositionDumpSize + 1) <= 0)
			{
				return "internal error dumping fpos_t";
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600035D RID: 861 RVA: 0x000095A4 File Offset: 0x000077A4
		public override bool Equals(object obj)
		{
			FilePosition filePosition = obj as FilePosition;
			return obj != null && !(filePosition == null) && this.ToString().Equals(obj.ToString());
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000095D7 File Offset: 0x000077D7
		public bool Equals(FilePosition value)
		{
			return this == value || this.ToString().Equals(value.ToString());
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000095F0 File Offset: 0x000077F0
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00009600 File Offset: 0x00007800
		~FilePosition()
		{
			this.Cleanup();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000962C File Offset: 0x0000782C
		public static bool operator ==(FilePosition lhs, FilePosition rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00009635 File Offset: 0x00007835
		public static bool operator !=(FilePosition lhs, FilePosition rhs)
		{
			return !object.Equals(lhs, rhs);
		}

		// Token: 0x04000138 RID: 312
		private static readonly int FilePositionDumpSize = Stdlib.DumpFilePosition(null, new HandleRef(null, IntPtr.Zero), 0);

		// Token: 0x04000139 RID: 313
		private HandleRef pos;
	}
}
