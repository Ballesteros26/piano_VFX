using System;

namespace System.Web
{
	// Token: 0x020000E2 RID: 226
	internal sealed class NameValueTraceData
	{
		// Token: 0x06000C14 RID: 3092 RVA: 0x0002034B File Offset: 0x0001E54B
		public NameValueTraceData(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x040010D9 RID: 4313
		public string Name;

		// Token: 0x040010DA RID: 4314
		public string Value;
	}
}
