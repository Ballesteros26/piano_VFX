using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x020002B7 RID: 695
	internal class Win32EncodedResource : Win32Resource
	{
		// Token: 0x06001FBB RID: 8123 RVA: 0x0007CBFC File Offset: 0x0007ADFC
		internal Win32EncodedResource(NameOrId type, NameOrId name, int language, byte[] data)
			: base(type, name, language)
		{
			this.data = data;
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x0007CC0F File Offset: 0x0007AE0F
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x0007CC17 File Offset: 0x0007AE17
		public override void WriteTo(Stream s)
		{
			s.Write(this.data, 0, this.data.Length);
		}

		// Token: 0x04001144 RID: 4420
		private byte[] data;
	}
}
