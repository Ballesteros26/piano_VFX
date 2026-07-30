using System;
using System.Data.Common;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Data.SqlTypes
{
	// Token: 0x020002C2 RID: 706
	internal sealed class StreamOnSqlChars : SqlStreamChars
	{
		// Token: 0x06001EBC RID: 7868 RVA: 0x00094B74 File Offset: 0x00092D74
		internal StreamOnSqlChars(SqlChars s)
		{
			this._sqlchars = s;
			this._lPosition = 0L;
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001EBD RID: 7869 RVA: 0x00094B8B File Offset: 0x00092D8B
		public override bool IsNull
		{
			get
			{
				return this._sqlchars == null || this._sqlchars.IsNull;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x00094BA2 File Offset: 0x00092DA2
		public override long Length
		{
			get
			{
				this.CheckIfStreamClosed("get_Length");
				return this._sqlchars.Length;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001EBF RID: 7871 RVA: 0x00094BBA File Offset: 0x00092DBA
		// (set) Token: 0x06001EC0 RID: 7872 RVA: 0x00094BCD File Offset: 0x00092DCD
		public override long Position
		{
			get
			{
				this.CheckIfStreamClosed("get_Position");
				return this._lPosition;
			}
			set
			{
				this.CheckIfStreamClosed("set_Position");
				if (value < 0L || value > this._sqlchars.Length)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lPosition = value;
			}
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x00094C00 File Offset: 0x00092E00
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckIfStreamClosed("Seek");
			switch (origin)
			{
			case SeekOrigin.Begin:
				if (offset < 0L || offset > this._sqlchars.Length)
				{
					throw ADP.ArgumentOutOfRange("offset");
				}
				this._lPosition = offset;
				break;
			case SeekOrigin.Current:
			{
				long num = this._lPosition + offset;
				if (num < 0L || num > this._sqlchars.Length)
				{
					throw ADP.ArgumentOutOfRange("offset");
				}
				this._lPosition = num;
				break;
			}
			case SeekOrigin.End:
			{
				long num = this._sqlchars.Length + offset;
				if (num < 0L || num > this._sqlchars.Length)
				{
					throw ADP.ArgumentOutOfRange("offset");
				}
				this._lPosition = num;
				break;
			}
			default:
				throw ADP.ArgumentOutOfRange("offset");
			}
			return this._lPosition;
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x00094CD0 File Offset: 0x00092ED0
		public override int Read(char[] buffer, int offset, int count)
		{
			this.CheckIfStreamClosed("Read");
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int num = (int)this._sqlchars.Read(this._lPosition, buffer, offset, count);
			this._lPosition += (long)num;
			return num;
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x00094D48 File Offset: 0x00092F48
		public override void Write(char[] buffer, int offset, int count)
		{
			this.CheckIfStreamClosed("Write");
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this._sqlchars.Write(this._lPosition, buffer, offset, count);
			this._lPosition += (long)count;
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x00094DBD File Offset: 0x00092FBD
		public override void SetLength(long value)
		{
			this.CheckIfStreamClosed("SetLength");
			this._sqlchars.SetLength(value);
			if (this._lPosition > value)
			{
				this._lPosition = value;
			}
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x00094DE6 File Offset: 0x00092FE6
		protected override void Dispose(bool disposing)
		{
			this._sqlchars = null;
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x00094DEF File Offset: 0x00092FEF
		private bool FClosed()
		{
			return this._sqlchars == null;
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x00094DFA File Offset: 0x00092FFA
		private void CheckIfStreamClosed([CallerMemberName] string methodname = "")
		{
			if (this.FClosed())
			{
				throw ADP.StreamClosed(methodname);
			}
		}

		// Token: 0x040015CE RID: 5582
		private SqlChars _sqlchars;

		// Token: 0x040015CF RID: 5583
		private long _lPosition;
	}
}
