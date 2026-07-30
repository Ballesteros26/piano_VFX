using System;
using System.IO;

namespace System.Data.SqlTypes
{
	// Token: 0x020002D5 RID: 725
	internal abstract class SqlStreamChars : INullable, IDisposable
	{
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06002170 RID: 8560
		public abstract bool IsNull { get; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06002171 RID: 8561
		public abstract long Length { get; }

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06002172 RID: 8562
		// (set) Token: 0x06002173 RID: 8563
		public abstract long Position { get; set; }

		// Token: 0x06002174 RID: 8564
		public abstract int Read(char[] buffer, int offset, int count);

		// Token: 0x06002175 RID: 8565
		public abstract void Write(char[] buffer, int offset, int count);

		// Token: 0x06002176 RID: 8566
		public abstract long Seek(long offset, SeekOrigin origin);

		// Token: 0x06002177 RID: 8567
		public abstract void SetLength(long value);

		// Token: 0x06002178 RID: 8568 RVA: 0x0009CC1D File Offset: 0x0009AE1D
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x00005E03 File Offset: 0x00004003
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
