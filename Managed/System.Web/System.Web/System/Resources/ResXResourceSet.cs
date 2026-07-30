using System;
using System.Collections;
using System.IO;

namespace System.Resources
{
	// Token: 0x02000029 RID: 41
	public class ResXResourceSet : ResourceSet
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00004B2A File Offset: 0x00002D2A
		public ResXResourceSet(Stream stream)
		{
			this.Reader = new ResXResourceReader(stream);
			this.Table = new Hashtable();
			this.ReadResources();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004B4F File Offset: 0x00002D4F
		public ResXResourceSet(string fileName)
		{
			this.Reader = new ResXResourceReader(fileName);
			this.Table = new Hashtable();
			this.ReadResources();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004B74 File Offset: 0x00002D74
		public override Type GetDefaultReader()
		{
			return typeof(ResXResourceReader);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004B80 File Offset: 0x00002D80
		public override Type GetDefaultWriter()
		{
			return typeof(ResXResourceWriter);
		}
	}
}
