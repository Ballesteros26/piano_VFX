using System;
using System.IO;

namespace System.Net.Mime
{
	// Token: 0x0200059D RID: 1437
	internal interface IEncodableStream
	{
		// Token: 0x06002CCF RID: 11471
		int DecodeBytes(byte[] buffer, int offset, int count);

		// Token: 0x06002CD0 RID: 11472
		int EncodeBytes(byte[] buffer, int offset, int count);

		// Token: 0x06002CD1 RID: 11473
		string GetEncodedString();

		// Token: 0x06002CD2 RID: 11474
		Stream GetStream();
	}
}
