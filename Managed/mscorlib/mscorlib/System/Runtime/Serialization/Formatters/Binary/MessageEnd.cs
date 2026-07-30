using System;
using System.Diagnostics;
using System.IO;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000719 RID: 1817
	internal sealed class MessageEnd : IStreamable
	{
		// Token: 0x06004BCA RID: 19402 RVA: 0x00002111 File Offset: 0x00000311
		internal MessageEnd()
		{
		}

		// Token: 0x06004BCB RID: 19403 RVA: 0x0010E778 File Offset: 0x0010C978
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte(11);
		}

		// Token: 0x06004BCC RID: 19404 RVA: 0x00002194 File Offset: 0x00000394
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
		}

		// Token: 0x06004BCD RID: 19405 RVA: 0x00002194 File Offset: 0x00000394
		public void Dump()
		{
		}

		// Token: 0x06004BCE RID: 19406 RVA: 0x00002194 File Offset: 0x00000394
		public void Dump(Stream sout)
		{
		}

		// Token: 0x06004BCF RID: 19407 RVA: 0x0010E782 File Offset: 0x0010C982
		[Conditional("_LOGGING")]
		private void DumpInternal(Stream sout)
		{
			if (BCLDebug.CheckEnabled("BINARY") && sout != null && sout.CanSeek)
			{
				long length = sout.Length;
			}
		}
	}
}
