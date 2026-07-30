using System;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000A8 RID: 168
	internal abstract class BaseResponseHeader
	{
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00015FCF File Offset: 0x000141CF
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x00015FD8 File Offset: 0x000141D8
		public string Value
		{
			get
			{
				return this.headerValue;
			}
			set
			{
				string text;
				string text2;
				HttpEncoder.Current.HeaderNameValueEncode(null, value, out text, out text2);
				this.headerValue = text2;
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00015FFC File Offset: 0x000141FC
		internal BaseResponseHeader(string val)
		{
			this.Value = val;
		}

		// Token: 0x060008F6 RID: 2294
		internal abstract void SendContent(HttpWorkerRequest wr);

		// Token: 0x04000FEB RID: 4075
		private string headerValue;
	}
}
