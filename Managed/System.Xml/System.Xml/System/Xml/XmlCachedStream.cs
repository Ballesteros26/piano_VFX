using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x0200028F RID: 655
	internal class XmlCachedStream : MemoryStream
	{
		// Token: 0x0600187D RID: 6269 RVA: 0x0008E670 File Offset: 0x0008C870
		internal XmlCachedStream(Uri uri, Stream stream)
		{
			this.uri = uri;
			try
			{
				byte[] array = new byte[4096];
				int num;
				while ((num = stream.Read(array, 0, 4096)) > 0)
				{
					this.Write(array, 0, num);
				}
				base.Position = 0L;
			}
			finally
			{
				stream.Close();
			}
		}

		// Token: 0x04001015 RID: 4117
		private const int MoveBufferSize = 4096;

		// Token: 0x04001016 RID: 4118
		private Uri uri;
	}
}
